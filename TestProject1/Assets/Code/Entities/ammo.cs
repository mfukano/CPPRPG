using UnityEngine;
using System.Collections;

public class ammo : MonoBehaviour {


	public void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			Inventory inv = (Inventory)GetComponent (typeof(Inventory));
			inv.ammoCount += Random.Range (20, 100);
			Destroy (gameObject);
		}
	}

}
