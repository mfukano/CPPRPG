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

	public float bulletSpeed;
	public bool bulletInitVel;

	private float velMag;
	private bool isDead;
	private int SMGSlow=0;
	Animator anim;

	private bool outOfAmmo = false;

	void Start() {
		anim = GetComponent<Animator>();
		currentHealth = startHealth;
	}

	void OnGUI() {
		if (outOfAmmo) {
			StartCoroutine(NotEnoughAmmo());
		}
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

	public IEnumerator NotEnoughAmmo () {
		GUI.Label (new Rect(20, Screen.height - 128, 300, 48), "Not enough ammo!");
		yield return new WaitForSeconds (1);
		outOfAmmo = false;
	}

	public void healDamage (float heal_val) {
		if (currentHealth != maxHealth) {
			currentHealth += heal_val;
				}
	}


	public void takeDamage (float dmg_val) {
		currentHealth -= dmg_val;

		if (currentHealth <= 0) {
			Death();
				}
		}

	void Death(){
		isDead = true;
		playerSpeed = 0;
	}
	
	void Update() {
		Inventory inv = (Inventory)gameObject.GetComponent (typeof(Inventory));
		string currentGun = inv.inventory [0].itemName;
		SMGSlow++;

		anim.SetFloat ("Speed", rigidbody2D.velocity.magnitude);

		if (Input.GetMouseButtonDown (0) && Time.timeScale == 1 && currentGun != null) {
			if (inv.inventory [0].isMelee) {
				Texture2D texture = inv.inventory[0].itemIcon;
				MeleeWeapon mel = GetComponentInChildren<MeleeWeapon> ();
				Sprite mac = Resources.Load("Spritesheet/mace", typeof(Sprite)) as Sprite;

				if(mel != null)
				mel.GetComponent<SpriteRenderer>().sprite = mac;//////////////////////////////////////////
				anim.SetTrigger ("Attack");
			} else {
				Projectile prj = GetComponentInChildren<Projectile> ();
				if (inv.ammoCount >= (int)inv.inventory [0].getAmmoPerShot ()) {
					prj.ShootGun (currentGun);
					inv.ammoCount -= (int)inv.inventory [0].getAmmoPerShot ();
				} else {
					outOfAmmo = true;
				}
			}
		}

		if (Input.GetMouseButton (0) && Time.timeScale == 1 && currentGun == "SMG" && SMGSlow % 10 == 0) {
			if (inv.inventory [0].isMelee) {
			} else {
				Projectile prj = GetComponentInChildren<Projectile> ();
				if (inv.ammoCount >= (int)inv.inventory [0].getAmmoPerShot ()) {
					prj.ShootGun (currentGun);
					inv.ammoCount -= (int)inv.inventory [0].getAmmoPerShot ();
				} else {
					outOfAmmo = true;
				}
			}
		}
	}
}
