using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class State_GotoLocationAware : State {

	// Location to move
	private Vector2 location;

	public bool AmIRunning { get; private set; }

	//The calculated path
	public Path path;
	private Seeker seeker;
	private int currentWaypoint = 0;
	private bool endOfPath;
	private int NEXTWAYPOINTDISTANCE = 3;

	// Constructor
	public State_GotoLocationAware(FSM fsm, Enemy owner, Vector2 theLocation, bool isRunning)
		: base(fsm, owner)
	{
		this.location = theLocation;
		this.AmIRunning = isRunning;
	}
	
	// Called when the state is activated
	public override void OnStart()
	{
		seeker = Owner.gameObject.GetComponent<Seeker> ();
		//this.MyPlayer = GameObject.FindGameObjectWithTag("Player");
		seeker.StartPath (Owner.transform.position, location, OnPathComplete);
		endOfPath = false;
	}
	
	// Basic Update for the state
	public override void Update()
	{
		// If I see the player, Attack()
		if (DoISeeThisGuy())
		{
			this.myFSM.ChangeState(new State_Attack(this.myFSM, this.Owner));
		} else 
		{
			if(path == null)
			{
				// Nothing to do yet.
				return;
			}

			// We reached the end of the path
			if (currentWaypoint >= path.vectorPath.Count) {
//				Debug.Log ("End Of Path Reached, still no enemy.");
				endOfPath = true;
				Owner.rigidbody2D.velocity = Vector2.zero;
				//doIHaveALastKnownLocation = false;

				// If we were walking that means we made it back to our post
				if(!AmIRunning)
				{
					myFSM.ChangeState(new State_Guard(this.myFSM, this.Owner, Owner.myAI.guardPoints[0]));
					return;
				}

				// Return to Post
				switch(Owner.myAI.myAIType)
				{
					case (EnemyAIType.Guard): 
						myFSM.ChangeState(new State_GotoLocationAware(
						this.myFSM,
						this.Owner,
						Owner.myAI.guardPoints[0],
						false)) ;// Not running
						break;

					case (EnemyAIType.Patrol): 
						myFSM.ChangeState(new State_GotoLocationAware(
						this.myFSM,
						this.Owner,
						Owner.myAI.guardPoints[0],
						false)); // Not running
						break;

					case (EnemyAIType.Wander): 
						 myFSM.ChangeState(new State_Wander(
						this.myFSM, 
						this.Owner));
						break;

					default:
						break;
				}
			}

			//Direction to the next waypoint
			if (!endOfPath){
				//Debug.Log ("Not at end of path");
				// If I see the player go back to attack mode.
				if (DoISeeThisGuy())
				{
					myFSM.ChangeState(new State_Attack(myFSM, this.Owner));
					return;
				}
				Vector3 dir = (path.vectorPath[currentWaypoint]-Owner.transform.position).normalized;
				dir *= (AmIRunning ? Owner.myAI.runSpeed : Owner.myAI.walkSpeed) * Time.fixedDeltaTime;
				Owner.rigidbody2D.velocity = dir;
				// Look at it  
				Owner.transform.rotation = 
					Quaternion.LookRotation(Owner.myAI.myUpVector, -dir);
				
				//Check if we are close enough to the next waypoint
				//If we are, proceed to follow the next waypoint
				if (Vector3.Distance (Owner.transform.position,path.vectorPath[currentWaypoint]) < NEXTWAYPOINTDISTANCE) {
					currentWaypoint++;
					return;
				}
			}
		}
	}

	// Called when leaving the state
	public override void OnExit()
	{
		this.myFSM.myAIBrain.doISeeThePlayer = false; // COULD BE A PROBLEM
	}

	// Helper Function
	public void OnPathComplete(Path p)
	{
//		Debug.Log ("Yay, we got a path back from State_GotoLocationAware. Did it have an error? "+p.error);
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
			endOfPath = false;
		}
	}

}
