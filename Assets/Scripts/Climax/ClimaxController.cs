using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ClimaxController : MonoBehaviour {

    public PlayableDirector director;
	public ContaminationWave wave;

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.C))
        {
			StartClimax();
			wave.KillWave ();
        }

    }

	public void StartClimax() {
		director.Stop();
		director.Play();
	}
}
