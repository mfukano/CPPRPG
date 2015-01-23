using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	private int levelWidth;
	private int levelHeight;

	public Transform grassTile;
	public Transform stoneBrickTile;

	private Color[] tileColors;

	public Color grassColor;
	public Color stoneBrickColor;

	public Texture2D levelTexture;

	// Use this for initialization
	void Start () {
		levelWidth = levelTexture.width;
		levelHeight = levelTexture.height;
		loadLevel ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void loadLevel()
	{
		tileColors = new Color[levelWidth * levelHeight];
		tileColors = levelTexture.GetPixels();

		for (int y = 0; y < levelHeight; y++)
		{
			for (int x = 0; x < levelWidth; y++)
			{
				if (tileColors[x+y*levelWidth] == grassColor)
				{
					Instantiate(grassTile, new Vector3(x,y), Quaternion.identity);
				}
				if (tileColors[x+y*levelWidth] == stoneBrickColor)
				{
					Instantiate(stoneBrickTile, new Vector3(x,y), Quaternion.identity);
				}
			}
		}
	}
}
