using UnityEngine;
using System.Collections;

public class State {

	// Owner of state
	public Enemy Owner { get; private set; }

	// Enemy (The player)
	public GameObject MyPlayer { get; protected set; }

	// The FSM we are in
	public FSM myFSM { get; private set; }

	// Constructor
	public State(FSM fsm, Enemy owner)
	{
		this.myFSM = fsm;
		this.Owner = owner;
	}

	// Called when the state is activated
	public virtual void OnStart()
	{

	}

	// Basic Update for the state
	public virtual void Update()
	{

	}


	// Called when leaving the state
	public virtual void OnExit()
	{

	}


	/// <summary>
	/// Sets the FSM.
	/// </summary>
	/// <param name="fsm">New FSM.</param>
	public void SetFSM(FSM fsm)
	{
		this.myFSM = fsm;
	}


	public bool DoISeeThisGuy()
	{
		return myFSM.myAIBrain.DoISeeThePlayerCharacter ();
	}

	          



}
