using UnityEngine;
using System.Collections;

public class FlyingBees : MonoBehaviour {

	public float _MaxRefreshRate,_TimeBeforeDeath,_speed;
	public float _RangeDirectionY,_RangeDirectionX,_RangeDirectionZ,_MaxFlyRate;
	public BeesController Controller;

	public float LifeTimer,t,UpdateTime;
	public Vector3 CurrentPos, DestinationPos,AtractPos,FallPosition;
	public bool InAttraction,Dying;
    public FlockingClimax flocking;

    private Transform center;
    private Renderer _FlyZone;
    private Quaternion _facing;

	void Awake() {
		Controller = GameObject.Find ("Bees_System").GetComponent<BeesController>();
        center = GameObject.Find("Bees_System").transform;
        _MaxRefreshRate = Controller.MaxRefreshRate;
		_TimeBeforeDeath = Controller.TimeBeforeDeath - Controller.TimeBeforeDeath / Random.Range(0,Controller.TimeBeforeDeath/3);
		_speed = Controller.speed;
        _FlyZone = Controller.FlyZone;

    }

	void Start () 
	{
		UpdateTime = NewRandUpdate();
        DirectionUpdateFly();
        _facing = transform.rotation;
    }
		

	void OnCollisionEnter(Collision collision)
	{
        DirectionUpdateFly();
	}

    private void DirectionUpdateFly()
    {
            UpdateTime = NewRandUpdate();
            if (InAttraction == false)
            {
			t = 0;
			DestinationPos = RandomPointInBox(_FlyZone.bounds.center, _FlyZone.bounds.size);
            } else
            {
            }
    }

    public void FixedUpdate()
    {
            Fly();   
	}


    public void Fly()
    {
        //GLOBAL 
        t += Time.deltaTime;

        if (InAttraction == true)
        {
            t = 0;
            var preRandom = GameObject.Find("ClimaxFlocking(Clone)").GetComponent<FlockingClimax>().DestinationFlock;
            var randomFlock = new Vector3(preRandom.x + Random.Range(-2f, 2f), preRandom.y + Random.Range(-2f, 2f), preRandom.z + Random.Range(-2f, 2f));
            DestinationPos = randomFlock;

            transform.position = randomFlock;

            transform.LookAt(DestinationPos, Vector3.right);

        }
        else { 

            if (t >= UpdateTime){
               DirectionUpdateFly();
            }   

        //LERP
        CurrentPos = transform.position;

        var directionVector = Vector3.Slerp(CurrentPos, DestinationPos, t * t);

        transform.position = directionVector;

        transform.LookAt(DestinationPos, Vector3.right);

        LifeTimer += Time.deltaTime;

		if (LifeTimer >= (_TimeBeforeDeath - 10))
		{
			FallPosition = new Vector3(CurrentPos.x, CurrentPos.y - 2, CurrentPos.z);
			DestinationPos = FallPosition;
			Dying = true;

			if (LifeTimer >= _TimeBeforeDeath)
			{
				Destroy(gameObject);
			}
		}
        }
    }

    public float NewRandUpdate() {
	    return Random.Range(0, _MaxRefreshRate);
	}

    private Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {

        return center + new Vector3(
           ((Random.value - 0.5f) * size.x),
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }

}