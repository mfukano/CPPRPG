using UnityEngine;
using System.Collections;

public class Player : Entity {
	// Public
	private bool isMovingVert = false;
	private bool isMovingHori = false;

	public Vector3 velocity;
	private Vector3 truePos;
	// Use this for initialization
	void Start () {
		velocity = Vector3.zero;
	}

	void Update() {
		isMovingVert = false;
		isMovingHori = false;
		//rigidbody2D.transform.position = new Vector3 (Mathf.Round (transform.position.x), Mathf.Round (transform.position.y), 0.0f);
	}
	
	// Update is called once per frame
//	void FixedUpdate () {
//		isMovingVert = false;
//		isMovingHori = false;
//
//		float moveVert = Input.GetAxis ("Vertical");
//		float moveHori = Input.GetAxis ("Horizontal");
//		// Vertical
//		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) 
//		{
//			rigidbody2D.velocity = new Vector3(rigidbody2D.velocity.x, Mathf.Round(speed * moveVert));
//			isMovingVert = true;
//		}
//		else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow))
//		{
//			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Round(speed * moveVert));
//			isMovingVert = true;
//		}
//		else // Slow down
//		{
//			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Round(rigidbody2D.velocity.y * 0.2f));
//		}
//		// Horizontal
//		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) 
//		{
//			rigidbody2D.velocity = new Vector2(Mathf.Round(speed * moveHori), rigidbody2D.velocity.y);
//			isMovingHori = true;
//		}
//		else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) 
//		{
//			rigidbody2D.velocity = new Vector2(Mathf.Round(speed * moveHori), rigidbody2D.velocity.y);
//			isMovingHori = true;
//		}
//		else // Slow down
//		{
//			rigidbody2D.velocity = new Vector2(Mathf.Round(rigidbody2D.velocity.x * 0.2f), rigidbody2D.velocity.y);
//		}
//
//
//		//rigidbody2D.transform.position = new Vector3 (Mathf.FloorToInt (transform.position.x), Mathf.FloorToInt (transform.position.y), 0);
//	
//	}

	void FixedUpdate()
	{
		isMovingVert = false;
		isMovingHori = false;

		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) 
		{
			velocity = new Vector3(velocity.x, Mathf.Round(speed * Time.deltaTime), 0.0f);
			isMovingVert = true;
		}
		else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow))
		{
			velocity = new Vector3(velocity.x, Mathf.Round(-speed * Time.deltaTime), 0.0f);
			isMovingVert = true;
		}
		else // Slow down
		{
			velocity = new Vector3(velocity.x, Mathf.Round(velocity.y * 0.2f * Time.deltaTime), 0.0f);
		}
		// Horizontal
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) 
		{
			velocity = new Vector3(Mathf.Round(speed * Time.deltaTime), velocity.y, 0.0f);
			isMovingHori = true;
		}
		else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) 
		{
			velocity = new Vector3(Mathf.Round(-speed * Time.deltaTime), velocity.y, 0.0f);
			isMovingHori = true;
		}
		else // Slow down
		{
			velocity = new Vector3(Mathf.Round(velocity.x * 0.2f * Time.deltaTime), velocity.y, 0.0f);
		}

		velocity.Normalize ();
		velocity *= speed/100f;
		truePos = rigidbody2D.transform.position;
		truePos += velocity;
		rigidbody2D.velocity = velocity * 50f;
		//rigidbody2D.transform.position = new Vector3 (Mathf.Round (truePos.x), Mathf.Round (truePos.y), 0.0f);

	}
	
}
