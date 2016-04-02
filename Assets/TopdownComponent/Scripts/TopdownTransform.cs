using UnityEngine;
using Headache.TopdownComponent;

namespace Headache.TopdownComponent
{
    /// <summary>
    /// Simulate object position in the "Topdown space", by making it move like a 3D object without touching the z axis, using a matrix projection.
    /// </summary>
    [ExecuteInEditMode]
    public class TopdownTransform : MonoBehaviour
    {
        #region Public Variables

        // The 3d coordinates of the object. Use that to move this object instead of transform.position
        public Vector3 position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                UpdateTransform();
            }
        }
        [SerializeField]
        private Vector3 _position;

        // Show floor gizmo in the scene to help adjust the object height
        public bool showFloor = true;
        #endregion

        #region Private Variables

        // The 3D to 2D transformation matrix
        private Matrix4x4 matrix;
        #endregion

        #region Main Methods
        void Awake()
        {
            // Create the projection matrix and store it
            matrix = TopdownUtils.TopDownMatrix();

            if(!Application.isPlaying)
                position = WorldToTdPosition(transform.position);
        }

        #endregion

        #region Topdown Methods

        /// <summary>
        /// Translate the GameObject
        /// </summary>
        /// <param name="direction"></param>
        public void Translate(Vector3 dir)
        {
            position += dir;
        }

        #endregion

        #region Topdown Utility Methods
        /// <summary>
        /// Convert a world position into a topdown position
        /// </summary>
        /// <param name="world position"></param>
        /// <returns></returns>
        public Vector3 WorldToTdPosition(Vector3 worldPos)
        {
            return new Vector3(worldPos.x, position.y, worldPos.y);
        }



        /// <summary>
        /// Return the world position of the floor
        /// </summary>
        public Vector3 FloorPosition()
        {
            return transform.position - new Vector3(0, position.y * TopdownUtils.AngleRatio, 0);
        }

        /// <summary>
        /// Update GameObject transform using a matrix projection
        /// </summary>
        public void UpdateTransform()
        {
            transform.position = matrix.MultiplyPoint(position);
        }

        #endregion
    }
}


    
