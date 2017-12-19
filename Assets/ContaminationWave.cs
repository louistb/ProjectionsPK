using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContaminationWave : MonoBehaviour {

    public Animator ContaminationAnimator;

    void OnCollisionEnter(Collision collision)
    {
        var touched = collision.transform.gameObject;
        if (touched.tag == "bee")
        {
            if (touched.GetComponent<WalkingBees>() != null)
            {
                var touchedWalk = touched.GetComponent<WalkingBees>();

                touchedWalk.Dying = true;
                touchedWalk.LifeTimer = touchedWalk._TimeBeforeDeath - 10;
            }
            else {

                var touchedFly = touched.GetComponent<FlyingBees>();
                touchedFly.Dying = true;
                touchedFly.LifeTimer = touchedFly._TimeBeforeDeath - 10;
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine("Wave");
        }
    }

    public IEnumerator Wave()
    {
        ContaminationAnimator.SetBool("Infection", true);
        yield return new WaitForSeconds(1f);
        ContaminationAnimator.SetBool("Infection", false);
    }

}
