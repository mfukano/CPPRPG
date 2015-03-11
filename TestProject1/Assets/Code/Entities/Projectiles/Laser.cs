using UnityEngine;
using System.Collections;

public class Laser : Projectile {
	void Start () {
		transform.rigidbody2D.AddForce (transform.up * speed);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Enemy" ||
		    col.gameObject.tag == "Bullet")
		{
			//this.DoDamage()?
			Destroy (gameObject);
		}
	}
	
}
