using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equippables_RG : Item {
	// equippables attributes
	public float Damage;
	
	// Use this for initialization
	void Start () {
		rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		//database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDB>();
		
	}
	
}
