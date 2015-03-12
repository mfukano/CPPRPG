using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadPlayerStats : MonoBehaviour
{
	public Slider healthBar;
	public Slider energyBar;
	private Player myPlayer;
#if UNITY_EDITOR
	public GUIStyle style;
#endif

	// Use this for initialization
	void Start() {
		myPlayer = (Player)GameObject.FindObjectOfType(typeof(Player));
		healthBar.value = myPlayer.currentHealth;
		energyBar.value = myPlayer.currentEnergy;
	}

	IEnumerator Death(){
#if UNITY_EDITOR
		style.normal.textColor = new Color(0.8F, 0, 0.2F, 0.95F);
		style.alignment = TextAnchor.UpperCenter;
		style.fontSize = 100;
		GUI.TextField (new Rect(Screen.width*0.25F,Screen.height*0.25F,Screen.width*0.5F,Screen.height*0.25F), "U DED!?", style);
#endif
		myPlayer.gameObject.SetActive (false);
		yield return new WaitForSeconds (0.2F);
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
	