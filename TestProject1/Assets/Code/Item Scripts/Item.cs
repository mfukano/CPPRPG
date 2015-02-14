using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {
	// things that all items have
	public string itemName;
	public int itemID;
	public int itemWeight;
	public Texture2D itemIcon;

	// other stuff
	private string labelText = "Press E to pickup";
	private bool Highlighted;
	private bool getRidOfIt = false;
	private bool pickUp = false;
	private GameObject player = null;

	public Item() { }

	public Item(string name, int ID, int weight) {
		itemName = name;
		itemID = ID;
		itemWeight = weight;
		itemIcon = Resources.Load<Texture2D> ("Item_Sprites/" + name);
	}

	public void OnGUI(){
		if(Highlighted == true){
			//GUI.Box(Rect(140,Screen.height-50,Screen.width-300,120),(labelText));
			GUI.Box (new Rect (680, 40, 200, 50), labelText);
		}
	}

	// Update is called once per frame
	public void Update () {
		if (pickUp == true) {						
			if (Input.GetKeyUp (KeyCode.E)) {
				//inventory.Add (database.items[1]);
				Highlighted = false;
				getRidOfIt = true;
				pickUp = false;

			}
		}
		if (getRidOfIt == true) {
			// add to inventory
			addToInventory ();
			//Destroy(gameObject);
			gameObject.SetActive(false);
			getRidOfIt = false;
		}
	}

	//for collision of item with player, after collision pick up item, now it destroys the item
	public void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			player = col.gameObject;
			Highlighted = true;
			pickUp = true;
		}
	}
	
	public void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			Highlighted = false;
			pickUp = false;
		}
	}

	public void addToInventory() {
		Inventory i = (Inventory)player.GetComponent(typeof(Inventory));
		int count = i.inventory.Count;
		for (int k = 0; k < count; k++) {
			if (i.inventory[k].itemName == null) {
				i.inventory[k] = this;
				break;
			}
		}
	}

	public virtual float getRestore() {
		return 0;
	}

	public virtual float getDamage() {
		return 0;
	}



}
