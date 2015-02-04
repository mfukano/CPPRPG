using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public int width;
	public int height;
	public Texture2D mapTexture;

	void Start() {
		width = mapTexture.width;
		height = mapTexture.height;
	}
}
