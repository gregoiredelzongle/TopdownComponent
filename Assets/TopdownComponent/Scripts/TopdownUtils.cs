using UnityEngine;
using Headache.TopdownComponent;

namespace Headache.TopdownComponent
{

    public static class TopdownUtils
    {

        // Angle of the TopDown view in degrees
        public static float angle = 30;
        public static float AngleRatio { get { return angle / 90; } }

        /// <summary>
        /// Return the Topdown transformation matrix
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Convert World space point (2D) into Topdown space point (3D)
        /// </summary>
        /// <param name="worldPos">world position of the object</param>
        /// <param name="height">facultative height of the object (default 0)</param>
        /// <returns></returns>
        public static Vector3 WorldToTdPosition(Vector3 worldPos, float height = 0)
        {
            return new Vector3(worldPos.x, height, worldPos.y);
        }

        /// <summary>
        /// Convert Topdown space point (2D) into world space point (3D)
        /// </summary>
        /// <param name="tdPos">3D position of the object</param>
        /// <returns></returns>
        public static Vector3 TdToWorldPosition(Vector3 tdPos)
        {
            return TopDownMatrix().MultiplyPoint(tdPos);
        }

        /// <summary>
        /// Get average center of multiple vectors
        /// </summary>
        /// <param name="vectors"></param>
        /// <returns></returns>
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
