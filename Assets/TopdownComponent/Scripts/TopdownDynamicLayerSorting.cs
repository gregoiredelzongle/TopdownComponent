using UnityEngine;
using System.Collections;
using Headache.TopdownComponent;

namespace Headache.TopdownComponent
{
    // Use this script if you need to sort a renderer (ex sprite) based on fake 2.5d space

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
