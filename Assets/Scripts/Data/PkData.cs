using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PkData : MonoBehaviour {

	public string MeteoUrl = "http://meteo.pqds.org/realtime.txt";
	public float SpeedWind,MaxWindSpeed,CurrentTemperature,CurrentHumidity;
	public string DirectionWind;
    public WindUpdater wind;

	private string[] dataArray;

	void Start () {
		StartCoroutine (GetDataFromWebpage());
	}


	public IEnumerator GetDataFromWebpage ()
	{
		while (true) {
			var dateTime = new DateTime (2015, 05, 24, 10, 2, 0, DateTimeKind.Local);
			var epoch = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			var unixDateTime = (dateTime.ToUniversalTime () - epoch).TotalSeconds;
			WWW webpage = new WWW (MeteoUrl + "?" + unixDateTime.ToString ());

			while (!webpage.isDone)
				yield return false;
			
			string content = webpage.text;
			Char splitChar = Convert.ToChar (" ");
			dataArray = content.Split (splitChar);
			SplitToValue (dataArray);
			wind.UpdateWind (DirectionWind, SpeedWind, MaxWindSpeed);
			yield return new WaitForSecondsRealtime (10);
		}

	}
		
	public void SplitToValue(String[] DataArray) {
        try {
            MaxWindSpeed = float.Parse(DataArray[32]);
            SpeedWind = float.Parse(DataArray[5]);
            DirectionWind = DataArray[11];
            CurrentTemperature = float.Parse(DataArray[2]);
            CurrentHumidity = float.Parse(DataArray[3]);
        }
        catch
        {
            
        }
    }

}
