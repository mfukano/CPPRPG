using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equippables : MonoBehaviour {
	public float eWeight;
	public float Damage;
	// Use this for initialization
	void Start () {
		rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
