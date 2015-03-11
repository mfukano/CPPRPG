using UnityEngine;
using System.Collections;

public class DayNightCycleFast : MonoBehaviour {
	float time = 0;
	float duration = 1; 
	float progress = 0;
	float progress1 = 0;
	float progress2 = 0;
	float progress3 = 0;
	float smoothness = 0.02f;
	float startTime = 0;
	float elapsedTime = 0;
	Color currentColor = Color.white;

	Color fullDark = new Color(32.0f / 255.0f, 28.0f / 255.0f, 46.0f / 255.0f);  
	Color fullLight = new Color(1.0f, 1.0f, 1.0f);  
	Color dawnDuskFog = new Color(133.0f / 255.0f, 124.0f / 255.0f, 102.0f / 255.0f);  
	Color dayFog = new Color(180.0f / 255.0f, 208.0f / 255.0f, 209.0f / 255.0f);  
	Color nightFog = new Color(12.0f / 255.0f, 15.0f / 255.0f, 91.0f / 255.0f);  
	bool light = true;
	bool fog = true;
	bool dark = true;
	bool dawn = true;
	bool masterBool = false;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime = Time.time - startTime;
		if (masterBool == true) {
			if (Time.time > 30) {
				startTime = Time.time;
				StopAllCoroutines ();
				light = true; fog = true; dark = true; dawn = true;
				progress = 0; progress1 = 0; progress2 = 0; progress3 = 0;
				elapsedTime = 0;
				masterBool = false;
			}
		}
		if (light == true) {
			if (elapsedTime > 5) {
				StartCoroutine ("LerpColorDayFog");
				light = false;
			}
		}
//		if (RenderSettings.ambientLight == dayFog) {
//			light = false;
//			StopCoroutine("ColorLerpDayFog");
//		}
		if (fog == true) {
			if (elapsedTime > 10) {
				StopCoroutine("LerpColorDayFog");
				StartCoroutine ("LerpColorFullDark");
				fog = false;
			}
		}
//		if (RenderSettings.ambientLight == fullDark) {
//			fog = false;
//			StopCoroutine("LerpColorFullDark");
//		}
		if (dark == true) {
			if (elapsedTime > 15 ){
				StopCoroutine("LerpColorFullDark");
				StartCoroutine ("LerpColorDawn");
				dark = false;
			}
		}
//		if (RenderSettings.ambientLight == dawnDuskFog) {
//			dark = false;
//			StopCoroutine("LerpColorDawn");
//		}
		if (dawn == true) {
			if (elapsedTime > 20) {
				StopCoroutine("LerpColorDawn");
				StartCoroutine ("LerpColorDay");
				dawn = false;
				masterBool = true;
			}
		}
	}

	IEnumerator LerpColorDayFog()
	{
		 //This float will serve as the 3rd parameter of the lerp function.
		float increment = smoothness/duration; //The amount of change to apply.
		
		while(progress < 1)
		{
			RenderSettings.ambientLight = Color.Lerp(fullLight, dayFog, progress);
			progress += increment;
			yield return new WaitForSeconds(smoothness);
		}
//		RenderSettings.ambientLight = dayFog;
		yield return true;

	}

	IEnumerator LerpColorFullDark()
	{
		StopCoroutine ("LerpColorDayFog");
		float progress1 = 0; //This float will serve as the 3rd parameter of the lerp function.
		float increment = smoothness/duration; //The amount of change to apply.
		
		while(progress1 < 1)
		{
			StopCoroutine ("LerpColorDayFog");
			RenderSettings.ambientLight = Color.Lerp(dayFog, fullDark, progress1);
			progress1 += increment;
			yield return new WaitForSeconds(smoothness);
		}
//		RenderSettings.ambientLight = fullDark;
//		StopCoroutine("LerpColorDayFog");
		yield return true;
	}

	IEnumerator LerpColorDawn()
	{
		StopCoroutine ("LerpColorFullDark");
		float progress2 = 0; //This float will serve as the 3rd parameter of the lerp function.
		float increment = smoothness/duration; //The amount of change to apply.
		
		while(progress2 < 1)
		{
			RenderSettings.ambientLight = Color.Lerp(fullDark, dawnDuskFog , progress2);
			progress2 += increment;
			yield return new WaitForSeconds(smoothness);
		}
//		RenderSettings.ambientLight = dawnDuskFog;
		yield return true;
	}

	IEnumerator LerpColorDay()
	{
		StopCoroutine ("LerpColorDawn");
		float progress3 = 0; //This float will serve as the 3rd parameter of the lerp function.
		float increment = smoothness/duration; //The amount of change to apply.
		
		while(progress3 < 1)
		{
			RenderSettings.ambientLight = Color.Lerp(dawnDuskFog,fullLight , progress3);
			progress3 += increment;
			yield return new WaitForSeconds(smoothness);
		}
		yield return true;
	}
	

		




}
