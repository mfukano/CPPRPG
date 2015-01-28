using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Level : MonoBehaviour {

	private int levelWidth;
	private int levelHeight;
	private int tileWidth = 30;
	private int tileHeight = 30;

	public Transform grassTile;
	public Transform lavaTile;

	private Color[] tileColors;

	public Color grassColor;
	public Color lavaColor;

	public Texture2D levelTexture;

	public Entity player;



	// Use this for initialization
	void Start () {
		levelWidth = levelTexture.width;
		levelHeight = levelTexture.height;
		loadLevel ();
	
	}

	// Make the script also execute in edit mode.

	// Update is called once per frame
	void Update () {
		//loadLevel ();
	}

	void loadLevel()
	{
		tileColors = new Color[levelWidth * levelHeight];
		tileColors = levelTexture.GetPixels();

		for (int y = 0; y < levelHeight; y++)
		{
			for (int x = 0; x < levelWidth; x++)
			{
				if (tileColors[x+(y*levelWidth)] == grassColor)
				{
					Instantiate(grassTile, new Vector3(x*tileWidth,y*tileHeight), Quaternion.identity);
				}
				if (tileColors[x+(y*levelWidth)] == lavaColor)
				{
					Instantiate(lavaTile, new Vector3(x*tileWidth,y*tileHeight), Quaternion.identity);
				}
			}
		}
	}
}
