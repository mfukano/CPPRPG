using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {

	public int ratio;

	// Use this for initialization
	void Start () {
		camera.orthographicSize = Screen.height / (2 * ratio);
	
	}

	public float dampTime = 0.2f;
	private Vector3 velocity = Vector3.zero;
	private Vector3 cameraTruePos;
	public Transform target;
	
	// Update is called once per frame
//	void Update () 
//	{
//		if (target)
//		{
//			Vector3 point = camera.WorldToViewportPoint(target.position);
//			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
//			Vector3 destination = transform.position + delta;
//			cameraTruePos = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
//			transform.position = cameraTruePos; //+ new Vector3( -0.1f, -0.1f, 0f);
//
//			//var v = target.position;
//			//transform.position = Vector3.MoveTowards(transform.position, v, 10 * Time.deltaTime);
//			//var v = target.position;
//			//transform.position = new Vector3 (v.x, v.y, transform.position.z);
//		}
//
//		
//	}

	void Update()
	{
		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			cameraTruePos = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			transform.position = cameraTruePos; //+ new Vector3( -0.1f, -0.1f, 0f);
			
			//var v = target.position;
			//transform.position = new Vector3 (Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(transform.position.z));
		}
	}
}
