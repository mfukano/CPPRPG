using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TestMenu : MonoBehaviour {
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
		GUI.Label (new Rect (Screen.width*pos, Screen.height*pos, Screen.width*buttonWidth, Screen.height*buttonHeight), "Test Suite", style);
		//display menu buttons
		if (GUI.Button (new Rect (Screen.width*pos, (float)Screen.height*1.5F*pos, Screen.width*buttonWidth, (float)Screen.height*0.5F*buttonHeight), "Low Health Runner")){
			Application.LoadLevel("LowHealthRunner");
		}
		if (GUI.Button (new Rect (Screen.width * pos, (float)Screen.height*2.0F* pos, Screen.width*buttonWidth, (float)Screen.height*0.5F*buttonHeight), "Day Night Magic")) {
			Application.LoadLevel ("Day_Night_Magic");
		}
		if (GUI.Button (new Rect (Screen.width*pos, (float)Screen.height*2.5F*pos, Screen.width*buttonWidth, (float)Screen.height*0.5F*buttonHeight), "Mapper Quest")) {
			Application.LoadLevel ("MapperQuest1");
		}
		if (GUI.Button (new Rect (Screen.width*pos, (float)Screen.height*3.0F*pos, Screen.width*buttonWidth, (float)Screen.height*0.5F*buttonHeight), "Back To Main Menu")) {
			Application.LoadLevel ("MainMenu");
		}
	}
}