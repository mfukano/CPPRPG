using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
	float time = 0;
	float duration = 5; 
	float progress = 0;
	float progress1 = 0;
	float progress2 = 0;
	float progress3 = 0;
	float smoothness = 0.02f; 
	Color currentColor = Color.white;

	Color fullDark = new Color(32.0f / 255.0f, 28.0f / 255.0f, 46.0f / 255.0f);  
	Color fullLight = new Color(1.0f, 1.0f, 1.0f);  
	Color dawnDuskFog = new Color(133.0f / 255.0f, 124.0f / 255.0f, 102.0f / 255.0f);  
	Color dayFog = new Color(180.0f / 255.0f, 208.0f / 255.0f, 209.0f / 255.0f);  
	Color nightFog = new Color(12.0f / 255.0f, 15.0f / 255.0f, 91.0f / 255.0f);  
	bool fuckit = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		time = Time.time;
		if (Time.time > 80) {
			time = 0;
		}
		if (time > 5) {
			StartCoroutine("LerpColorDayFog");
		}
		if (RenderSettings.ambientLight == dayFog) {
			StopCoroutine("ColorLerpDayFog");
			fuckit = true;
		}
		if (time > 20) {
			StartCoroutine("LerpColorFullDark");
		}
		if (RenderSettings.ambientLight == fullDark) {

			StopCoroutine("LerpColorFullDark");
		}
		if (time > 40) {
			StartCoroutine("LerpColorDawn");
		}
		if (RenderSettings.ambientLight == dawnDuskFog) {

			StopCoroutine("LerpColorFullDark");
			StopCoroutine("LerpColorDawn");
		}
		if (time > 60) {
			StartCoroutine("LerpColorDay");
		}
		if (RenderSettings.ambientLight == fullLight) {
			StopAllCoroutines();
		}
		if (fuckit == true) {
			StopCoroutine("ColorLerpDayFog");
		}
	}

	IEnumerator LerpColorDayFog()
	{
		print ("dayfogishappening");
		 //This float will serve as the 3rd parameter of the lerp function.
		float increment = smoothness/duration; //The amount of change to apply.
		
		while(progress < 1)
		{
			RenderSettings.ambientLight = Color.Lerp(fullLight, dayFog, progress);
			progress += increment;
			yield return new WaitForSeconds(smoothness);
		}
		RenderSettings.ambientLight = dayFog;
		yield return true;

	}

	IEnumerator LerpColorFullDark()
	{
		print ("fulldarkishappening");
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
		RenderSettings.ambientLight = fullDark;
		StopCoroutine("LerpColorDayFog");
		yield return true;
	}

	IEnumerator LerpColorDawn()
	{
		print ("dawnishappening");
		StopCoroutine ("LerpColorFullDark");
		float progress2 = 0; //This float will serve as the 3rd parameter of the lerp function.
		float increment = smoothness/duration; //The amount of change to apply.
		
		while(progress2 < 1)
		{
			RenderSettings.ambientLight = Color.Lerp(fullDark, dawnDuskFog , progress2);
			progress2 += increment;
			yield return new WaitForSeconds(smoothness);
		}
		RenderSettings.ambientLight = dawnDuskFog;
		yield return true;
	}

	IEnumerator LerpColorDay()
	{
		print ("dayishappening");
		StopCoroutine ("LerpColorDawn");
		float progress3 = 0; //This float will serve as the 3rd parameter of the lerp function.
		float increment = smoothness/duration; //The amount of change to apply.
		
		while(progress3 < 1)
		{
			RenderSettings.ambientLight = Color.Lerp(dawnDuskFog,fullLight , progress3);
			progress3 += increment;
			yield return new WaitForSeconds(smoothness);
		}
		RenderSettings.ambientLight = fullLight;
		yield return true;
	}
	

		




}
