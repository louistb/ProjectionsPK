using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBees : MonoBehaviour {

	public BeesController controllerBee;
	void OnEnable () {
		controllerBee.Init();
	}

}
