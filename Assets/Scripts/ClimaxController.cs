using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimaxController : MonoBehaviour {

    public GameObject[] Climax;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.C))
        {
            var ClimaxObj = Instantiate(Climax[Random.Range(0, Climax.Length)], transform.position, transform.rotation) as GameObject;
        }
	}
}
