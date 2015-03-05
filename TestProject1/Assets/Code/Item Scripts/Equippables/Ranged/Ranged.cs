using UnityEngine;
using System.Collections;

public class Ranged : Equippables {
	public int maxRange;
	public int AmmoPerShot;
	// Use this for initialization
	void Start () {
	
	}

	public override int getAmmoPerShot() {
		return this.AmmoPerShot;
	}

}
