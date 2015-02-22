using UnityEngine;
using System.Collections;

public class Enemy_AI_Ranged : MonoBehaviour {

	// MyOwner
	public Enemy Owner;

	// Location Vars
	public bool DoISeePlayer;
	public Transform LastKnownLocation;
	public Transform TargetLocation;
	public Player TargetPlayer;

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
	}
}
