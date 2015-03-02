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
		velMag = rigidbody2D.velocity.magnitude;
		GameObject projectile;
		SMGSlow++;
		healthBar.value = currentHealth;
		energyBar.value = currentEnergy;


		if (Input.GetMouseButtonDown (1)) {
			anim.SetTrigger ("Attack");
		}
		if (Input.GetMouseButtonDown (0) && Time.timeScale == 1 && currentGun != null) {
			inv.ammoCount -= (int)inv.inventory[0].getAmmoPerShot();
			switch(currentGun) {
			case("Shotgun"):
				Transform sg1 = transform.Find("shotgun1");
				Transform sg2 = transform.Find("shotgun2");
				Transform sg3 = transform.Find("shotgun3");
				Transform sg4 = transform.Find("shotgun4");
				Transform sg5 = transform.Find("shotgun5");
				GameObject sg_bullet1, sg_bullet2, sg_bullet3, sg_bullet4, sg_bullet5;
				sg_bullet1 = Instantiate(Resources.Load("Prefabs/bullet"), sg1.position, sg1.rotation) as GameObject;
				sg_bullet1.rigidbody2D.AddForce (sg1.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
				sg_bullet2 = Instantiate(Resources.Load("Prefabs/bullet"), sg2.position, sg2.rotation) as GameObject;
				sg_bullet2.rigidbody2D.AddForce (sg2.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
				sg_bullet3 = Instantiate(Resources.Load("Prefabs/bullet"), sg3.position, sg3.rotation) as GameObject;
				sg_bullet3.rigidbody2D.AddForce (sg3.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
				sg_bullet4 = Instantiate(Resources.Load("Prefabs/bullet"), sg4.position, sg4.rotation) as GameObject;
				sg_bullet4.rigidbody2D.AddForce (sg4.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
				sg_bullet5 = Instantiate(Resources.Load("Prefabs/bullet"), sg5.position, sg5.rotation) as GameObject;
				sg_bullet5.rigidbody2D.AddForce (sg5.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
				break;
			case("Laser Pistol"):
				projectile = Instantiate(Resources.Load("Prefabs/Laser"), transform.position, transform.rotation) as GameObject;
				projectile.rigidbody2D.AddForce (projectile.transform.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
				break;
			case("Rocket Launcher"):
				projectile = Instantiate(Resources.Load("Prefabs/Missile"), transform.position, transform.rotation) as GameObject;
				projectile.rigidbody2D.AddForce (projectile.transform.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
				break;
			case("SMG"):
				break;
			default:
				projectile = Instantiate(Resources.Load("Prefabs/bullet"), transform.position, transform.rotation) as GameObject;
				projectile.rigidbody2D.AddForce (projectile.transform.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
				break;
			}
		}
		if (Input.GetMouseButton (0) && Time.timeScale == 1 && currentGun == "SMG" && SMGSlow%10 == 0) {
			inv.ammoCount -= (int)inv.inventory[0].getAmmoPerShot();
			projectile = Instantiate(Resources.Load("Prefabs/bullet"), transform.position, transform.rotation) as GameObject;
			projectile.rigidbody2D.AddForce (projectile.transform.up * (bulletSpeed + (playerSpeed * Input.GetAxis ("Vertical"))));
		}
		float input = Input.GetAxisRaw ("Vertical");
		anim.SetFloat ("Speed", rigidbody2D.velocity.magnitude);

		
		if (Input.GetKeyUp (KeyCode.Q)) {
			Item tmp = inv.inventory[0];
			inv.inventory[0] = inv.inventory[1];
			inv.inventory[1] = tmp;
		}
	}
}
