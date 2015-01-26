using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MainMenu : MonoBehaviour {
	public Texture backgroundTexture;
	public float buttonWidth = 0.5f;
	public float buttonHeight = 0.12f;
	public float pos = 0.25f;
	public GUIStyle style;

	void Start() {
		style.normal.textColor = Color.white;
		style.alignment = TextAnchor.UpperCenter;
		style.fontSize = 30;
	}

	void OnGUI(){
		//display background texture
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);
		GUI.Label (new Rect (Screen.width*pos, Screen.height*pos, Screen.width*buttonWidth, Screen.height*buttonHeight), "Not Zombies", style);
		//display menu buttons
		if (GUI.Button (new Rect (Screen.width*pos, Screen.height*2*pos, Screen.width*buttonWidth, Screen.height*buttonHeight), "New Game")){
			Application.LoadLevel("TestScene1");
		}
		if (GUI.Button (new Rect (Screen.width*pos, Screen.height*3*pos, Screen.width*buttonWidth, Screen.height*buttonHeight), "Quit")) {
			Application.Quit();
		}
	}
}
