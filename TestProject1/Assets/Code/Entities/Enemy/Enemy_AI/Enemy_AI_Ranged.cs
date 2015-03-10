using UnityEngine;
using System.Collections;

public class Enemy_AI_Ranged : MonoBehaviour {

	// MyOwner
	public Enemy Owner;

	// Location Vars
	public bool DoISeePlayer;
	public Vector3 LastKnownLocation;
	public Transform TargetLocation;
	public Player TargetPlayer;

	// Visual Variables
	public float fieldOfViewAngle = 110;
	public int fieldOfViewDistance = 1000; // This is the maximum, based on the CircleCollider2D
	public bool playerInSight;
	public Vector3 personalLastKnownLocation;
	private GameObject player;
	private Animator playerAnim;
	private Vector3 previousSighting;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		foreach (CircleCollider2D col in GetComponents<CircleCollider2D>()) 
		{
			if(!col.isTrigger) continue;
			fieldOfViewDistance = (int)col.radius;
		}
	}

	// Use this for initialization
	void Start () {
		Owner = (Enemy)gameObject.GetComponentInParent (typeof(Enemy));
		TargetPlayer = (Player)GameObject.FindObjectOfType (typeof(Player));
	}
	
	// Update is called once per frame
	void Update () {
		// Going to try to make the enemy look at the player
		//DoISeePlayer = true;
		if (playerInSight) 
		{
			TargetLocation = TargetPlayer.transform;
			Owner.transform.rotation =
				Quaternion.LookRotation (Vector3.forward, TargetLocation.position - transform.position);
		} else 
		{
			// Check to see if I'm already at the players location

			// Moving to last known player location.
			Vector2 dir = personalLastKnownLocation - transform.position;
			transform.rotation = Quaternion.LookRotation (Vector3.forward, dir);
			// TODO: Put in fixed update
			//rigidbody2D.velocity = dir.normalized * Owner.enemySpeed; //* Time.deltaTime;
		}

		// Seeing the player
		if (LastKnownLocation != previousSighting)
			personalLastKnownLocation = LastKnownLocation;

		previousSighting = LastKnownLocation;

		//TODO: Check player health here
	}

	void OnTriggerStay2D(Collider2D other)
	{
		// Skip anything not the player
		if (other.gameObject != player)
		{
			return;
		}
		//Debug.Log ("Is the Player");
		// Player is hidden by default
		playerInSight = false;

		Vector3 direction = other.transform.position - transform.position;
		// Transform.up is used since the player is looking up to begin with
		float angle = Vector3.Angle (direction, transform.up);
		if (angle < fieldOfViewAngle * 0.5f) 
		{
			//Debug.Log ("In My angle.");
			/*
			// Save current object layer
			int oldLayer = gameObject.layer;

			// Change object layer to a layer it will be alone
			gameObject.layer = LayerMask.NameToLayer("Ghost");
			int layerToIgnore = 1 << gameObject.layer;
			layerToIgnore = ~layerToIgnore;
			*/
			RaycastHit2D[] hits = 
				Physics2D.RaycastAll(transform.position, direction.normalized, fieldOfViewDistance); //layerToIgnore);
			foreach (RaycastHit2D hit in hits)
			{
				if (hit)
				{
					if (hit.collider.CompareTag("Wall"))
					{
						// We hit a wall before a player
						break;
					}
					if(hit.collider.gameObject == player)
					{
						playerInSight = true;

						// Set last global sighting is player current position
						LastKnownLocation = player.transform.position;
						
						//Debug.Log("See the player!");
					}
				}
			}
			/*if (hit) {
				if(hit.collider.gameObject == player)
				{
					playerInSight = true;

					// Set last global sighting is player current position
					LastKnownLocation = player.transform.position;

					Debug.Log("See the player!");
				}
				//gameObject.layer = oldLayer;
			}*/
		}

		// Code here can be used to check animation frames to see if he is sneaking.

		// EXAMPLE CODE:
		// Store the name hashes of the current states.
		/*int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).nameHash;
		int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).nameHash;
		
		// If the player is running or is attracting attention...
		if(playerLayerZeroStateHash == hash.locomotionState || playerLayerOneStateHash == hash.shoutState)
		{
			// ... and if the player is within hearing range...
			if(CalculatePathLength(player.transform.position) <= col.radius)
				// ... set the last personal sighting of the player to the player's current position.
				personalLastSighting = player.transform.position;
		}*/
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject != player)
			return;
		playerInSight = false;
	}
}
