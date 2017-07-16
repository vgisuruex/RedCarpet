using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace RedCarpet.Gfx
{
    public class SmBoundingBox : IDisposable
    {
        // Min/Max
        public readonly Vector3 minimum;
        public readonly Vector3 maximum;

        // OpenGL items
        private readonly int vboId;
        private readonly int eboId;
        private readonly int vaoId;

        public SmBoundingBox(List<Vector3> positionVectors)
        {
            // Calculate Vector3s for minimum and maximum
            minimum = CalculateBBMin(positionVectors);
            maximum = CalculateBBMax(positionVectors);

            // Create the vertices array to transfer into the VBO
            float[] vertices = new float[]
            {
                minimum.X, maximum.Y, minimum.Z, // corner A
                maximum.X, maximum.Y, minimum.Z, // corner B
                minimum.X, minimum.Y, minimum.Z, // corner C
                maximum.X, minimum.Y, minimum.Z, // corner D
                minimum.X, maximum.Y, maximum.Z, // corner E
                maximum.X, maximum.Y, maximum.Z, // corner F
                minimum.X, minimum.Y, maximum.Z, // corner G
                maximum.X, minimum.Y, maximum.Z, // corner H
            };

            // Generate the VBO
            GL.GenBuffers(1, out vboId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StaticDraw);

            // Create the indices array to transfer into the EBO
            uint[] indices = new uint[]
            {
                0, 1, 2, 3, // ABCD
                0, 2, 6, 4, // ACGE
                4, 5, 6, 7, // EFGH
                1, 3, 7, 5, // BDHF
            };

            // Generate the EBO
            GL.GenBuffers(1, out eboId);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboId);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * indices.Length, indices, BufferUsageHint.StaticDraw);

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
            GL.DrawElements(BeginMode.Quads, 16, DrawElementsType.UnsignedInt, 0);
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
