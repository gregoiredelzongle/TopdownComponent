using UnityEngine;
using System.Collections;
using Headache.TopdownComponent;

[RequireComponent(typeof(SpriteRenderer))]
public class TopdownDynamicShadow : MonoBehaviour {

    public TopdownTransform topdownTransform;

    public float sizeMultiplier = 0.5f;

    private Vector3 localScale;
    
	// Use this for initialization
	void Awake () {
        localScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

        if(topdownTransform != null)
        {
            transform.localScale = localScale*Mathf.Max(0,1 - Mathf.Max(0, topdownTransform.position.y*sizeMultiplier));
            transform.position = topdownTransform.FloorPosition();
        }
	
	}
}
