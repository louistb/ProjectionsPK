using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutClimax : MonoBehaviour {

	public Animator PistonAnimator;

	void OnEnable () {
		PistonAnimator.SetBool ("Out", true);
	}

}
