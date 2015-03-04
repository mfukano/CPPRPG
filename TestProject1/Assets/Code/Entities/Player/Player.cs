using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxHealth = 1000;
	public float startHealth = 800;
	public float maxEnergy = 100;

	public float startEnergy = 100;
	public float currentHealth, currentEnergy;

	public float playerSpeed;

	public Slider healthBar;
	public Slider energyBar;

	public float bulletSpeed;
	public bool bulletInitVel;

	private float velMag;
	private bool isDead;
	private int SMGSlow=0;
	Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
		currentHealth = startHealth;
		//currentEnergy = startEnergy;
		healthBar.value = currentHealth;
		energyBar.value = currentEnergy;
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
		//GUI.Label(new Rect(40, 10, 100, 20), velMag.ToString());
		//GUI.Label(new Rect(40, 30, 100, 20), i.inventory [0].itemName);
	}
	
	void Update() {
		Inventory inv = (Inventory)gameObject.GetComponent(typeof(Inventory));
		string currentGun = inv.inventory [0].itemName;
		SMGSlow++;
		healthBar.value = currentHealth;
		energyBar.value = currentEnergy;

		anim.SetFloat ("Speed", rigidbody2D.velocity.magnitude);

		if (Input.GetMouseButtonDown (1)) {
			anim.SetTrigger ("Attack");
		}

		if (Input.GetMouseButtonDown (0) && Time.timeScale == 1 && currentGun != null) {
			inv.ammoCount -= (int)inv.inventory [0].getAmmoPerShot ();
			Projectile prj = GetComponentInChildren<Projectile> ();
			prj.ShootGun (currentGun);
		}

		if (Input.GetMouseButton (0) && Time.timeScale == 1 && currentGun == "SMG" && SMGSlow%10 == 0) {
			inv.ammoCount -= (int)inv.inventory [0].getAmmoPerShot ();
			Projectile prj = GetComponentInChildren<Projectile> ();
			prj.ShootGun (currentGun);
		}
	}
}
