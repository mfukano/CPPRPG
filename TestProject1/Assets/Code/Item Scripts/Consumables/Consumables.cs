using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Consumables : Item {
	//consumables attributes
	public int restoreValue;
	
	//extra necessay variables
	private int pHealth;
	
	// Use this for initialization
	void Start () {
		//access the players health
		GameObject p = GameObject.Find("Player_Character");
		Player eScript = p.GetComponent<Player>();
		//pHealth = eScript.playerHealth;
	}

	//access player's health for health and use restoreValue for value
	void restorePlayerHealth(int value, int health){
		health += value;
	}

	public override int getRestore() {
			return this.restoreValue;
	}
	
}
