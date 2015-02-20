using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public float maxHealth = 1000;
	public float startHealth;
	public float currentHealth;
	
	public float enemySpeed;
	private float velMag;
	private bool isDead;
	Animator anim;

	// AI
	public Enemy_AI_Ranged myAI;
	
	void Start() {
		anim = GetComponent<Animator>();
		startHealth = maxHealth;
		currentHealth = startHealth;
		isDead = false;

		// Create AI
		myAI = new Enemy_AI_Ranged ();
		myAI.Owner = this;
	}
	
	void FixedUpdate() {

	}
	
	public void takeDamage (float dmg_val) {
		currentHealth -= dmg_val;
		
		if (currentHealth <= 0) {
			Death();
		}
	}
	
	void Death(){
		//TODO: Remove charater here
		isDead = true;
		enemySpeed = 0;
		
	}

	
/*	void Update() {
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
	} */
}


