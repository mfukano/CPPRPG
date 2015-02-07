using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();
	public int slotsX, slotsY;
	public GUISkin skin;
	private ItemDB database;
	private bool paused = false;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		//database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDB>();
		//inventory.Add (database.items[0]);
		/*for (int i = 0; i < (slotsX*slotsY); i++) {
			slots.Add (new Item());
		}*/
	}

	void OnGUI () {
		GUI.skin = skin;
		if (paused) {
			drawInventory();
			/*for (int i=0; i<inventory.Count; i++) {
				if (inventory[i].GetType() == typeof(Consumables_RG)) {
					GUI.Label (new Rect (10, i*20, 200, 50), inventory[i].itemName +" "+ inventory[i].getRestore());
				} else if (inventory[i].GetType() == typeof(Equippables_RG)) {
					GUI.Label (new Rect (10, i*20, 200, 50), inventory[i].itemName +" "+ inventory[i].getDamage());
				}
			}*/
		}
	}

	void drawInventory() {
		int i = 0;
		int count = inventory.Count;
		for (int y = 0; y < slotsY; y++) {
			for (int x = 0; x < slotsX; x++) {
				Rect slotRect = new Rect(x*32, y*32, 32, 32);
				GUI.Box (slotRect, "", skin.GetStyle("inventory_skin"));
				if(i < count) {
					GUI.DrawTexture(slotRect, inventory[i].itemIcon);
				}
				i++;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.I)) {
			paused = toggleInventory();
		}
	}

	bool toggleInventory() {
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
			return false;
		} else {
			Time.timeScale = 0;
			return true;
		}
	}
}
