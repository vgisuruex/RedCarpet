using OpenTK.Graphics.OpenGL4;
using Syroot.BinaryData;
using Syroot.NintenTools.Bfres;
using Syroot.NintenTools.Bfres.GX2;
using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;

namespace RedCarpet.Gfx
{
    public class SmModel : IDisposable
    {
        private readonly List<SmMesh> meshes = new List<SmMesh>();
        public Vector3 bboxMin;
        public Vector3 bboxMax;

        public SmModel(Model model, ByteOrder byteOrder)
        {
            foreach (String shapeKey in model.Shapes.Keys)
            {
                Shape shape = model.Shapes[shapeKey];

                // Get the vertex buffer for this FSHP
                VertexBuffer vertexBuffer = model.VertexBuffers[shape.VertexBufferIndex];

                // Initialize a variable that will be used to hold vertex data
                float[] verticesArray = null;

                // Find the position attributes
                foreach (string attribKey in vertexBuffer.Attributes.Keys)
                {
                    VertexAttrib attrib = vertexBuffer.Attributes[attribKey];

                    if (attribKey.Equals("_p0"))
                    {
                        // Get the buffer with positions
                        Syroot.NintenTools.Bfres.Buffer positionBuffer = vertexBuffer.Buffers[attrib.BufferIndex];

                        // Create a List to store all the floats
                        List<float> rawVertices = new List<float>();

                        // Create a List to store Vector3s for BBox calculations later
                        List<Vector3> positionVectors = new List<Vector3>();

                        // Open a reader to make things easier
                        using (MemoryStream stream = new MemoryStream(positionBuffer.Data[0]))
                        using (BinaryDataReader reader = new BinaryDataReader(stream))
                        {
                            // Set the byte order to what the bfres specifies
                            reader.ByteOrder = byteOrder;

                            switch (attrib.Format)
                            {
                                case GX2AttribFormat.Format_32_32_32_32_Single:
                                case GX2AttribFormat.Format_32_32_32_Single:
                                    for (long pos = 0; pos < positionBuffer.Data[0].Length; pos += positionBuffer.Stride)
                                    {
                                        // Seek to the position in the buffer
                                        reader.Seek(pos, SeekOrigin.Begin);

                                        // Read in all floats
                                        rawVertices.Add(reader.ReadSingle());
                                        rawVertices.Add(reader.ReadSingle());
                                        rawVertices.Add(reader.ReadSingle());

                                        if (attrib.Format == GX2AttribFormat.Format_32_32_32_32_Single)
                                            rawVertices.Add(reader.ReadSingle());
                                    }

                                    break;
                                default:
                                    throw new Exception("Unsupported attribute format (" + attrib.Format + ")");
                            }
                        }

                        // Convert the vertices list into an array
                        verticesArray = rawVertices.ToArray();

                        // Create Vector3s for BBox calculations
                        for (int i = 0; i < verticesArray.Length; i += 3)
                        {
                            positionVectors.Add(new Vector3(verticesArray[i], verticesArray[i + 1], verticesArray[i + 2]));
                        }

                        // Calculate BBox
                        bboxMax = CalculateBBMax(positionVectors);
                        bboxMin = CalculateBBMin(positionVectors);

                        break;
                    }
                }

                // Generate the VBO for this FSHP
                int vboId;
                GL.GenBuffers(1, out vboId);
                GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
                GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * verticesArray.Length, verticesArray, BufferUsageHint.StaticDraw);

                // Use LoD 0 as the mesh
                meshes.Add(new SmMesh(shape.Meshes[0], vboId));
            }
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

        private Vector3 CalculateBBMin(List<Vector3> positionVectors)
        {
            Vector3 minimum = new Vector3();
            foreach (Vector3 position in positionVectors)
            {
                if (position.X < minimum.X) minimum.X = position.X;
                if (position.Y < minimum.Y) minimum.Y = position.Y;
                if (position.Z < minimum.Z) minimum.Z = position.Z;
            }

            return minimum;
        }

        private Vector3 CalculateBBMax(List<Vector3> positionVectors)
        {
            Vector3 maximum = new Vector3();
            foreach (Vector3 position in positionVectors)
            {
                if (position.X > maximum.X) maximum.X = position.X;
                if (position.Y > maximum.Y) maximum.Y = position.Y;
                if (position.Z > maximum.Z) maximum.Z = position.Z;
            }

            return maximum;
        }

    }
}
