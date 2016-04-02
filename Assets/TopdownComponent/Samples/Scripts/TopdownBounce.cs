using UnityEngine;
using System.Collections;
using Headache.TopdownComponent;

public class TopdownBounce : MonoBehaviour {

    public Vector3 direction;
    public float maxHeight = 1.0f;
    public float bounceSpeed = 1.0f;

    private Vector3 velocity;

    private TopdownTransform tdTransform;

	// Use this for initialization
	void Awake () {
        tdTransform = GetComponent<TopdownTransform>();
	}

    void Start()
    {
        StartCoroutine("StartBounce");
    }

    IEnumerator StartBounce()
    {
        while(true)
        {
            tdTransform.position = new Vector3
                (tdTransform.position.x + direction.x * Time.deltaTime, 
                Bounce((Time.time * bounceSpeed) % 1) * maxHeight, 
                tdTransform.position.z + direction.z * Time.deltaTime);

            yield return 0;
        }
    }

    float Bounce(float t){
    return Mathf.Sin(Mathf.Clamp01(t) * Mathf.PI);
}

}
