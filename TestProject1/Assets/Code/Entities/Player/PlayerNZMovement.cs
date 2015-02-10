using UnityEngine;
using System.Collections;

public class PlayerNZMovement : MonoBehaviour {

	public float playerSpeed;
	public float bulletSpeed;
	Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
	}

	void FixedUpdate() {
		lookAtMouse ();
		move ();
	}

	void Update() {
		updateAnim ();
	}

	void lookAtMouse() {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePos - transform.position);
	}

	void move() {
		float input = Input.GetAxis ("Vertical");
		rigidbody2D.AddForce (gameObject.transform.up * playerSpeed * input);
		rigidbody2D.angularVelocity = 0;
	}

	void updateAnim() {
		if (Input.GetMouseButtonDown (1)) {
			anim.SetTrigger ("Attack");
		}
		if (Input.GetMouseButtonDown (0)) {
			GameObject bullet = Instantiate(Resources.Load("bullet"), transform.position, transform.rotation) as GameObject;
			bullet.rigidbody2D.AddForce (bullet.transform.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
		}
		float input = Input.GetAxis ("Vertical");
		anim.SetFloat ("Speed", playerSpeed * input);
	}
}
