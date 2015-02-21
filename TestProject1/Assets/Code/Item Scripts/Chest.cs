using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {

	public Sprite closedChest, openedChest; // Sprites
	private SpriteRenderer spriteRenderer;
	public GameObject prefab;

	private string labelText = "Press E to open chest";
	private bool Highlighted;
	private bool openChest;
	private bool empty = false;

	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		GameObject prefab = Resources.Load ("Apple.prefab") as GameObject;
		//GameObject go = (GameObject)Instantiate(Resources.Load("Apple.prefab")); 
		//GameObject testPrefab = (GameObject.Instantiate (Resources.LoadAssetAtPath ("Assets/Resources/Prefabs/Items/Food/Apple.prefab")));
		//object[] chestSprites = AssetDatabase.LoadAllAssetsAtPath ("Assets/Resources/chests.png");
	}

	public void OnGUI(){
		if(Highlighted == true){
			//GUI.Box(Rect(140,Screen.height-50,Screen.width-300,120),(labelText));
			GUI.Box (new Rect (680, 40, 200, 50), labelText);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(empty == false){
			if (openChest == true) {
				if (Input.GetKeyUp (KeyCode.E)) {
					if (spriteRenderer.sprite == closedChest) {
						spriteRenderer.sprite = openedChest;
					} else {
						spriteRenderer.sprite = closedChest;
					}
			    Instantiate(prefab,new Vector3(208, -150, 0), Quaternion.Euler(0, 180, 0));
				openChest = false;
					empty = true;
				}
			}	
		}
	
	}

	public void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			///player = col.gameObject;
			if(empty == false){
			Highlighted = true;
			}
			openChest = true;
		}
	}

	public void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			Highlighted = false;
			openChest = false; 
		}
	}

}
