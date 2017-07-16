using OpenTK;
using System.Collections.Generic;

namespace RedCarpet.Gfx
{
    public class SmBoundingBox
    {
        // Min/Max
        public readonly Vector3 minimum;
        public readonly Vector3 maximum;

        public SmBoundingBox(List<Vector3> positionVectors)
        {
            // Calculate Vector3s for minimum and maximum
            minimum = CalculateBBMin(positionVectors);
            maximum = CalculateBBMax(positionVectors);
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
