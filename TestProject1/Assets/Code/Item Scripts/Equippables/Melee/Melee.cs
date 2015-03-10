using UnityEngine;
using System.Collections;

public class Melee : Equippables {

	public override void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Enemy") {
			col.gameObject.GetComponent<Enemy>().takeDamage(this.Damage);
		}
		if (col.gameObject.tag == "Player") {
			player = col.gameObject;
			Highlighted = true;
			pickUp = true;
		}
	}

}
