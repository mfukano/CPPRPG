using UnityEngine;
using System;
using System.Collections;

public class ShotgunPellet : Projectile {

	void Start () {
		transform.rigidbody2D.AddForce (transform.up * speed);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Enemy" ||
		    col.gameObject.tag == "Bullet")
		{
			bool WasShotgun = false;
			try
			{
				col.gameObject.GetComponent<ShotgunPellet>();
				WasShotgun = true;
			} catch (Exception e){
				WasShotgun = false;
			}
			// If we collided with another shotgun bullet. Do nothing
			if (!WasShotgun)
			{
				Destroy (gameObject);
			}
		} else if (col.gameObject.tag == "Player")
		{
			Player player = col.gameObject.GetComponent<Player>();
			player.takeDamage((float)this.dmg); //TODO: Change this number
			Destroy (gameObject);
		}
	}
}
