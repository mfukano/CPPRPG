using UnityEngine;
using System.Collections;

public class Exit_World_Script : MonoBehaviour {

	public Player player_0;

	void Start() {
		player_0 = (Player)GameObject.FindObjectOfType (typeof(Player));
	}
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D unit) {
		if (unit.gameObject.tag == "Player"){
			Application.LoadLevel("Aridae");
		}
	}
}
