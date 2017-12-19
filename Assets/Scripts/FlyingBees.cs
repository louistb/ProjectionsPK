using UnityEngine;
using System.Collections;

public class FlyingBees : MonoBehaviour {

	public float _MaxRefreshRate,_TimeBeforeDeath,_speed;
	public float _RangeDirectionY,_RangeDirectionX,_RangeDirectionZ,_MaxFlyRate;
	public BeesController Controller;

	public float LifeTimer,t,UpdateTime;
	public Vector3 CurrentPos, DestinationPos,AtractPos,FallPosition;
	public bool InAttraction,Dying;


    private Transform center;
    private Renderer _FlyZone;
    private Quaternion _facing;

	void Awake() {
		Controller = GameObject.Find ("Bees_System").GetComponent<BeesController>();
        center = GameObject.Find("Bees_System").transform;
        _MaxRefreshRate = Controller.MaxRefreshRate;
		_TimeBeforeDeath = Controller.TimeBeforeDeath - Controller.TimeBeforeDeath / Random.Range(0,Controller.TimeBeforeDeath/3);
		_speed = Controller.speed;
		_RangeDirectionX = Controller.RangeDirectionX;
		_RangeDirectionY = Controller.RangeDirectionY;
		_RangeDirectionZ = Controller.RangeDirectionZ;
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
		print (collision);
        DirectionUpdateFly();
	}

    private void DirectionUpdateFly()
    {
        UpdateTime = NewRandUpdate();
        t = 0;
        DestinationPos = RandomPointInBox(_FlyZone.bounds.center, _FlyZone.bounds.size);
    }

	public void FixedUpdate() 
	{
        Fly();
	}

    public void Fly()
    {
        //GLOBAL 
        t += Time.deltaTime;

        if (t >= UpdateTime)
        {
            DirectionUpdateFly();
        }

        //LERP
        CurrentPos = transform.position;

        var directionVector = Vector3.Lerp(CurrentPos, DestinationPos, t * _speed);

        transform.position = directionVector;

        var rotation = Quaternion.LookRotation(directionVector);
        rotation *= _facing;
        transform.rotation = rotation;

        //KILLING WHEN TIME IS OVER
        LifeTimer += Time.deltaTime;

		if (LifeTimer >= (_TimeBeforeDeath - 4))
		{
			//print("diying");
			FallPosition = new Vector3(CurrentPos.x, CurrentPos.y - 2, CurrentPos.z);
			DestinationPos = FallPosition;
			Dying = true;
			//            var toChangeRed = GetComponentsInChildren<MeshRenderer>();

			//            foreach (var bees in toChangeRed)
			//            {
			//                bees.material.color = new Color(255, 0, 0);
			//            }

			if (LifeTimer >= _TimeBeforeDeath)
			{
				Destroy(gameObject);
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