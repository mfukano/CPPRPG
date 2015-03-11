using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Spawner : MonoBehaviour {

	// Spawner Var
	public int CurrentRandomEnemies { get; set; }
	public LinkedList<Enemy> ListEnemies;
	public GameObject Camera { get; set; }
	public BoxCollider2D SpawnArea { get; set; }

	

	public int MAX_CURRENT_ENEMIES = 6;
	private int MIN_DISTANCE_TO_SPAWN = 600;
	private int MAX_DISTANCE_TO_SPAWN = 1200;
	private int TIME_BETWEEN_WAVES = 30;
	private bool ShouldIStartNextWave { get; set; }
	

	void Start()
	{
		this.ListEnemies = new LinkedList<Enemy> ();
		this.Camera = GameObject.FindGameObjectWithTag ("MainCamera");
		this.SpawnArea = (BoxCollider2D)gameObject.GetComponent<BoxCollider2D> ();
		CurrentRandomEnemies = 0;
		this.ShouldIStartNextWave = true;
	}

	void Update()
	{
		while (CurrentRandomEnemies < MAX_CURRENT_ENEMIES)
		{
			Debug.Log("Adding_Enemy");
			Vector2 tempPos = GetRandomPointInBox();
			GameObject enemy = 
				Instantiate(Resources.Load("Prefabs/Enemies/Enemy_Wander"), tempPos,
				            Quaternion.AngleAxis(180,  Vector3.forward)) as GameObject;
			CurrentRandomEnemies++;
			//ListEnemies.AddLast(e);
		}
	}

	// Helper functions
	public Vector2 GetRandomPointInBox()
	{
		// Find random point in box
		float dx = Random.Range (-SpawnArea.size.x / 2, SpawnArea.size.x / 2);
		float dy = Random.Range (-SpawnArea.size.y / 2, SpawnArea.size.y) / 2;
		return new Vector2 (dx, dy);
	}

}
