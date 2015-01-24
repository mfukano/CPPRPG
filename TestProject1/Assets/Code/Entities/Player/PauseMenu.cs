using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public float buttonWidth = 0.5f;
	public float buttonHeight = 0.12f;
	public float pos = 0.25f;
	bool paused = false;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		Time.timeScale = 1;
	}

	void OnGUI() {
		if (paused) {
			if (GUI.Button (new Rect (Screen.width*pos, Screen.height*pos, Screen.width*buttonWidth, Screen.height*buttonHeight), "Exit to Main Menu")){
				Application.LoadLevel("MainMenu");
			}
			if (GUI.Button (new Rect (Screen.width*pos, Screen.height*2*pos, Screen.width*buttonWidth, Screen.height*buttonHeight), "Exit to Desktop")) {
				Application.Quit();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			paused = togglePause();
		}
	}

	bool togglePause() {
		if (Time.timeScale == 0) {
			Screen.lockCursor = true;
			Time.timeScale = 1;
			return false;
		} else {
			Screen.lockCursor = false;
			Time.timeScale = 0;
			return true;
		}
	}


}
