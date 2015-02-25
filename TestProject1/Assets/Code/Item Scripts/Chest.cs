using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour {

	public Sprite closedChest, openedChest; // Sprites
	private SpriteRenderer spriteRenderer;
	public static GameObject[] myObjects;
	public static int numSpawned = 0;
	public static int numToSpawn = 14;

	private string labelText = "Press E to open chest";
	private bool Highlighted;
	private bool openChest;
	private bool empty = false;

	// Use this for initialization
	void Start () {
		myObjects = Resources.LoadAll<GameObject>("Prefabs/Items/Food");
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}

	public void OnGUI(){
		if(Highlighted == true){
			//GUI.Box(Rect(140,Screen.height-50,Screen.width-300,120),(labelText));
			GUI.Box (new Rect (Screen.width/2, 40, 200, 50), labelText);
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
				spawnObjects();
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


	Vector3 ranCircle ( Vector3 center ,   float radius  ){
		float ang = Random.value * 360;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.z = center.z + 5;
		return pos;
	}


	public void spawnObjects(){
		for (int i = 0; i < 3; i++) {
			Vector3 center = transform.position;
			Vector3 pos = ranCircle (center, 50.0f);
			int prefabIndex = UnityEngine.Random.Range (0, 13);
			Instantiate (myObjects [prefabIndex], pos, Quaternion.Euler(0, 180, 0));
		}
	}
     
}
