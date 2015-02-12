using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();
	public int slotsX, slotsY;
	public GUISkin skin;

	private bool paused = false;
	private bool showItemStats = false;
	private string itemStats;

	//will be used for rearranging items
	private bool dragging;
	private Item selectedItem;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}

	void OnGUI () {
		itemStats = "";
		GUI.skin = skin;
		if (paused) {
			drawInventory();
			if (showItemStats) {
				GUI.Box (new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 75, 75), itemStats);
			}
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
					if (slotRect.Contains(Event.current.mousePosition)) {
						itemStats = createItemStats (inventory[i]);
						showItemStats = true;
						// if the item is left clicked
						if (Event.current.button == 0) {
							dragging = true;
							selectedItem = inventory[i];
						}
					}
				}
				if (itemStats == "") {
					showItemStats = false;
				}
				i++;
			}
		}
	}

	//constructs a string of item stats to display
	string createItemStats (Item item) {
		string stats = "<color=#ffffff>" + item.itemName + "\nWgt: " + item.itemWeight + "</color>";
		if (item.getRestore() == 0) {
			stats = stats + "<color=#ff7a88>\nDamage: " + item.getDamage() + "</color>";
		} else {
			stats = stats + "<color=#15ff00>\nRestore: " + item.getRestore() + "</color>";
		}
		return stats;
	}

	void removeFromInventory() {
		for (int i=0; i<inventory.Count; i++) {
			inventory[i] = new Item();
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.I)) {
			paused = toggleInventory();
		}
		if (Input.GetKeyUp (KeyCode.R)) {
			Debug.Log ("R pressed");
			removeFromInventory();
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
