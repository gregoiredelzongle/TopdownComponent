using UnityEngine;
using Headache.TopdownComponent;

namespace Headache.TopdownComponent
{

    public static class TopdownUtils
    {

        // Fake Topdown angle
        public static float angle = 30;
        public static float AngleRatio { get { return angle / 90; } }

        // Return Topdown Matrix to convert 2.5d to world space
        public static Matrix4x4 TopDownMatrix()
        {
            Matrix4x4 matrix = new Matrix4x4();
            Vector4[] rows =
            {
            new Vector4(1,0,0,0),
            new Vector4(0,AngleRatio,1,0),
            new Vector4(0,0,0,0),
            new Vector4(0,0,0,1)
        };
            for (int i = 0; i < 4; i++)
                matrix.SetRow(i, rows[i]);

            return matrix;
        }
        // Convert World point into Topdown point
        public static Vector3 WorldToTdPosition(Vector3 worldPos, float height = 0)
        {
            return new Vector3(worldPos.x, height, worldPos.y);
        }

        // Convert topDown point into world point
        public static Vector3 TdToWorldPosition(Vector3 tdPos)
        {
            return TopDownMatrix().MultiplyPoint(tdPos);
        }

        // Average center of multiple points in space
        public static Vector3 VectorsCenter(Vector3[] vectors)
        {
            Vector3 sum = Vector3.zero;
            if (vectors == null || vectors.Length == 0)
            {
                return sum;
            }

            foreach (Vector3 vec in vectors)
            {
                sum += vec;
            }
            return sum / vectors.Length;
        }

    }
}
