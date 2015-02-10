using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {

	public float bulletSpeed;
	// Use this for initialization
	void Start () {
		rigidbody2D.AddForce (Vector3.forward * bulletSpeed);
	}
}
