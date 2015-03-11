using UnityEngine;
using System.Collections;

public class Enemy_Spawner : MonoBehaviour {

	// Spawner Var
	public int CurrentRandomEnemies { get; set; }
	public GameObject Camera { get; set; }
	public BoxCollider2D SpawnArea { get; set; }
	

	public int MAX_CURRENT_ENEMIES = 6;
	private int MIN_DISTANCE_TO_SPAWN = 600;
	private int MAX_DISTANCE_TO_SPAWN = 1200;
	private int TIME_BETWEEN_WAVES = 30;
	private bool ShouldIStartNextWave { get; set; }
	

	void Start()
	{
		this.Camera = GameObject.FindGameObjectWithTag ("MainCamera");
		this.SpawnArea = (BoxCollider2D)gameObject.GetComponent<BoxCollider2D> ();
		CurrentRandomEnemies = 0;
		this.ShouldIStartNextWave = true;
	}

	void Update()
	{
		while (CurrentRandomEnemies < MAX_CURRENT_ENEMIES)
		{
			Vector2 spawnPoint = GetRandomPointInBox();
			Instantiate(Resources.Load ("Prefabs/Enemies/Enemy_Wander"), spawnPoint,
			            Quaternion.AngleAxis(180, Vector3.forward));
			CurrentRandomEnemies++;
		}
	}

	// Helper functions
	public Vector2 GetRandomPointInBox()
	{
		// Find random point in box
		float dx = Random.Range (-SpawnArea.size.x / 2, SpawnArea.size.x / 2);
		float dy = Random.Range (-SpawnArea.size.y / 2, SpawnArea.size.y / 2);
		return new Vector2 (dx, dy);
	}

}
