using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Consumables : Item {
	//consumables attributes
	public float restoreValue;
	
	//extra necessay variables
	private float pHealth;
	
	// Use this for initialization
	void Start () {
		//access the players health
		GameObject p = GameObject.Find("Player_Character");
		Player eScript = p.GetComponent<Player>();
		//pHealth = eScript.playerHealth;
	}

	//access player's health for health and use restoreValue for value
	void restorePlayerHealth(float value, float health){
		health += value;
	}

	public override float getRestore() {
			return this.restoreValue;
	}
	
}
