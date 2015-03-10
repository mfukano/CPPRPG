using UnityEngine;
using System.Collections;

public class PlayerSpeed : MonoBehaviour
{
		private Player myPlayer;
		public float startEnergy = 100;
		public float currentEnergy;
		public float walkSpeed = 400;
		public float runSpeed = 600;
		public bool isRunning;
		public bool energyRegen;
		// Use this for initialization
		void Start ()
		{
				myPlayer = (Player)GameObject.FindObjectOfType (typeof(Player));
				myPlayer.currentEnergy = startEnergy;
				currentEnergy = myPlayer.currentEnergy;
				myPlayer.playerSpeed = walkSpeed;
				energyRegen = true;
		}

		void SetPlayerEnergy ()
		{
				myPlayer.currentEnergy = currentEnergy;
		}

		void EnergyDrain ()
		{
				if (currentEnergy >= 2) {
						currentEnergy -= 2;
						SetPlayerEnergy ();
				} else {
						SetWalk ();
				}
				
		}
	
		void EnergyRestore ()
		{
				if (currentEnergy < startEnergy) {
						currentEnergy += 1;
						SetPlayerEnergy ();
				}
		}
	
		void SetRun ()
		{
				CancelInvoke ("EnergyRestore");
				isRunning = true;
				myPlayer.playerSpeed = runSpeed;
				if (energyRegen) {
						energyRegen = false;
						InvokeRepeating ("EnergyDrain", 0.1F, 0.1F);
				}
				if (currentEnergy < 3) {
			        isRunning = false;
				}
		}
		
		public void SetWalk ()
		{
				// Else-block disables running, sets energy regen
				isRunning = false;
				CancelInvoke ("EnergyDrain");
				myPlayer.playerSpeed = walkSpeed;
				if (!energyRegen) {
						energyRegen = true;
						InvokeRepeating ("EnergyRestore", 0.5F, 0.3F);
				}
		}

		// Update is called once per frame
		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.LeftShift) && currentEnergy > 2) {
						SetRun ();
				} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
						SetWalk ();
				}
		}

}



