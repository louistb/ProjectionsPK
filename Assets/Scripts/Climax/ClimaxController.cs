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
            director.Stop();
            director.Play();
        }

    }
}
