using UnityEngine;
using System.Collections;

public class Enemy_Spawner : MonoBehaviour {

	// Spawner Var
	public int CurrentRandomEnemies { get; set; }
	public GameObject Camera {get;set;}

	private int MIN_DISTANCE_TO_SPAWN = 400;
	private int MAX_DISTANCE_TO_SPAWN = 1000;

}
