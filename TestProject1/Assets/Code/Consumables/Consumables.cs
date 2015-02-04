using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Consumables : MonoBehaviour {
	public float restoreValue;
	private string labelText = "Press E to pickup";
	public float cWeight;
	private bool Highlighted;
	private float pHealth;
	// Use this for initialization
	void Start () {
		rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		//access the players health
		GameObject p = GameObject.Find("Player_Character");
		Player eScript = p.GetComponent<Player>();
		pHealth = eScript.playerHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//access player's health for health and use restoreValue for value
	void restorePlayerHealth(float value, float health){
		health += value;
	}

	//for collision of item with player, after collision pick up item, now it destroys the item
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {

			Destroy(gameObject);
		}
	}


}
