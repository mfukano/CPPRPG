using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxHealth = 1000;
	public float startHealth = 800;
	public float currentHealth;
	public Slider healthBar;
	public Slider energyBar;

	public float playerSpeed;
	public float bulletSpeed;
	public bool bulletInitVel;
	private float velMag;
	private bool isDead;
	Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
		currentHealth = startHealth;
		healthBar.value = currentHealth;
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

	public void healDamage (float heal_val) {
		if (currentHealth != maxHealth) {
			currentHealth += heal_val;
			healthBar.value = currentHealth;
				}

	}


	public void takeDamage (float dmg_val) {
		currentHealth -= dmg_val;
		healthBar.value = currentHealth;

		if (currentHealth <= 0) {
			Death();
				}
		}

	void Death(){
		isDead = true;
		playerSpeed = 0;

	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 100, 20), velMag.ToString());
	}
	
	void Update() {
		velMag = rigidbody2D.velocity.magnitude;
		if (Input.GetMouseButtonDown (1)) {
			anim.SetTrigger ("Attack");
		}
		if (Input.GetMouseButtonDown (0) && Time.timeScale == 1) {
			GameObject bullet = Instantiate(Resources.Load("bullet"), transform.position, transform.rotation) as GameObject;
			if (bulletInitVel)
				bullet.rigidbody2D.AddForce (bullet.transform.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
			else
				bullet.rigidbody2D.AddForce (bullet.transform.up * bulletSpeed);
		}
		float input = Input.GetAxisRaw ("Vertical");
		anim.SetFloat ("Speed", rigidbody2D.velocity.magnitude);
	}
}
