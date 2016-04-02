using UnityEngine;
using Headache.TopdownComponent;

namespace Headache.TopdownComponent
{
    /// <summary>
    /// Drag this script into any GameObject with a renderer (ex a sprite renderer) and a TopdownTransform to link the sorting order and the y axis of the TopdownTransform. Optionally you can 
    /// drag a TopdownTransform if the GameObject doesn't have any.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Renderer))]
    public class TopdownDynamicLayerSorting : MonoBehaviour
    {

        Renderer r;
        public TopdownTransform topdownTransform;

        // Use this for initialization
        void Awake()
        {
            r = GetComponent<Renderer>();
            if (topdownTransform == null)
                topdownTransform = GetComponent<TopdownTransform>();

        }

        // Update is called once per frame
        void Update()
        {
            if(topdownTransform != null)
                r.sortingOrder = -(int)(topdownTransform.position.z * 1000);
        }
    }
}
