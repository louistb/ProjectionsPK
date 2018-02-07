using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhettoCamPlacement : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			var currentposr = transform.position;
			currentposr.x += 0.01f; 
			transform.position = currentposr;
			print(transform.position.x);
		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			var currentposr = transform.position;
			currentposr.x -= 0.01f; 
			transform.position = currentposr;
			print (transform.position.x);
		}
	}
}
