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
	private bool canShoot;
	Animator anim;

	public Quaternion defaultRotaton;

	// AI
	public Enemy_AI_Movement myAI;
	
	void Start() {
		anim = GetComponent<Animator>();
		myAI = GetComponent<Enemy_AI_Movement> ();
		startHealth = maxHealth;
		currentHealth = startHealth;
		isDead = false;
		canShoot = false;

		gun = "Pistol";
		shootCount = 0;
		shootRate = 75;

		// Create AI
		// myAI = new Enemy_AI_Ranged ();
		// myAI.Owner = this;
		defaultRotaton = this.transform.rotation;
		this.transform.rotation = defaultRotaton;
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

	void OnDestroy() {
		GameObject ammo = Instantiate(Resources.Load("Prefabs/Items/ammo"), transform.position, transform.rotation) as GameObject;
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

		// While not paused
		if (Time.timeScale == 1) 
		{
			anim.SetFloat ("Speed", rigidbody2D.velocity.magnitude);
			if (canShoot)
			{
				return;
			}

			if (shootCount % shootRate == 0)
			{
				canShoot = true;
				return;
			}
			shootCount++;
		}
//		if (Time.timeScale == 1 && shootCount % shootRate == 0) {
//			Projectile prj = GetComponentInChildren<Projectile> ();
//			prj.ShootGun (gun);
//		}

	}

	public void Shoot()
	{
		if (canShoot)
		{
			Projectile proj = GetComponentInChildren<Projectile> ();
			proj.ShootGun(gun);
			canShoot = false;
			shootCount = 1;
		}
	}
}


