using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float playerSpeed;
	public float bulletSpeed;
	public bool bulletInitVel;
	private float velMag;
	Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
	}

	void FixedUpdate() {
		float horiz = Input.GetAxisRaw ("Horizontal");
		float vert = Input.GetAxisRaw ("Vertical");
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePos - transform.position);

		Vector2 velocity = new Vector2 (Mathf.Lerp (0, horiz * playerSpeed, 0.8f),
			                                Mathf.Lerp (0, vert * playerSpeed, 0.8f));
		if ((horiz != 0) || (vert != 0)) {
			velocity.Normalize ();
		}
		velocity *= playerSpeed;
		rigidbody2D.velocity = velocity;
		rigidbody2D.angularVelocity = 0;

	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 100, 20), velMag.ToString());
	}
	
	void Update() {
		velMag = rigidbody2D.velocity.magnitude;
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
