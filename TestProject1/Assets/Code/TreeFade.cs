using UnityEngine;
using System.Collections;

public class TreeFade : MonoBehaviour {

	private float treeAlpha = 1.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Transform tree = gameObject.transform;
		Color col = tree.renderer.material.color;
		col.a = treeAlpha;
		tree.renderer.material.color = col;
	}

	IEnumerator ChangeTreeAlpha (float alpha) {
		if (treeAlpha < alpha) {
			while (treeAlpha < alpha) {
				treeAlpha = treeAlpha + 0.05f;
				yield return new WaitForSeconds(0.01f);
			}
			StopAllCoroutines ();
		} else if (treeAlpha > alpha) {
			while (treeAlpha > alpha) {
				treeAlpha = treeAlpha - 0.05f;
				yield return new WaitForSeconds(0.01f);
			}
		}
	}

	// makes tree transparent on enter
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			StopAllCoroutines();
			StartCoroutine(ChangeTreeAlpha(0.5f));
		}
	}
	
	// returns tree to solid on exit
	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			StopAllCoroutines();
			StartCoroutine(ChangeTreeAlpha(1.0f));
		}
	}
}
