using UnityEngine;
using System.Collections;
using System;

public class FlyingBees : MonoBehaviour
{

    public Vector3[] destinations;
    public int nbOfPointsinPath = 12;
    public float _speed, _TimeBeforeDeath;
    public Renderer _FlyZone;
    public BeesController Controller;
    public float LifeTimer;
	public bool Alive;
    public string beeId;
    private float time;

    private Hashtable param = new Hashtable();
	private Hashtable paramDrop = new Hashtable();

    void Awake() {
        Controller = GameObject.Find("Bees_System").GetComponent<BeesController>();
        _TimeBeforeDeath = Controller.TimeBeforeDeath - Controller.TimeBeforeDeath / UnityEngine.Random.Range(0, Controller.TimeBeforeDeath / 3);
        _speed = Controller.speed;
        _FlyZone = Controller.FlyZone;
        beeId = Guid.NewGuid().ToString();
		gameObject.name = beeId;
		Alive = true;
    }

    void Start()
    {
        destinations = new Vector3[nbOfPointsinPath];
        UpdatePathInPoints();

        param.Add("name", beeId);
        param.Add("oncompletetarget", gameObject);
        param.Add("oncomplete", "UpdatePath");
        param.Add("path", destinations);
        param.Add("speed", _speed);
        param.Add("orienttopath", true);

        param.Add("easetype", iTween.EaseType.linear);

        iTween.MoveTo(gameObject, param);

    }
    public void KillMe(float killTime)
    {
		if (Alive == true) {
			if (gameObject.GetComponent<iTween>()!= null) {
		        iTween.StopByName(beeId);
		        var currentPos = transform.position;
				paramDrop.Remove ("name");
				paramDrop.Add("name", beeId);
				paramDrop.Remove("position");
				paramDrop.Add("position",new Vector3(currentPos.x,-4f,currentPos.z));
				paramDrop.Remove("time");
				paramDrop.Add("time",killTime - 2f);

				iTween.MoveTo(gameObject,paramDrop);

		        StartCoroutine(KillAfterDelay(killTime));
			}
		}
		Alive = false;
    }

    public IEnumerator KillAfterDelay(float killbefore)
    {
        yield return new WaitForSecondsRealtime(killbefore + 2f);
		if (gameObject.GetComponent<iTween>() != null) {
			iTween.StopByName(beeId);
		}
		Destroy(gameObject);
        
    }

    public void KillMeClimax()
    {
		if (gameObject.GetComponent<iTween>()!= null) {
			iTween.StopByName (beeId);
		}
    }

    public void UpdatePath()
    {
		if (gameObject.GetComponent<iTween>()!= null) {
	        iTween.StopByName(beeId);

	        UpdatePathInPoints();

	        param.Remove("path");
	        param.Add("path", destinations);

	        iTween.MoveTo(gameObject, param);
		}

    }

    public void UpdatePathInPoints()
    {
        for (var i = 0; i < destinations.Length; i++)
        {
            destinations[i] = RandomPointInBox(_FlyZone.bounds.center, _FlyZone.bounds.size);
        }

    }

    public Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3(
           ((UnityEngine.Random.value - 0.5f) * size.x),
           (UnityEngine.Random.value - 0.5f) * size.y,
           (UnityEngine.Random.value - 0.5f) * size.z
        );
    }

}