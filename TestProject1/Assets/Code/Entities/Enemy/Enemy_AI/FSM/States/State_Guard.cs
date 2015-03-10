using UnityEngine;
using System.Collections;
using Pathfinding;

public class State_Guard : State {

	// Vars
	public Vector2 PositionToGuard { get; private set; }
	private int DISTANCEOFFSET = 10;

	// Constructor
	public State_Guard(FSM fsm, Enemy owner, Vector2 locationToGuard)
	: base(fsm, owner)
	{
		this.PositionToGuard = locationToGuard;
	}
	
	// Called when the state is activated
	public override void OnStart()
	{
		// If we are NOT near our guard location
		if (Mathf.Abs(Owner.transform.position.x - PositionToGuard.x) > DISTANCEOFFSET ||
		    Mathf.Abs(Owner.transform.position.y - PositionToGuard.y) > DISTANCEOFFSET)
		{
			this.myFSM.ChangeState(new State_GotoLocationAware(myFSM, this.Owner, PositionToGuard, false));
		} else
		{
			Owner.transform.position = PositionToGuard;
			Owner.rigidbody2D.velocity = Vector2.zero;
			this.MyPlayer = GameObject.FindGameObjectWithTag("Player");
		}
	}
	
	// Basic Update for the state
	public override void Update()
	{
		// If I don't see the player Do nothing
		if (!DoISeeThisGuy())
		{
			return;
		}

		// Otherwise Look at him and change state
		
		/*Owner.transform.rotation = 
			Quaternion.LookRotation(Owner.myAI.myUpVector,
				-(MyPlayer.transform.position - Owner.transform.position));*/
		myFSM.ChangeState (new State_Attack (myFSM, Owner));
		
	}
	
	
	// Called when leaving the state
	public override void OnExit()
	{
		
	}


}
