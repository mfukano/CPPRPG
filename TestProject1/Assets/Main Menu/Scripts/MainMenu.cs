﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MainMenu : MonoBehaviour {
	public Texture backgroundTexture;
	public float buttonWidth = 0.5f;
	public float buttonHeight = 0.12f;
	public float pos = 0.25f;

	void OnGUI(){
		//display background texture
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

		//display menu buttons
		if (GUI.Button (new Rect (Screen.width*pos, Screen.height*pos, Screen.width*buttonWidth, Screen.height*buttonHeight), "New Game")){
			Application.LoadLevel("TestArea");
		}
		if (GUI.Button (new Rect (Screen.width*pos, Screen.height*2*pos, Screen.width*buttonWidth, Screen.height*buttonHeight), "Quit")) {
			Application.Quit();
		}
	}
}
