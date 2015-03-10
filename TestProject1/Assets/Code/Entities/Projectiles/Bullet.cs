using UnityEngine;
using System.Collections;

public class Bullet : Projectile {
	void Start () {
		transform.rigidbody2D.AddForce (transform.up * speed);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Enemy")
		{
			//this.DoDamage()?
			Destroy (gameObject);
		} else if (col.gameObject.tag == "Player")
		{
			Player player = col.gameObject.GetComponent<Player>();
			player.DamagePlayer((int)this.dmg); //TODO: Change this number
			Destroy (gameObject);
		}
	}

}
