using UnityEngine;
using System.Collections;
using Pathfinding;

public class State_Attack : State {

	// Last location
	Vector2 lastKnownLocation;

	// Constructor
	public State_Attack(FSM fsm, Enemy owner)
		: base(fsm, owner)
	{

	}
	
	// Called when the state is activated
	public override void OnStart()
	{
		this.MyPlayer = GameObject.FindGameObjectWithTag("Player");
		//lastKnownLocation = null;
	}
	
	// Basic Update for the state
	public override void Update()
	{
		// If I see the player, Attack()
		if (DoISeeThisGuy())
		{
			// Stop Moving
			Owner.rigidbody2D.velocity = Vector2.zero;
			// Look that way
			Owner.transform.rotation = 
				Quaternion.LookRotation(Owner.myAI.myUpVector,
					-(MyPlayer.transform.position - Owner.transform.position));
			this.lastKnownLocation = MyPlayer.transform.position;
			this.Owner.Shoot();
		} else 
		{
			this.myFSM.ChangeState (new State_GotoLocationAware(myFSM, Owner, lastKnownLocation, true));
		}
		
	}
	
	
	// Called when leaving the state
	public override void OnExit()
	{
		this.myFSM.myAIBrain.doISeeThePlayer = false; // COULD BE A PROBLEM
	}
}
