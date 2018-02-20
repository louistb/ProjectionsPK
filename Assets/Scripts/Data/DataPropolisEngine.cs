using UnityEngine;
using System.Collections;

public class DataPropolisEngine : MonoBehaviour {
    
   	public OSC osc;
    public float BatteryLevel;
	public int Count10Sec,Last10,ActivatedShield;
	public bool Wave;
    public BeesController Bees;
	public float lastClimax,lastWave;
	public ContaminationWave WaveController;
	public ClimaxController ClimaxController;
	public ReservoirController Reservoir;
	public bool IsClimaxRunning;

	private bool oneTimeWave,oneTimeClimax;

	void Start () {
	   osc.SetAddressHandler( "/hexpress" , OnReceiveHex );
	   osc.SetAddressHandler( "/cleanser" , OnReceiveCleanser );
       osc.SetAddressHandler("/WaveIsActive", OnReceiveWave);
	   osc.SetAddressHandler("/ShieldsCount", OnReceiveShieldsCount);
	   osc.SetAddressHandler("/startClimax", OnReceiveClimax);
	   osc.SetAddressHandler("/reservoir", OnBatteryLevel);

	   StartCoroutine (loop10Sec ());
    }

	void Update() {
		lastClimax += Time.deltaTime;
		lastWave += Time.deltaTime;
	}

    void OnReceiveHex(OscMessage message) {
		
        float state = message.GetFloat(0);
		if (state == 1) {
            Bees.burst("Walking");
			Count10Sec++;
		}
	}

	void OnReceiveCleanser(OscMessage message) {
		float state = message.GetFloat(0);

		if (state == 1){
			Bees.burst("FlyingBlue");
			Count10Sec++;
		}
	}

	void OnBatteryLevel(OscMessage message)
    {
        float baterryLevel = message.GetFloat(0);
        BatteryLevel = baterryLevel;
		Reservoir.setMaxParticle(baterryLevel);
    }

    void OnReceiveWave(OscMessage message)
    {
		if (!IsClimaxRunning) {
			lastWave = 0;

			float state = message.GetFloat (0);

			if (state == 1 && !oneTimeWave) {
				oneTimeWave = true;
				print ("Wave Started");
				WaveController.StartWave ();
			}

			if (state == 0) {
				oneTimeWave = false;
			}
		}
    }

	public void OnReceiveClimax(OscMessage message) {
		lastClimax = 0;

		float state = message.GetInt(0);

		if (state == 1 && !oneTimeClimax) {
			oneTimeClimax = true;
			print ("Climax Started");
			WaveController.KillWave();
			ClimaxController.StartClimax();
		}

		if (state == 0) {
			oneTimeClimax = false;
		}
	}

	void OnReceiveShieldsCount(OscMessage message)
	{
		ActivatedShield = message.GetInt(0);
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
