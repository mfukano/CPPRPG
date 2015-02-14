using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {
	//inventory stuff
	public List<Item> inventory = new List<Item>();
	public int slotsX = 5;
	public int slotsY = 5;
	public GUISkin skin, hand, holster;
	private int handItem = 0;
	private int pocketItem = 1;

	// misc
	private bool paused = false;
	private bool showItemStats = false;
	private string itemStats;
	public GUIStyle style;


	//will be used for rearranging items
	private bool dragging;
	private int prevIndex;
	private Item selectedItem;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		for (int i = 0; i < (slotsX*slotsY)+2; i++) {
			inventory.Add (new Item());
		}
		style.normal.textColor = Color.white;
		style.fontSize = 15;
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
		int boxSize = 32;
		Rect slotRect;
		// inventory section names
		GUI.Label (new Rect (((Screen.width / 2) - 80), ((Screen.height / 2) - 142), 500, 500), "Hand", style);
		GUI.Label (new Rect (((Screen.width / 2) - 20), ((Screen.height / 2) - 142), 500, 500), "Holster", style);
		GUI.Label (new Rect (((Screen.width / 2) - 80), ((Screen.height / 2) - 70), 500, 500), "Backpack", style);
		// add row for hand and holster items
		for (int y = 0; y < slotsY+1; y++) {
			for (int x = 0; x < slotsX; x++) {
				if (x == handItem && y == 0) {
					slotRect = new Rect( ((Screen.width/2)-80)+(x*boxSize), ((Screen.height/2)-112)+(y*boxSize), boxSize, boxSize);
					GUI.Box (slotRect, "", hand.GetStyle("inventory_hand_skin"));
					//slotRect = new Rect( ((Screen.width/2)-80)+(x*boxSize), ((Screen.height/2)-80)+(y*boxSize), boxSize, boxSize);
				} else if (x == pocketItem && y == 0) {
					slotRect = new Rect( ((Screen.width/2)-48)+(x*boxSize), ((Screen.height/2)-112)+(y*boxSize), boxSize, boxSize);
					GUI.Box (slotRect, "", holster.GetStyle("inventory_holster_skin"));
					//slotRect = new Rect( ((Screen.width/2)-80)+(x*boxSize), ((Screen.height/2)-80)+(y*boxSize), boxSize, boxSize);
				} else if (y != 0){
					slotRect = new Rect( ((Screen.width/2)-80)+(x*boxSize), ((Screen.height/2)-80)+(y*boxSize), boxSize, boxSize);
					GUI.Box (slotRect, "", skin.GetStyle("inventory_skin"));
				} else {
					continue;
				}

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
