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
	private Coroutine waveCoroutine;
	private GameObject ParticleSystem;

    void OnCollisionEnter(Collision collision)
    {
		ProtectedShields = dataEngine.ActivatedShield;

		var chancesToKill = ProtectedShields * (30 / 3);
		var lerp = ProtectedShields * (1 / 3);

		var revesedChance = Mathf.Lerp(30f,0f,lerp);

		var random100 = UnityEngine.Random.Range(0, 100);

		print (random100);

		print (revesedChance);

		if (revesedChance > random100) {
				print ("touched");
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
			StartWave();
        }
    }

	public void StartWave() {
		
		if (waveCoroutine != null)
			StopCoroutine(waveCoroutine);
		
		waveCoroutine = StartCoroutine("Wave");
	}

	public IEnumerator Wave()
    {
		ParticleSystem = Instantiate(ParticalComtamination, transform.position,Quaternion.Euler(0f,90f,0f)) as GameObject;
		ParticleSystem.transform.parent = GameObject.Find ("Contamination").transform;

		var System = ParticleSystem.GetComponent<ParticleSystem>();
        var ps = System.main;

        System.Stop();
        ps.duration = Duration;
        System.Play();
            
		StartCoroutine(KillAfterWave(ParticleSystem, Duration));
        
		ContaminationAnimator.SetBool("Infection", true);
        yield return new WaitForSeconds(1f);
        ContaminationAnimator.SetBool("Infection", false);
    }

    public IEnumerator KillAfterWave(GameObject toKill,float delay)
    {
        yield  return new WaitForSeconds(delay);
		Destroy(ParticleSystem);
        Controller.Init();
        Destroy(toKill);
    }

	public void KillWave() {
		if (waveCoroutine != null)
			StopCoroutine(waveCoroutine);
			ContaminationAnimator.Play("hiden");
			Destroy(ParticleSystem);
		
	}
}
