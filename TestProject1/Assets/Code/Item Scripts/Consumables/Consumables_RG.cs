using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Consumables_RG : Item {
	//consumables attributes
	public float restoreValue;
	
	//extra necessay variables
	private float pHealth;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		//rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		//access the players health
		GameObject p = GameObject.Find("Player_Character");
		Player eScript = p.GetComponent<Player>();
		//pHealth = eScript.playerHealth;
		//database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDB>();
		//inventory.Add (database.items[0]);
	}

	//access player's health for health and use restoreValue for value
	void restorePlayerHealth(float value, float health){
		health += value;
	}
	
	void pickUpItem(){
		//add the item into your inventory	
	}

	public override float getRestore() {
			return this.restoreValue;
	}
	
}
