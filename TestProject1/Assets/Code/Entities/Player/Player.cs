using UnityEngine;
using System.Collections;

public class Player : Entity {
	// Public
	private bool isMovingVert = false;
	private bool isMovingHori = false;
	// Use this for initialization
	void Start () {
	
	}

	void Update() {
		isMovingVert = false;
		isMovingHori = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		isMovingVert = false;
		isMovingHori = false;

		float moveVert = Input.GetAxis ("Vertical");
		float moveHori = Input.GetAxis ("Horizontal");
		// Vertical
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) 
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * moveVert);
			isMovingVert = true;
		}
		else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow))
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * moveVert);
			isMovingVert = true;
		}
		else
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.2f);
		}
		// Horizontal
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) 
		{
			rigidbody2D.velocity = new Vector2(speed * moveHori, rigidbody2D.velocity.y);
			isMovingHori = true;
		}
		else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) 
		{
			rigidbody2D.velocity = new Vector2(speed * moveHori, rigidbody2D.velocity.y);
			isMovingHori = true;
		}
		else
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x * 0.2f, rigidbody2D.velocity.y);
		}
	
	}
	
}
