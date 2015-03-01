using UnityEngine;
using System.Collections;

public class Ranged : Equippables {
	public float maxRange;
	public float AmmoPerShot;
	// Use this for initialization
	void Start () {
	
	}
	
	public override float getAmmoPerShot() {
		return this.AmmoPerShot;
	}

}
