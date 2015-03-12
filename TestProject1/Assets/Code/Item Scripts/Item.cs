using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item : MonoBehaviour {
	// things that all items have
	public string itemName;
	public int itemID;
	public int itemWeight;
	public Texture2D itemIcon;
	public Sprite itemSprite;
	public bool isMelee;

	// other stuff
	private string labelText = "Press E to pickup";
	protected bool Highlighted;
	private bool getRidOfIt = false;
	protected bool pickUp = false;
	protected GameObject player = null;
	private bool tooMuchWeight = false;

	public GUIStyle style2;

	void Start() {

	}

	// empty item constructor for initilization in the inventory
	public Item() { }

	// actual item constructor with contributes
	public Item(string name, int ID, int weight) {
		itemName = name;
		Debug.Log ("constructor itemName: " + itemName);
		Debug.Log ("constructor name: " + name);
		itemID = ID;
		itemWeight = weight;
		itemIcon = Resources.Load<Texture2D> ("Item_Sprites/" + name);
	}

	public void OnGUI(){
		if(Highlighted == true){
			//GUI.Box(Rect(140,Screen.height-50,Screen.width-300,120),(labelText));
			GUI.Box (new Rect (680, 40, 200, 50), labelText);
		}
		if (tooMuchWeight) {
			#if UNITY_EDITOR
			style2.normal.textColor = Color.red;
			style2.fontSize = 30;
			#endif
			StartCoroutine(TooMuchWeight());
		}
	}

	IEnumerator TooMuchWeight () {
		#if UNITY_EDITOR
		GUI.Label (new Rect(Screen.width/2 - 200, Screen.height/2 - 150, 300, 300), "Not enough space in your backpack!", style2);
		#endif
		yield return new WaitForSeconds (1);
		tooMuchWeight = false;
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
			getRidOfIt = false;
		}
	}

	//for collision of item with player, after collision pick up item, now it destroys the item
	public virtual void OnTriggerEnter2D(Collider2D col) {
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

	//get player inventory and add the item to it
	public void addToInventory() {
		Inventory inv = (Inventory)player.GetComponent(typeof(Inventory));
		if (inv.currWeight + this.itemWeight > inv.maxWeight) {
			tooMuchWeight = true;
			return;
		}
		int count = inv.inventory.Count;
		int k = 0;
		// put consumable items into inventory and not hand/holster
		if (this.getRestore() != 0) {
			k = 2;
		}
		for ( ; k < count; k++) {
			if (inv.inventory[k]==null){//.itemName == null) {
				// add the item to the inventory and increase current weight
				inv.inventory[k] = this;
				inv.currWeight += this.itemWeight;
				gameObject.SetActive(false);
				DontDestroyOnLoad (gameObject);
				break;
			}
		}
	}

	//placeholder functions which are overrode in Consumables and Equippables
	public virtual int getRestore() {
		return 0;
	}

	public virtual int getDamage() {
		return 0;
	}

	public virtual int getAmmoPerShot() {
		return 0;
	}



}
