﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NorthernToBoreal : MonoBehaviour {
	
	public Player player_0;
	float newX, newY, newZ;
	
	void Start() {
		player_0 = (Player)GameObject.FindObjectOfType (typeof(Player));
		newX = 1880;
		newY = PlayerPrefs.GetFloat ("PlayerY");
		newZ = PlayerPrefs.GetFloat ("PlayerZ");
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D unit) {
		if (unit.gameObject.tag == "Player"){
			DontDestroyOnLoad (player_0);
			Application.LoadLevel("Boreal");
			player_0.transform.position = new Vector3(newX, newY, newZ);
		}
	}
}

