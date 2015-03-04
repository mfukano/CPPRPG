using UnityEngine;
using System.Collections;

public class Missile : Projectile {
	void Start () {
		transform.rigidbody2D.AddForce (transform.up * speed);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Enemy")
		{
			//this.DoDamage()?
			Destroy (gameObject);
		}
	}
	
}
