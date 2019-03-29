using OpenTK.Graphics.OpenGL4;
using Syroot.NintenTools.Bfres;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace RedCarpet.Gfx
{
    class SmMesh : IDisposable
    {
        private int vboId;
        private int eboId;
        private int vaoId;
        private int drawCount;
        private DrawElementsType indicesType;
        //private BeginMode drawType;

        public SmMesh(Mesh mesh, int vboId)
        {
            // Set the VBO ID and bind it
            this.vboId = vboId;
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);

            // Generate an EBO
            GL.GenBuffers(1, out eboId);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboId);

            // Get the indices
            uint[] indicesArray = mesh.GetIndices().ToArray();

            // Set the buffer data
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * indicesArray.Length, indicesArray, BufferUsageHint.StaticDraw);

            // Set class settings
            indicesType = DrawElementsType.UnsignedInt;
            drawCount = indicesArray.Length;

            // Set the primitive type to use (TODO)
            //drawType = (BeginMode)mesh.PrimitiveType;

            // Generate the VAO
            GL.GenVertexArrays(1, out vaoId);
            GL.BindVertexArray(vaoId);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindVertexArray(0);
        }

        public void Render()
        {
            // Render the VAO
            GL.BindVertexArray(vaoId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboId);
            //GL.DrawArrays(OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles, 0, drawCount);
            GL.DrawElements(BeginMode.Triangles, drawCount, indicesType, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            // Delete all buffers and the VAO
            GL.DeleteBuffer(vboId);
            GL.DeleteBuffer(eboId);
            GL.DeleteVertexArray(vaoId);
            int a = loadImage((Bitmap)Bitmap.FromFile("DefaultTexture.png"));
        }
        public static int loadImage(Bitmap image)
        {
            int texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);
            BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            image.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 2);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return texID;
        }

    }
}