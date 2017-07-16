using OpenTK.Graphics.OpenGL4;
using Syroot.BinaryData;
using Syroot.NintenTools.Bfres;
using Syroot.NintenTools.Bfres.GX2;
using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;
using System.Reflection;
using Syroot.NintenTools.Bfres.Helpers;

namespace RedCarpet.Gfx
{
    public class SmModel : IDisposable
    {
        private readonly List<SmMesh> meshes = new List<SmMesh>();
        public readonly SmBoundingBox boundingBox;

        public SmModel(Model model, ByteOrder byteOrder)
        {
            // Create a List to hold all shape vertices for BBox calculation
            List<Vector3> positionVectors = new List<Vector3>();

            foreach (String shapeKey in model.Shapes.Keys)
            {
                Shape shape = model.Shapes[shapeKey];

                // Create a List to hold the raw vertices for this Shape
                List<float> rawVertices = new List<float>();

                // Create the VertexBufferHelper with this Shape's vertex buffer
                VertexBufferHelper helper = new VertexBufferHelper(model.VertexBuffers[shape.VertexBufferIndex], byteOrder);

                // Get the positions in Vector4Fs
                VertexBufferHelperAttrib positionAttrib = helper["_p0"];
                Syroot.Maths.Vector4F[] vec4Positions = positionAttrib.Data;

                foreach (Syroot.Maths.Vector4F position in vec4Positions)
                {
                    // Switch based on format
                    switch (positionAttrib.Format)
                    {
                        case GX2AttribFormat.Format_32_32_32_32_Single:
                        case GX2AttribFormat.Format_32_32_32_Single:
                            rawVertices.Add(position.X);
                            rawVertices.Add(position.Y);
                            rawVertices.Add(position.Z);

                            if (positionAttrib.Format == GX2AttribFormat.Format_32_32_32_32_Single)
                                rawVertices.Add(position.W);

                            break;
                        case GX2AttribFormat.Format_16_16_16_16_Single:
                            rawVertices.Add(position.X);
                            rawVertices.Add(position.Y);
                            rawVertices.Add(position.Z);
                            rawVertices.Add(position.W);

                            break;
                        default:
                            throw new Exception("Unhandled attribute format " + positionAttrib.Format + ", go nag OatmealDome");
                    }
                }

                // Convert the list into an array
                float[] verticesArray = rawVertices.ToArray();

                // Create Vector3s for BBox calculation
                for (int i = 0; i < verticesArray.Length; i += 3)
                {
                    positionVectors.Add(new Vector3(verticesArray[i], verticesArray[i + 1], verticesArray[i + 2]));
                }

                // Generate the VBO for this Shape
                int vboId;
                GL.GenBuffers(1, out vboId);
                GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
                GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * verticesArray.Length, verticesArray, BufferUsageHint.StaticDraw);

                // Use LoD 0 as the mesh
                meshes.Add(new SmMesh(shape.Meshes[0], vboId));
            }

            // Create the bounding box for this model
            boundingBox = new SmBoundingBox(positionVectors);
        }

        public void Render()
        {
            foreach (SmMesh mesh in meshes)
            {
                mesh.Render();
            }
        }

        public void Dispose()
        {
            foreach (SmMesh mesh in meshes)
            {
                mesh.Dispose();
            }
        }

    }
}
