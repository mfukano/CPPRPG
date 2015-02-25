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
	public bool playerInSight;
	public Vector3 personalLastKnownLocation;
	private GameObject player;
	private Animator playerAnim;
	private Vector3 previousSighting;

	void Awake()
	{

	}

	// Use this for initialization
	void Start () {
		Owner = (Enemy)gameObject.GetComponentInParent (typeof(Enemy));
		TargetPlayer = (Player)GameObject.FindObjectOfType (typeof(Player));
	}
	
	// Update is called once per frame
	void Update () {
		// Going to try to make the enemy look at the player
		DoISeePlayer = true;
		TargetLocation = TargetPlayer.transform;
		Owner.transform.rotation = Quaternion.LookRotation (Vector3.forward, TargetLocation.position - transform.position);
		if (LastKnownLocation != previousSighting)
			personalLastKnownLocation = LastKnownLocation;
		previousSighting = LastKnownLocation;

		//TODO: Check player health here
	}

	void OnTriggerStay2D(Collider2D other)
	{
		// Player is hidden by default
		playerInSight = false;

		Vector3 direction = other.transform.position - transform.position;
		float angle = Vector3.Angle (direction, transform.forward);
	}
}
