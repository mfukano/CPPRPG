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
	private int SMGSlow=0;
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
		Inventory i = (Inventory)gameObject.GetComponent(typeof(Inventory));
		GUI.Label(new Rect(40, 10, 100, 20), velMag.ToString());
		GUI.Label(new Rect(40, 30, 100, 20), i.inventory [0].name);
	}
	
	void Update() {
		Inventory inv = (Inventory)gameObject.GetComponent(typeof(Inventory));
		string currentGun = inv.inventory [0].name;
		velMag = rigidbody2D.velocity.magnitude;
		GameObject projectile;
		SMGSlow++;

		if (Input.GetMouseButtonDown (1)) {
			anim.SetTrigger ("Attack");
		}
		if (Input.GetMouseButtonDown (0) && Time.timeScale == 1 && currentGun != null) {
			switch(currentGun) {
			case("Laser_Pistol"):
				projectile = Instantiate(Resources.Load("Prefabs/Laser"), transform.position, transform.rotation) as GameObject;
				projectile.rigidbody2D.AddForce (projectile.transform.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
					//projectile.rigidbody2D.AddForce (bullet.transform.up * bulletSpeed);
				break;
			case("SMG"):
				break;
			default:
				projectile = Instantiate(Resources.Load("Prefabs/bullet"), transform.position, transform.rotation) as GameObject;
				projectile.rigidbody2D.AddForce (projectile.transform.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
					//projectile.rigidbody2D.AddForce (bullet.transform.up * bulletSpeed);
				break;
			}
		}
		if (Input.GetMouseButton (0) && Time.timeScale == 1 && currentGun == "SMG" && SMGSlow%10 == 0) {

			projectile = Instantiate(Resources.Load("Prefabs/bullet"), transform.position, transform.rotation) as GameObject;
			projectile.rigidbody2D.AddForce (projectile.transform.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
			//projectile.rigidbody2D.AddForce (bullet.transform.up * bulletSpeed);
		}
		float input = Input.GetAxisRaw ("Vertical");
		anim.SetFloat ("Speed", rigidbody2D.velocity.magnitude);
	}
}
