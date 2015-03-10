using UnityEngine;
using System.Collections;

public class FSM {

	// States
	public State CurrentState { get; private set; }
	public State PreviousState { get; private set; }

	// Character that owns this FSM
	public Enemy Owner { get; private set; }
	public Enemy_AI_Movement myAIBrain { get; protected set; }

	// Constructor
	public FSM(Enemy owner, Enemy_AI_Movement myAIBrains)
	{
		this.Owner = owner;
		this.myAIBrain = myAIBrains;
	}

	// Main Update loop
	public void Update()
	{
		if (CurrentState != null) 
		{
			CurrentState.Update ();
		} else
		{
			Debug.LogError("I have no state to update!");
		}
	}

	// Helper Function
	/// <summary>
	/// Changes the state.
	/// </summary>
	/// <returns><c>true</c>, If state was changed, <c>false</c> otherwise.</returns>
	/// <param name="newState">New state to switch to.</param>
	public bool ChangeState(State newState)
	{
		if (CurrentState == newState)
		{
			Debug.LogError("Tried to switch to the same state.");
			return false;
		}

		// Run exit method of current state
		CurrentState.OnExit ();
		// Run start method of new state
		newState.OnStart();
		// Keep track of previous state
		PreviousState = CurrentState;
		// Set current state to the new state
		CurrentState = newState;

		return true;

	}

	/// <summary>
	/// Sets the starting state and call OnStart(). Only call this function once.
	/// </summary>
	/// <param name="startingState">Starting state.</param>
	public void SetStartingState(State startingState)
	{
		CurrentState = startingState;
		CurrentState.SetFSM (this);
		CurrentState.OnStart ();
	}
	
}
