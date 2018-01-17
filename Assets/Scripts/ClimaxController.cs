using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimaxController : MonoBehaviour {

    public GameObject[] Climax;
    public int TimeClimax;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.R))
        {
            var ClimaxObj = Instantiate(Climax[Random.Range(0, Climax.Length)], transform.position, transform.rotation) as GameObject;
            StartCoroutine(DeleteClimaxAfter(TimeClimax, ClimaxObj));
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var ClimaxObj = Instantiate(Climax[0], transform.position, transform.rotation) as GameObject;
            StartCoroutine(DeleteClimaxAfter(TimeClimax, ClimaxObj));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var ClimaxObj = Instantiate(Climax[1], transform.position, transform.rotation) as GameObject;
            StartCoroutine(DeleteClimaxAfter(TimeClimax, ClimaxObj));
        }

    }

    public IEnumerator DeleteClimaxAfter(int DelayDestroy,GameObject tokill)
    {
        yield return new WaitForSeconds(DelayDestroy);
        Destroy(tokill);

    }
}
