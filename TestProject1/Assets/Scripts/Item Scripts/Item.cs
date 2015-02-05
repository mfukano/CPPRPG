using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {
	public string itemName;
	public int itemID;
	public int itemWeight;
	public Texture2D itemIcon;
	public ItemType itemType;

	public enum ItemType {
		Equipment,
		Consumable
	}

	public Item(string name, int ID, int weight, ItemType type) {
		itemName = name;
		itemID = ID;
		itemWeight = weight;
		itemIcon = Resources.Load<Texture2D> ("Item_Sprites/" + name);
		itemType = type;
	}
}
