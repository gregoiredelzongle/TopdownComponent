using UnityEngine;
using System.Collections;
using Headache.TopdownComponent;

public class MoveToPosition : MonoBehaviour {

    public Transform target;
    public float speed = 1;
    public bool loop = true;

    private Vector3 start;
    private Vector3 end;
    private TopdownTransform tdTransform;

	// Use this for initialization
	void Awake () {
        tdTransform = GetComponent<TopdownTransform>();
        start = tdTransform.position;
        end = TopdownUtils.WorldToTdPosition(target.position);
	}
	
	// Update is called once per frame
	void Update () {
        tdTransform.position = Vector3.Lerp(start, end, Mathf.PingPong(Time.time * speed,1));
	}
}
