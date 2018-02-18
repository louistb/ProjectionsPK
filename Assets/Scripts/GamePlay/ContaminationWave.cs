using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContaminationWave : MonoBehaviour {

    public Animator ContaminationAnimator;
    public GameObject ParticalComtamination;
    public float Duration;
    public BeesController Controller;
	public DataPropolisEngine dataEngine;
	private int ProtectedShields;

    void OnCollisionEnter(Collision collision)
    {
		ProtectedShields = dataEngine.ActivatedShield;

		var chancesToKill = ProtectedShields * (3 / 100);

		var random100 = UnityEngine.Random.Range(0, 100);

		if (chancesToKill < random100) {
	        var touched = collision.transform.gameObject;

	        if (touched.tag == "bee")
	        {
	            if (touched.GetComponent<WalkingBees>() != null)
	            {
	                touched.GetComponent<WalkingBees>().KillMe(3f);
	            }
	            else {

	                touched.GetComponent<FlyingBees>().KillMe(3f);
	            }
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

	public void StartWave() {
		
		StartCoroutine("Wave");
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
        Controller.Init();
        Destroy(toKill);
    }

}
