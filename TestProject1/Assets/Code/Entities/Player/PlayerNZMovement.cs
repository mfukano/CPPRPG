using UnityEngine;
using System.Collections;

public class PlayerNZMovement : MonoBehaviour {

	private float walkSpeed;
	private float curSpeed;
	private float maxSpeed;

	public float playerSpeed;
	public float bulletSpeed;
	public bool bulletInitVel;
	Animator anim;

	void Start() {
		anim = GetComponent<Animator>();

		walkSpeed = 300;
	}

	void FixedUpdate() {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePos - transform.position);
		Vector2 velocity = new Vector2 (Mathf.Lerp (0, Input.GetAxis ("Horizontal") * playerSpeed, 0.8f),
			                                Mathf.Lerp (0, Input.GetAxis ("Vertical") * playerSpeed, 0.8f));
		velocity.Normalize();
		velocity *= playerSpeed;
		rigidbody2D.velocity = velocity;
		rigidbody2D.angularVelocity = 0;

	}

	void Update() {
		if (Input.GetMouseButtonDown (1)) {
			anim.SetTrigger ("Attack");
		}
		if (Input.GetMouseButtonDown (0)) {
			GameObject bullet = Instantiate(Resources.Load("bullet"), transform.position, transform.rotation) as GameObject;
			if (bulletInitVel)
				bullet.rigidbody2D.AddForce (bullet.transform.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
			else
				bullet.rigidbody2D.AddForce (bullet.transform.up * bulletSpeed);
		}
		float input = Input.GetAxis ("Vertical");
		anim.SetFloat ("Speed", rigidbody2D.velocity.magnitude);
	}
}