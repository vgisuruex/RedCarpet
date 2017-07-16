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
        public readonly SmBoundingBox boundingBox;

        public SmModel(Model model, ByteOrder byteOrder)
        {
            // Create a List to hold all shape vertices for BBox calculation
            List<Vector3> positionVectors = new List<Vector3>();

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

                        // Create a List to hold the raw vertices
                        List<float> rawVertices = new List<float>();

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
                                    // Read in the whole buffer as floats
                                    for (long pos = 0; pos < positionBuffer.Data[0].Length; pos += positionBuffer.Stride)
                                    {
                                        reader.Seek(pos, SeekOrigin.Begin);

                                        rawVertices.Add(reader.ReadSingle());
                                        rawVertices.Add(reader.ReadSingle());
                                        rawVertices.Add(reader.ReadSingle());

                                        if (attrib.Format == GX2AttribFormat.Format_32_32_32_32_Single)
                                            rawVertices.Add(reader.ReadSingle());
                                    }

                                    // Convert the list into an array
                                    verticesArray = rawVertices.ToArray();

                                    break;
                                default:
                                    throw new Exception("Unsupported attribute format (" + attrib.Format + ")");
                            }
                        }

                        // Convert the list into an array
                        verticesArray = rawVertices.ToArray();

                        // Create Vector3s for BBox calculation
                        for (int i = 0; i < rawVertices.Count; i += 3)
                        {
                            positionVectors.Add(new Vector3(rawVertices[i], rawVertices[i + 1], rawVertices[i + 2]));
                        }

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

            // Create the bounding box for this model
            boundingBox = new SmBoundingBox(positionVectors);
        }

        public void Render()
        {
            RenderMeshes();
        }

        private void RenderMeshes()
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
