using UnityEngine;
using System.Collections;
using Pathfinding;

public class State_Wander : State {

	// Location to move
	private Vector2 location;
	
	public bool AmIRunning { get; private set; }
	
	//The calculated path
	public Path path;
	private Seeker seeker;
	private int currentWaypoint = 0;
	private bool endOfPath;
	private int NEXTWAYPOINTDISTANCE = 3;

	private bool goodPath;
	private bool waitingForPath;
	private int MIN_DISTANCE_AWAY = 40;
	private int MAX_DISTANCE_AWAY = 300;
	
	// Constructor
	public State_Wander(FSM fsm, Enemy owner)
		: base(fsm, owner)
	{
		this.AmIRunning = false;
		this.location = GetRandomPoint ();
	}
	
	// Called when the state is activated
	public override void OnStart()
	{
		// Path is bad by default
		this.goodPath = false;
		// Default is waiting for path
		this.waitingForPath = true;
		// Get random point
		location = GetRandomPoint ();


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
				if (!waitingForPath)
				{
					location = GetRandomPoint();
					seeker.StartPath(Owner.transform.position, location, OnPathComplete);
					waitingForPath = true;
					goodPath = false;
				}
				// Nothing to do yet.
				return;
			}
			
			// We reached the end of the path
			if (currentWaypoint >= path.vectorPath.Count) {
				//Debug.Log ("End Of Path Reached, still no enemy.");
				endOfPath = true;
				Owner.rigidbody2D.velocity = Vector2.zero;
				//doIHaveALastKnownLocation = false;
				location = GetRandomPoint();
				if (!waitingForPath)
				{
					seeker.StartPath(Owner.transform.position, location, OnPathComplete);
					waitingForPath = true;
					goodPath = false;
				}
			}
			
			//Direction to the next waypoint
			if (!endOfPath){
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
		//this.myFSM.myAIBrain.doISeeThePlayer = false; // COULD BE A PROBLEM
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
			goodPath = true;
			waitingForPath = false;
			return;
		}
		goodPath = false;
		waitingForPath = false;
		location = GetRandomPoint ();
	}

	private Vector2 GetRandomPoint()
	{
		// Get random point
		Vector2 dir = Random.insideUnitCircle;
		int dist = Random.Range (MIN_DISTANCE_AWAY, MAX_DISTANCE_AWAY);
		dir = dir.normalized;
		dir *= dist;
		Vector2 finalPos = 
			this.Owner.gameObject.transform.position +
						new Vector3 (dir.x, dir.y, 0);
		return finalPos;
	}


}
