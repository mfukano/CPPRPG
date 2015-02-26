using UnityEngine;
using System.Collections;

public class OldToCoastalis : MonoBehaviour {
	
	public Player player_0;
	float newX, newY, newZ;
	
	void Start() {
		player_0 = (Player)GameObject.FindObjectOfType (typeof(Player));
		newX = PlayerPrefs.GetFloat ("PlayerX");
		newY = 1880;
		newZ = PlayerPrefs.GetFloat ("PlayerZ");
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D unit) {
		if (unit.gameObject.tag == "Player"){
			DontDestroyOnLoad (player_0);
			Application.LoadLevel("Coastalis_Final");
			player_0.transform.position = new Vector3(newX, newY, newZ);
		}
	}
}