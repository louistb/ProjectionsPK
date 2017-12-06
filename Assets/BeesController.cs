using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeesController : MonoBehaviour {

	public GameObject bees;
	public int NbOfBees;
	public float MaxRefreshRate,TimeBeforeDeath,speed, MaxFlyRate;
	public float RangeDirectionY,RangeDirectionX,RangeDirectionZ;


	private GameObject[] intheScene;

	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			NewBees();
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			intheScene = GameObject.FindGameObjectsWithTag ("bee");
			print (intheScene.Length);
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			intheScene = GameObject.FindGameObjectsWithTag ("bee");
			foreach (var bees in intheScene) {
				Destroy (bees);
			}
		}
		if(Input.GetMouseButtonDown(0)) {
			intheScene = GameObject.FindGameObjectsWithTag ("bee");
			foreach (var bees in intheScene) {
				var beesScript = bees.GetComponent<Bees>();
				beesScript.InAttraction = true;
			}
		}

		if(Input.GetMouseButton(0)) {
			foreach (var bees in intheScene) {
				if (bees != null) {
				var beesScript = bees.GetComponent<Bees>();
				var mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition).direction;
				beesScript.AtractPos = new Vector3 (mouseRay.x, mouseRay.y, 0f);
				}
			}
		}

		if(Input.GetMouseButtonUp(0)) {
			foreach (var bees in intheScene) {
				if (bees != null) {
				var beesScript = bees.GetComponent<Bees>();
				beesScript.InAttraction = false;
				}
			}
		}

	}
	void NewBees() {
		for (var i = 0; i <= NbOfBees; i++) {
			var newRotation =  Quaternion.Euler(-90f, 0f, 0f);
			GameObject NewBee = Instantiate(bees, transform.position,newRotation,transform) as GameObject;
			NewBee.tag = "bee";
		}
	}
			
}
