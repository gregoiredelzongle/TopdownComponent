using UnityEngine;
using System.Collections;
using Headache.TopdownComponent;

/*
This script fakes 3D coordinates for a 2D Topdown game. 
*/

namespace Headache.TopdownComponent
{

    [ExecuteInEditMode]
    public class TopdownTransform : MonoBehaviour
    {
        #region Public Variables

        // The fake 2.5d coordinate. Use that to move this object instead of transform.position
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

        public bool showFloor = true;
        #endregion

        #region Private Variables

        // The 2.5d to 2d transformation matrix
        private Matrix4x4 matrix;
        #endregion

        #region Main Methods
        void Awake()
        {
            matrix = TopdownUtils.TopDownMatrix();
            if(!Application.isPlaying)
                position = WorldToTdPosition(transform.position);
        }

        #endregion

        // Convert a world space position (= world position) into the fake one

        public Vector3 WorldToTdPosition(Vector3 worldPos)
        {
            return new Vector3(worldPos.x, position.y, worldPos.y);
        }

        //Translate gameObject

        public void Translate(Vector3 dir)
        {
            position += dir;
        }

        // Get World space floor position for the current object

        public Vector3 FloorPosition()
        {
            return transform.position - new Vector3(0, position.y * TopdownUtils.AngleRatio, 0);
        }

        // Update world space transform using fake one

        public void UpdateTransform()
        {
            transform.position = matrix.MultiplyPoint(position);
        }
    }
}


    
