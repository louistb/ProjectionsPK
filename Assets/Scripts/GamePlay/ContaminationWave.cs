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
    private string currentId;
    private List<string> listTouched;

    private void Start()
    {
        listTouched = new List<string>();
    }

    void OnCollisionEnter(Collision collision)
    {
        var touched = collision.transform.gameObject;

        currentId = "";

        //ProtectedShields = dataEngine.ActivatedShield;
        ProtectedShields = 0;

        if (touched.GetComponent<WalkingBees>() != null)
        {
            currentId = touched.GetComponent<WalkingBees>().beeId;

        } else if (touched.GetComponent<FlyingBees>() != null)
        {
            currentId = touched.GetComponent<FlyingBees>().beeId;
        }


        if (currentId != ""){

            var lerp = (ProtectedShields * 1 )/ 3;

            var revesedChance = Mathf.Lerp(10f, 0f, lerp);

            var random100 = UnityEngine.Random.Range(0, 100);  
                
                if (listTouched.Contains(currentId) == false) {
                    if (random100 <= revesedChance) {
                            if (touched.tag == "bee") { 
                            print("kiiling" + touched.gameObject);
                            print(touched.GetComponent<WalkingBees>());
                            print(touched.GetComponent<FlyingBees>());

                            if (touched.gameObject.GetComponent<WalkingBees>() != null)
                            {
                                touched.GetComponent<WalkingBees>().KillMe(3f);
                                listTouched.Add(touched.GetComponent<WalkingBees>().beeId);

                            }


                        if (touched.gameObject.GetComponent<FlyingBees>() != null)
                            { 
                                touched.GetComponent<FlyingBees>().KillMe(3f);
                                listTouched.Add(touched.GetComponent<FlyingBees>().beeId);
                        }

                        }
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

        listTouched.Clear();

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
