using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public float maxHealth = 1000;
	public float startHealth = 800;
	public float maxEnergy = 100;

	public float startEnergy = 100;
	public float currentHealth, currentEnergy;
	
	string currentGun;
	public float playerSpeed;

	public float bulletSpeed;
	public bool bulletInitVel;

	public Inventory inv;

	private float velMag;
	public bool isDead;
	private int SMGSlow=0;
	Animator anim;

	Vector2 velocity;
	float horiz;
	float vert;

	private bool outOfAmmo = false;
	public GUIStyle style;

	void Start() {
		anim = GetComponent<Animator>();
		currentHealth = startHealth;
		#if UNITY_EDITOR
		style.normal.textColor = Color.red;
		style.fontSize = 30;
		#endif
	}

	void OnGUI() {
		if (outOfAmmo) {
			StartCoroutine(NotEnoughAmmo());
		}
	}

	void FixedUpdate() {
		horiz = Input.GetAxisRaw ("Horizontal");
		vert = Input.GetAxisRaw ("Vertical");
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePos - transform.position);

		velocity = new Vector2 (Mathf.Lerp (0, horiz * playerSpeed, 0.8f),
			                                Mathf.Lerp (0, vert * playerSpeed, 0.8f));
		if ((horiz != 0) || (vert != 0)) {
			velocity.Normalize ();
		}
		velocity *= playerSpeed;
		rigidbody2D.velocity = velocity;
		rigidbody2D.angularVelocity = 0;
//		Debug.Log (velocity);

	}

	IEnumerator NotEnoughAmmo () {
		#if UNITY_EDITOR
		GUI.Label (new Rect(Screen.width/2 - 150, Screen.height/2 - 150, 300, 300), "Not enough ammo!", style);
		yield return new WaitForSeconds (1);
		outOfAmmo = false;
		#endif
	}

	public void healDamage (float heal_val) {
		if (currentHealth != maxHealth) {
			currentHealth += heal_val;
		}
	}

	public void takeDamage (float dmg_val) {
		this.currentHealth -= dmg_val;

		if (currentHealth <= 0) {
			currentHealth = 0;
			isDead = true;
				}
		}

	public void OnSceneChange(){
		rigidbody2D.velocity = new Vector2 (0, 0);
		anim.SetFloat ("Speed", rigidbody2D.velocity.magnitude);
		Debug.Log (rigidbody2D.velocity.magnitude);
		ResetVelocity ();
	}

	public void ResetVelocity(){
		velocity = new Vector2 (Mathf.Lerp (0, horiz * playerSpeed, 0.8f),
		                        Mathf.Lerp (0, vert * playerSpeed, 0.8f));
		if ((horiz != 0) || (vert != 0)) {
			velocity.Normalize ();
		}
		velocity *= playerSpeed;
		rigidbody2D.velocity = velocity;
		rigidbody2D.angularVelocity = 0;
		}
	
	void Update() {
		inv = (Inventory)gameObject.GetComponent (typeof(Inventory));
		SMGSlow++;

		if (currentHealth <= 0) {
				currentHealth = 0;
				isDead = true;
			}

		if (inv.inventory[0]!= null){
		    	if(inv.inventory[0].itemName != null){
						currentGun = inv.inventory [0].itemName;
			}
		}

		anim.SetFloat ("Speed", rigidbody2D.velocity.magnitude);

		if (Input.GetMouseButtonDown (0) && Time.timeScale == 1 && currentGun != null) {
			if (inv.inventory [0].isMelee) {
				anim.SetTrigger (currentGun);
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

	public void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Ammo") {
			Inventory inv = (Inventory)GetComponent (typeof(Inventory));
			inv.ammoCount += Random.Range (20, 100);
			Destroy (col.gameObject);
		}
	}

	/*public void DamagePlayer(int damage)
	{
		takeDamage ();
	}*/
}
