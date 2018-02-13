using UnityEngine;
using System.Collections;


public class FlyingBees : MonoBehaviour
{

    public Vector3[] destinations;
    public int nbOfPointsinPath = 12;
    public float _speed, _TimeBeforeDeath;
    public Renderer _FlyZone;
    public BeesController Controller;
    public float LifeTimer;
    public bool InAttraction, Dying;
    private string beeId;
    private float time;

    private Hashtable param = new Hashtable();

    void Awake() {
        Controller = GameObject.Find("Bees_System").GetComponent<BeesController>();
        _TimeBeforeDeath = Controller.TimeBeforeDeath - Controller.TimeBeforeDeath / Random.Range(0, Controller.TimeBeforeDeath / 3);
        _speed = Controller.speed;
        _FlyZone = Controller.FlyZone;
        beeId = "bee" + UnityEngine.Random.Range(0, 20000).ToString() + UnityEngine.Random.Range(0, 20000).ToString();


    }

    void Start()
    {
        destinations = new Vector3[nbOfPointsinPath];
        param.Add("name", beeId);
        param.Add("oncompletetarget", gameObject);
        param.Add("oncomplete", "UpdatePath");
        param.Add("path", destinations);
        param.Add("speed", _speed);
        param.Add("orienttopath", true);

        param.Add("easetype", iTween.EaseType.linear);

        iTween.MoveTo(gameObject, param);

    }

    public void KillMe()
    {
        iTween.StopByName(beeId);
    }

    public void UpdatePath()
    {
        iTween.StopByName(beeId);

        for (var i = 0; i < nbOfPointsinPath; i++)
        {
            destinations[i] = RandomPointInBox(_FlyZone.bounds.center, _FlyZone.bounds.size);
        }

        param.Remove("path");
        param.Add("path", destinations);

        iTween.MoveTo(gameObject, param);

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