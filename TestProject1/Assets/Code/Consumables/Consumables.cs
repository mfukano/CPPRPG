using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Consumables : MonoBehaviour {
    //main variables to change in side bar
	public float restoreValue;
	public float cWeight;

	//extra necessay variables
	private float pHealth;
	private string labelText = "Press E to pickup";
	private bool Highlighted;
	private bool getRidOfIt = false;
	private bool pickUp = false;

	//Getting inventory variables
	private Inventory inventory;
	private ItemDB database;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		//access the players health
		GameObject p = GameObject.Find("Player_Character");
		Player eScript = p.GetComponent<Player>();
		//pHealth = eScript.playerHealth;
		//database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDB>();
		//inventory.Add (database.items[0]);
	}

	void OnGUI(){
		if(Highlighted == true){
			//GUI.Box(Rect(140,Screen.height-50,Screen.width-300,120),(labelText));
			GUI.Box (new Rect (680, 40, 200, 50), labelText);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pickUp == true) {						
			if (Input.GetKeyUp (KeyCode.E)) {
			    //somehow add the current item/gameobject to the player's inventory
				Highlighted = false;
				getRidOfIt = true;
				pickUp = false;

			}
		}

		if (getRidOfIt == true) {
			Destroy(gameObject);
		}
	}

	//access player's health for health and use restoreValue for value
	void restorePlayerHealth(float value, float health){
		health += value;
	}

	//for collision of item with player, after collision pick up item, now it destroys the item
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			Highlighted = true;
			pickUp = true;
	
		}
	}

	void pickUpItem(){
		//add the item into your inventory

	}


}
