using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {

	public int ratio;
	private Map myMap;
	private Player myPlayer;
	public float dampTime = 0.2f;
	private Vector3 velocity = Vector3.zero;
	private Vector3 cameraTruePos;
	public Transform target;

	void Awake () {
		myPlayer = (Player)GameObject.FindObjectOfType (typeof(Player));// ("Player");
		target = myPlayer.transform;
		transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
	}


	// Use this for initialization
	void Start () {
		camera.orthographicSize = Screen.height / (2 * ratio);
		
		myMap = (Map)GameObject.FindObjectOfType (typeof(Map));
	}
	
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
	
	void LateUpdate()
	{
		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			
			cameraTruePos = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			//transform.position = cameraTruePos; //+ new Vector3( -0.1f, -0.1f, 0f);
			transform.position = new Vector3(
				Mathf.Clamp(cameraTruePos.x, -myMap.width / 2 + camera.pixelWidth / 2, myMap.width / 2 - camera.pixelWidth / 2),
				Mathf.Clamp(cameraTruePos.y, -myMap.height / 2 + camera.pixelHeight / 2, myMap.height / 2 - camera.pixelHeight / 2),
				cameraTruePos.z);
			
			//var v = target.position;
			//transform.position = new Vector3 (Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(transform.position.z));
		}
	}
}
