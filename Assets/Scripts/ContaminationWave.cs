using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContaminationWave : MonoBehaviour {

    public Animator ContaminationAnimator;
    public GameObject ParticalComtamination;
    public float Duration;

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

                touched.GetComponent<FlyingBees>().KillMe(20f);
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
        var WaveParticle = Instantiate(ParticalComtamination, transform.position,Quaternion.Euler(0f,90f,0f)) as GameObject;
		WaveParticle.transform.parent = GameObject.Find ("Contamination").transform;

        var System = WaveParticle.GetComponent<ParticleSystem>();
        var ps = System.main;

        System.Stop();
        ps.duration = Duration;
        System.Play();
            
        StartCoroutine(KillAfterWave(WaveParticle, Duration));
        ContaminationAnimator.SetBool("Infection", true);
        yield return new WaitForSeconds(1f);
        ContaminationAnimator.SetBool("Infection", false);
    }

    public IEnumerator KillAfterWave(GameObject toKill,float delay)
    {
        yield  return new WaitForSeconds(delay + 5f);
        Destroy(toKill);
    }

}
