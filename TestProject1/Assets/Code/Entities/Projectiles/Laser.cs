using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Enemy") {
			Destroy (gameObject);
		}
	}
	
}
