using UnityEngine;
using System.Collections;

public class BorealToNorthern : MonoBehaviour {
	
	public Player player_0;
	float newX, newY, newZ;
	
	void Start() {
		player_0 = (Player)GameObject.FindObjectOfType (typeof(Player));
		newX = -1880;
		newY = PlayerPrefs.GetFloat ("PlayerY");
		newZ = PlayerPrefs.GetFloat ("PlayerZ");
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D unit) {
		if (unit.gameObject.tag == "Player"){
			DontDestroyOnLoad (player_0);
			PlayerSpeed playerspeed = player_0.gameObject.GetComponent<PlayerSpeed>();
			playerspeed.SetWalk ();
			player_0.OnSceneChange();
			Application.LoadLevel("NorthernWastes");
			player_0.transform.position = new Vector3(newX, newY, newZ);
		}
	}
}

