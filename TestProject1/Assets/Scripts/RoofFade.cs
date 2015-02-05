using UnityEngine;
using System.Collections;

public class RoofFade : MonoBehaviour {

	private float roofAlpha = 1.0f;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () {
		int children = gameObject.transform.childCount;
		int i = 0;
		while (i < children) {
			Transform tile = gameObject.transform.GetChild(i);
			if(tile.name == "RoofTile") {
				Color color = tile.renderer.material.color;
				color.a = roofAlpha;
				tile.renderer.material.color = color;
			}
			i++;
		}
	}

	// changes the roofAlpha value which is applied to roof tiles in the update function
	IEnumerator ChangeTileAlpha (float alpha) {
		if (roofAlpha < alpha) {
			while (roofAlpha < alpha) {
				roofAlpha = roofAlpha + 0.05f;
				yield return new WaitForSeconds(0.01f);
			}
			StopAllCoroutines ();
		} else if (roofAlpha > alpha) {
			while (roofAlpha > alpha) {
				roofAlpha = roofAlpha - 0.05f;
				yield return new WaitForSeconds(0.01f);
			}
		}
	}

	// makes roof transparent on enter
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			StopAllCoroutines();
			StartCoroutine(ChangeTileAlpha(0.1f));
		}
	}

	// returns roof to solid on exit
	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			StopAllCoroutines();
			StartCoroutine(ChangeTileAlpha(1.0f));
		}
	}
}
