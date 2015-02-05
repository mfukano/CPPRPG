using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDB : MonoBehaviour {
	public List<Item> items = new List<Item>();

	void Start() {
		items.Add (new Item ("Bat", 0, 3, Item.ItemType.Equipment));
		items.Add (new Item ("Pear", 1, 5, Item.ItemType.Consumable));
	}
}
