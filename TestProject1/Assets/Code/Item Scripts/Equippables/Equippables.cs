using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equippables : Item {
	// equippables attributes
	public int Damage;
	public int BulletSpeed;
	public int ReloadTimePerShot;
	
	// Use this for initialization
	void Start () {
		
	}

	public override int getDamage() {
		return this.Damage;
	}

	public override int getAmmoPerShot() {
		return 0;
	}

}
