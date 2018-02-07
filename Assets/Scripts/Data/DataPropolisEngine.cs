using UnityEngine;
using System.Collections;

public class DataPropolisEngine : MonoBehaviour {
    
   	public OSC osc;
    public float BatteryLevel;
	public int Count10Sec,Last10;
    public bool Wave, Climax;
    public BeesController Bees;
	public float lastClimax,lastWave;

	void Start () {
	   osc.SetAddressHandler( "/hexpress" , OnReceiveHex );
       osc.SetAddressHandler("/WaveIsActive", OnReceiveWave);
	   StartCoroutine (loop10Sec ());
    }

	void Update() {
		lastClimax += Time.deltaTime;
		lastWave += Time.deltaTime;
	}

    void OnReceiveHex(OscMessage message) {
        float state = message.GetFloat(0);
        if (state == 1)
            Bees.burst();
			Count10Sec++;
    }

    void OnBatteryLevel(OscMessage message)
    {
        float baterryLevel = message.GetFloat(0);
        BatteryLevel = baterryLevel;
    }

    void OnReceiveWave(OscMessage message)
    {
		lastClimax = 0;
        float state = message.GetFloat(0);
        if (state == 1)
        {
            Wave = true;
        }
    }

	public IEnumerator loop10Sec() {
		while (true) {
			yield return new WaitForSecondsRealtime (10f);
			Last10 = Count10Sec;
			Count10Sec = 0;
		}
	}
	public void ClearCountDown(int toClear) {
		toClear = 0;
	}
}
