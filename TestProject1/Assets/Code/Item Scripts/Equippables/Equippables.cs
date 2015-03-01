using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equippables : Item {
	// equippables attributes
	public float Damage;
	public float BulletSpeed;
	public float ReloadTimePerShot;
	
	// Use this for initialization
	void Start () {
		
	}

	public override float getDamage() {
		return this.Damage;
	}

	public override float getAmmoPerShot() {
		return 0;
	}

}
