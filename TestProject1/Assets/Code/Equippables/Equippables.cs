using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equippables : MonoBehaviour {
	public float eWeight;
	public float Damage;

	private string labelText = "Press E to pickup";
	private bool Highlighted;
	private bool getRidOfIt = false;
	private bool pickUp = false;

	//Getting inventory variables
	public List<Item> inventory;
	private ItemDB database;

	// Use this for initialization
	void Start () {
		rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		//database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDB>();
	
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
				//inventory.Add (database.items[1]);
				Highlighted = false;
				getRidOfIt = true;
				pickUp = false;
				
			}
		}
		
		if (getRidOfIt == true) {
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			Highlighted = true;
			pickUp = true;
			
		}
	}

}
