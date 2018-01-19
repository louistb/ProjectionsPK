using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonClimax : MonoBehaviour {

	Animator PistonAnimator;
	// Use this for initialization
	void Start () {
		PistonAnimator = GetComponent<Animator> ();
	}


	public void Exit() {
		PistonAnimator.SetBool ("Out", true);
	}
}
