using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadPlayerStats : MonoBehaviour
{
	public Slider healthBar;
	public Slider energyBar;
	private Player myPlayer;
	// Use this for initialization
	void Start() {
		myPlayer = (Player)GameObject.FindObjectOfType(typeof(Player));
		healthBar.value = myPlayer.currentHealth;
		energyBar.value = myPlayer.currentEnergy;
	}

	void Update() {
				healthBar.value = myPlayer.currentHealth;
				energyBar.value = myPlayer.currentEnergy;
		}
}
	