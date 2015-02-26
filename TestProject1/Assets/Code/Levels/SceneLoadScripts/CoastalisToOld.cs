using UnityEngine;
using System.Collections;

public class CoastalisToOld : MonoBehaviour {
	
	public Player player_0;
	float newX = PlayerPrefs.GetFloat ("PlayerX");
	float newY = -1880;
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
			Application.LoadLevel("Old_Coastalis_final");
			player_0.transform.position = new Vector3(newX, newY, newZ);
		}
	}
}