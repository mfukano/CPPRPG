using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadPlayerStats : MonoBehaviour
{
	public Slider healthBar;
	public Slider energyBar;
	private Player myPlayer;
	public GUIStyle style;
	// Use this for initialization
	void Start() {
		myPlayer = (Player)GameObject.FindObjectOfType(typeof(Player));
		healthBar.value = myPlayer.currentHealth;
		energyBar.value = myPlayer.currentEnergy;
		style.normal.textColor = new Color(0.8F, 0, 0.3F, 0.95F);
		style.alignment = TextAnchor.UpperCenter;
		style.fontSize = 30;
	}

	IEnumerator Death(){
		GUI.TextField (new Rect(Screen.width*0.25F,Screen.height*0.25F, Screen.width*0.5F,Screen.height*0.25F), "You died.", style);
		myPlayer.gameObject.SetActive (false);
		yield return new WaitForSeconds (3);
		Application.LoadLevel ("MainMenu");
	}

	void OnGUI(){
				if (myPlayer.isDead) {
						StartCoroutine (Death ());
				}
		}

	void Update() {
				healthBar.value = myPlayer.currentHealth;
				energyBar.value = myPlayer.currentEnergy;
			
		}
}
	