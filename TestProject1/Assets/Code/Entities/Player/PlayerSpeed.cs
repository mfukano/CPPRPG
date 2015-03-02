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
				if (currentEnergy >= 3) {
						currentEnergy -= 3;
						SetPlayerEnergy ();
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
			Debug.Log ("LeftShift registered");
			myPlayer.playerSpeed = runSpeed;
		    if (energyRegen) {
						energyRegen = false;
						InvokeRepeating ("EnergyDrain", 1, 1);
				}
		}
		
		void SetWalk ()
		{
			// Else-block disables running, sets energy regen
			myPlayer.playerSpeed = walkSpeed;
		if (!energyRegen) {
						energyRegen = true;
						InvokeRepeating ("EnergyRestore", 1, 1);
				}
		}

		// Update is called once per frame
		void Update ()
		{
				
				if (Input.GetKeyDown (KeyCode.LeftShift)) {
					if(energyRegen) CancelInvoke ("EnergyRestore");
						isRunning = true;
						SetRun ();
				} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
					if(!energyRegen) CancelInvoke ("EnergyDrain");
						isRunning = false;
						SetWalk ();
				}
		}

}



