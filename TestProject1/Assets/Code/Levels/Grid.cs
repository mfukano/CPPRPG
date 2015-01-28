using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public float width = 32.0f;
	public float height = 32.0f;
	public float sizeOfDrawnGrid = 1200.0f;

	public Color color = Color.white;

	public Transform tilePrefab;

	public TileSet tileSet;

	public bool canPlace = true;

	void OnDrawGizmos() {
		Vector3 pos = Camera.current.transform.position;
		Gizmos.color = this.color;

		for (float y = pos.y - sizeOfDrawnGrid; y < pos.y + sizeOfDrawnGrid; y+= this.height) 
		{
			Gizmos.DrawLine(new Vector3 (-1000000.0f, Mathf.Floor(y/height)*height,0.0f),
			                new Vector3(1000000.0f, Mathf.Floor(y/height)*height,0.0f));
		}

		for (float x = pos.x - sizeOfDrawnGrid; x < pos.x + sizeOfDrawnGrid; x+= this.height) 
		{
			Gizmos.DrawLine(new Vector3 (Mathf.Floor(x/width)*width, -1000000.0f, 0.0f),
			                new Vector3 (Mathf.Floor(x/width)*width, 1000000.0f, 0.0f));
		}
	}
}
