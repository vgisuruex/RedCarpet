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
        public List<Vector3> objVerts = new List<Vector3>();

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
                                    List<float> verticesList = new List<float>();

                                    for (long pos = 0; pos < positionBuffer.Data[0].Length; pos += positionBuffer.Stride)
                                    {
                                        reader.Seek(pos, SeekOrigin.Begin);
                                        verticesList.Add(reader.ReadSingle());
                                        verticesList.Add(reader.ReadSingle());
                                        verticesList.Add(reader.ReadSingle());
                                        if (attrib.Format == GX2AttribFormat.Format_32_32_32_32_Single)
                                        {
                                            float temp = reader.ReadSingle();
                                            verticesList.Add(temp);
                                         }
                                    }

                                    // Convert the list into an array
                                    verticesArray = verticesList.ToArray();
                                    for (int i = 0; i < verticesArray.Length; i++)
                                    {
                                        Vector3 temp = new Vector3(verticesArray[i], verticesArray[i + 1], verticesArray[i + 2]);
                                        objVerts.Add(temp);
                                        i += 2;
                                    }

                                    break;
                                default:
                                    throw new Exception("Unsupported attribute format (" + attrib.Format + ")");
                            }
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
