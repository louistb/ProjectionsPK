using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimaxBool : MonoBehaviour {

	public BeesController Beecontroller;
	public DataPropolisEngine DataEngine;

	public void OnEnable() {
		Beecontroller.IsClimaxRunning = true;
		DataEngine.IsClimaxRunning = true;
	}

	public void OnDisable() {
		Beecontroller.IsClimaxRunning = false;
		DataEngine.IsClimaxRunning = false;
	}

}
