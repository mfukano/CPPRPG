using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public List<Item> inventory = new List<Item>();
	private ItemDB database;
	private bool paused = false;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDB>();
		//inventory.Add (database.items[0]);
	}

	void OnGUI () {
		if (paused) {
			for (int i=0; i<inventory.Count; i++) {
				GUI.Label (new Rect (10, i*20, 200, 50), inventory[i].itemName);
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
