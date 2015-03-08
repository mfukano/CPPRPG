using UnityEngine;
using System.Collections;
using Pathfinding;

public class Enemy_AI_Movement : MonoBehaviour {

	public Vector3 targetPosition;

	// My Enemy Script
	private Enemy myself;

	//The calculated path
	public Path path;
	private Seeker seeker;

	// The player's actual position
	public GameObject targetPlayer;
	public Vector2 playerPos;

	// The players last known loction
	public bool doISeeThePlayer;
	public bool doIKnowWhereThePlayerIs;
	public bool doIHaveALastKnownLocation;
	public Vector2 lastKnownLocation;

	// AI Vision
	public int fieldOfViewDistance;
	public int fieldOfViewAngle = 110;

	
	//The AI's speed per second
	public bool isRunning = true;
	public float runSpeed = 5000;
	public float walkSpeed = 1000;

	//The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 10;

	//The waypoint we are currently moving towards
	private int currentWaypoint = 0;
	private bool endOfPath = false;
	private bool usingLastKnownPath = false;


	public void Start () {
		//Get a reference to the Seeker component we added earlier
		seeker = GetComponent<Seeker>();
		myself = GetComponent<Enemy> ();

		// Find player Position
		targetPlayer = GameObject.FindGameObjectWithTag ("Player");
		playerPos = targetPlayer.transform.position;

		// Initialize the Vars
		doISeeThePlayer = false;
		doIKnowWhereThePlayerIs = false;
		doIHaveALastKnownLocation = false;
		lastKnownLocation = new Vector2 (0, 0);

		// Find the right collider
		foreach (CircleCollider2D col in GetComponents<CircleCollider2D>()) 
		{
			if(!col.isTrigger) continue;
			fieldOfViewDistance = (int)col.radius;
		}
		
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position,targetPosition, OnPathComplete);
		endOfPath = false;
	}
	
	public void OnPathComplete (Path p) {
		Debug.Log ("Yay, we got a path back. Did it have an error? "+p.error);
		if (!p.error) {
			path = p;
			//Reset the waypoint counter
			currentWaypoint = 0;
			endOfPath = false;
		}
	}

	public void FixedUpdate () {
		if (path == null) {
			//We have no path to move after yet
			return;
		}
		
		if (currentWaypoint >= path.vectorPath.Count) {
			//Debug.Log ("End Of Path Reached");
			endOfPath = true;
			//usingLastKnownPath = false;
			rigidbody2D.velocity = Vector2.zero;
			if (usingLastKnownPath)
			{
				doIHaveALastKnownLocation = false;
				usingLastKnownPath = false;
				doISeeThePlayer = false;
			}
			//doIHaveALastKnownLocation = false;
		}

		// If we see the player look at him and stop moving and attack!
		if (doISeeThePlayer) 
		{
			// Set mi up sum bewls
			doIKnowWhereThePlayerIs = true;
			doIHaveALastKnownLocation = true;
			usingLastKnownPath = false;

			path.vectorPath.Clear();
			//Debug.Log("Released All");
			// Stop moving
			rigidbody2D.velocity = Vector2.zero;

			transform.rotation = 
				Quaternion.LookRotation(Vector3.forward, -(targetPlayer.transform.position - transform.position));
			// Shoot the fucker right in the *** (for ranged)
			Shoot();
			return; // COULD BE BAD
		}

		// If we can't see the player do we know where he WAS?
		if (doIHaveALastKnownLocation)
		{
			//Only set the new path once
			if (!usingLastKnownPath)
			{
				// Create the new path
				seeker.StartPath(transform.position, lastKnownLocation, OnPathComplete);
				usingLastKnownPath = true;
				//endOfPath = true;
				currentWaypoint = 0;
				Debug.Log("Set a new path");
			}
		}

		// So if we don't see him and don't know where he was...
		// Look for the player
		if (!doIHaveALastKnownLocation)
		{
			LookForPlayer ();
		}


		//Direction to the next waypoint
		if (!endOfPath){
			//Debug.Log ("Not at end of path");
			Vector3 dir = (path.vectorPath[currentWaypoint]-transform.position).normalized;
			dir *= (isRunning ? runSpeed : walkSpeed) * Time.fixedDeltaTime;
			rigidbody2D.velocity = dir;
			// Look at it  
			transform.rotation = 
				Quaternion.LookRotation(Vector3.forward, -dir);
			
			//Check if we are close enough to the next waypoint
			//If we are, proceed to follow the next waypoint
			if (Vector3.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
				currentWaypoint++;
				return;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		// If we are in the vision area, check if we can see the player.
		// Skip anything not the player
		if (other.gameObject != targetPlayer)
		{
			return;
		}

		// I Don't See the player by default
		doISeeThePlayer = false;
		// Find direction to player
		Vector3 direction = other.transform.position - transform.position;
		// Transform.up is used since the player is looking up to begin with
		float angle = Vector3.Angle (direction, -transform.up);
		// If the player is within our view angle
		if (angle < fieldOfViewAngle * 0.5f) 
		{
			// This block of code checks to see if we have line of sight to the player
			RaycastHit2D[] hits = 
				Physics2D.RaycastAll(transform.position, direction.normalized, fieldOfViewDistance); //layerToIgnore);
			foreach (RaycastHit2D hit in hits)
			{
				if (hit)
				{
					if (hit.collider.CompareTag("Wall"))
					{
						// We hit a wall before a player so we can't see him
						break;
					}
					if(hit.collider.gameObject == targetPlayer)
					{
						doISeeThePlayer = true;
						doIKnowWhereThePlayerIs = true;
						
						// Set last global sighting is player current position
						lastKnownLocation = targetPlayer.transform.position;
						doIHaveALastKnownLocation = true;
						isRunning = true;
						
						//Debug.Log("See the player!");
					}
				}
			}
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject != targetPlayer)
		{
			return;
		}

		doISeeThePlayer = false;
		doIKnowWhereThePlayerIs = false;
	}

	void Shoot()
	{
		myself.Shoot();
	}

	void LookForPlayer()
	{
		//Debug.Log ("Should be looking");
		// TODO: run looking animation

		isRunning = false;
		lastKnownLocation = targetPlayer.transform.position;
		doIHaveALastKnownLocation = true;

	}
}





