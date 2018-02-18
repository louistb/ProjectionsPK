using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ClimaxController : MonoBehaviour {

    public PlayableDirector director;

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.C))
        {
			StartClimax();
        }

    }

	public void StartClimax() {
		director.Stop();
		director.Play();
	}
}
