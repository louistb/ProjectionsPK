using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UiController : MonoBehaviour {

	public Text FPS, WindDirection,WindSpeed,CurrentTemp, LastClimax, LastWave,stepsIn10,reservoire;
	public PkData weatherData;
	public float lastClimax,lastWave;
	public DataPropolisEngine engineData;
	
	// Update is called once per frame
	void Update () {
		float msec = Time.deltaTime * 1000.0f;
		float fps = 1.0f / Time.deltaTime;
		FPS.text = 	 string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		stepsIn10.text = engineData.Count10Sec.ToString () + " / " + engineData.Last10.ToString();
		reservoire.text = engineData.BatteryLevel.ToString();
		LastClimax.text = string.Format("{0:0}:{1:00}", Mathf.Floor(engineData.lastClimax/60), engineData.lastClimax % 60);
		LastWave.text = string.Format("{0:0}:{1:00}", Mathf.Floor(engineData.lastWave/60), engineData.lastWave % 60);
		WindDirection.text = weatherData.DirectionWind;
		WindSpeed.text = weatherData.SpeedWind.ToString() + " Kmh";
		CurrentTemp.text = weatherData.CurrentTemperature.ToString() + " C";
	}
	public void Quit() {
		Application.Quit();
	}

	public void Reload() {
		var allBees = GameObject.FindGameObjectsWithTag ("bee");

		foreach (var bee in allBees) {

            if (bee.GetComponent<WalkingBees>() != null)
                bee.GetComponent<WalkingBees>().KillMeClimax();

            if (bee.GetComponent<FlyingBees>() != null)
                bee.GetComponent<FlyingBees>().KillMeClimax();

            Destroy(bee);
        }

		SceneManager.LoadSceneAsync("Main");
	}
}
