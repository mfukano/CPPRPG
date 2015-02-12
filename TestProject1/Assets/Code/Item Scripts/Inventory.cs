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
	private int prevIndex;
	private Item selectedItem;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		for (int i = 0; i < slotsX*slotsY; i++) {
			inventory.Add (new Item());
		}
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
		if (dragging) {
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 32, 32), selectedItem.itemIcon);
		}
	}

	void drawInventory() {
		Event e = Event.current;
		int i = 0;
		int count = inventory.Count;
		for (int y = 0; y < slotsY; y++) {
			for (int x = 0; x < slotsX; x++) {
				Rect slotRect = new Rect(x*32, y*32, 32, 32);
				GUI.Box (slotRect, "", skin.GetStyle("inventory_skin"));
				if(inventory[i].itemName != null) {
					GUI.DrawTexture(slotRect, inventory[i].itemIcon);
					if (slotRect.Contains(e.mousePosition)) {
						//show item stats on mouse hover
						if (inventory[i].itemName != null) {
							itemStats = createItemStats (inventory[i]);
							showItemStats = true;
						}
						// if the item is left clicked
						if (e.button == 0 && e.type == EventType.mouseDown && !dragging) {
							dragging = true;
							selectedItem = inventory[i];
							prevIndex = i;
							inventory[i] = new Item();
						}
						if (e.button == 0 && e.type == EventType.mouseUp && dragging) {
							dragging = false;
							inventory[prevIndex] = inventory[i];
							inventory[i] = selectedItem;
							selectedItem = null;
						}
					}
				} else {
					if (slotRect.Contains(e.mousePosition)) {
						if (e.button == 0 && e.type == EventType.mouseUp && dragging) {
							dragging = false;
							inventory[i] = selectedItem;
							selectedItem = null;
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
