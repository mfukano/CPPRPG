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
	private string gun;
	private int shootRate;
	private int shootCount;
	Animator anim;

	// AI
	public Enemy_AI_Ranged myAI;
	
	void Start() {
		anim = GetComponent<Animator>();
		startHealth = maxHealth;
		currentHealth = startHealth;
		isDead = false;

		gun = "Rocket Launcher";
		shootCount = 0;
		shootRate = 75;

		// Create AI
		// myAI = new Enemy_AI_Ranged ();
		// myAI.Owner = this;
	}
	
	void FixedUpdate() {
		rigidbody2D.angularVelocity = 0;
	}
	
	public void takeDamage (float dmg_val) {
		currentHealth -= dmg_val;
		if (currentHealth <= 0) {
			Death();
		}
	}
	
	void Death(){
		isDead = true;
		enemySpeed = 0;
		Destroy (gameObject);
		
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Bullet") {
			takeDamage(200);
			Destroy(col.gameObject);
		}
		if (col.gameObject.tag == "Sword") {
			takeDamage(350);
			//Destroy(col.gameObject);
		}
	}

	
	void Update() {
		shootCount++;
		if (Time.timeScale == 1 && shootCount % shootRate == 0) {
			Projectile prj = GetComponentInChildren<Projectile> ();
			prj.ShootGun (gun);
		}

	} 
}


