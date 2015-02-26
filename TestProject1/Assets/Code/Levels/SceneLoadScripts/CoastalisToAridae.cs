using UnityEngine;
using System.Collections;

public class CoastalisToAridae : MonoBehaviour {
	
	public Player player_0;
	float newX = 1880;
	float newY = PlayerPrefs.GetFloat ("PlayerX");
	float newZ = PlayerPrefs.GetFloat ("PlayerZ");
	
	void Start() {
		player_0 = (Player)GameObject.FindObjectOfType (typeof(Player));
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D unit) {
		if (unit.gameObject.tag == "Player"){
			DontDestroyOnLoad (player_0);
			Application.LoadLevel("Aridae_Final");
			player_0.transform.position = new Vector3(newX, newY, newZ);
		}
	}
}

