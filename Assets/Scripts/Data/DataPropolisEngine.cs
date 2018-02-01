using UnityEngine;
using System.Collections;

public class DataPropolisEngine : MonoBehaviour {
    
   	public OSC osc;
    public float BatteryLevel;
    public bool Wave, Climax;
    public BeesController Bees;

	void Start () {
	   osc.SetAddressHandler( "/hexpress" , OnReceiveHex );
       //osc.SetAddressHandler("/resevoir", OnBatteryLevel);
       osc.SetAddressHandler("/WaveIsActive", OnReceiveWave);
    }


    void OnReceiveHex(OscMessage message) {
        float state = message.GetFloat(0);
        if (state == 1)
            Bees.burst();
    }

    void OnBatteryLevel(OscMessage message)
    {
        float baterryLevel = message.GetFloat(0);
        BatteryLevel = baterryLevel;
    }

    void OnReceiveWave(OscMessage message)
    {
        float state = message.GetFloat(0);
        if (state == 1)
        {
            Wave = true;
        }
    }


}
