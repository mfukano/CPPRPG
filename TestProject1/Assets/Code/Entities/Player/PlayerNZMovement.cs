using UnityEngine;
using System.Collections;

public class PlayerNZMovement : MonoBehaviour {

	public float speed;
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
		rigidbody2D.AddForce (gameObject.transform.up * speed * input);
		rigidbody2D.angularVelocity = 0;
	}

	void updateAnim() {
		if (Input.GetMouseButtonDown (0)) {
			anim.SetTrigger ("Attack");
		}
		float input = Input.GetAxis ("Vertical");
		anim.SetFloat ("Speed", speed * input);
	}
}
