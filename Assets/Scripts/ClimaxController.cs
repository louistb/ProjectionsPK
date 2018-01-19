using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimaxController : MonoBehaviour {

    public GameObject[] Climax;
    public int TimeClimax;

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.R))
        {
            var ClimaxObj = Instantiate(Climax[Random.Range(0, Climax.Length)], transform.position, transform.rotation) as GameObject;
			StartCoroutine(DeleteClimaxAfter(TimeClimax, ClimaxObj,"Null"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var ClimaxObj = Instantiate(Climax[0], transform.position, transform.rotation) as GameObject;
            StartCoroutine(DeleteClimaxAfter(TimeClimax, ClimaxObj,"Piston"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var ClimaxObj = Instantiate(Climax[1], transform.position, transform.rotation) as GameObject;
            StartCoroutine(DeleteClimaxAfter(TimeClimax, ClimaxObj,"Null"));
        }

    }

	public IEnumerator DeleteClimaxAfter(int DelayDestroy,GameObject tokill,string ClimaxId)
    {
        yield return new WaitForSeconds(DelayDestroy);
		if (ClimaxId == "Piston") {
			tokill.GetComponent<PistonClimax>().Exit();
			yield return new WaitForSeconds(3);
			Destroy(tokill);
		} else {
			Destroy(tokill);
		}

        

    }
}
