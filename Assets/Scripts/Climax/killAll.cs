using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killAll : MonoBehaviour {
    private GameObject[] intheScene;

    void OnEnable() {
        intheScene = GameObject.FindGameObjectsWithTag("bee");

        foreach (var bees in intheScene)
        {

            if (bees.GetComponent<WalkingBees>() != null)
                bees.GetComponent<WalkingBees>().KillMeClimax();

            if (bees.GetComponent<FlyingBees>() != null)
                bees.GetComponent<FlyingBees>().KillMeClimax();

            Destroy(bees);
        }
    }
	

}
