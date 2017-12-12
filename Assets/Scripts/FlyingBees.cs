using UnityEngine;
using System.Collections;

public class FlyingBees : MonoBehaviour {

	public float _MaxRefreshRate,_TimeBeforeDeath,_speed;
	public float _RangeDirectionY,_RangeDirectionX,_RangeDirectionZ,_MaxFlyRate;
	public BeesController Controller;

	public float LifeTimer,t,UpdateTime;
	public Vector3 CurrentPos, DestinationPos,AtractPos;
	public bool InAttraction;


    private Transform center;
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
        if (InAttraction = true)
        {
            DestinationPos.x = center.position.x + Random.Range(-_RangeDirectionX / 10, _RangeDirectionX / 10);
            DestinationPos.y = center.position.y + Random.Range(-_RangeDirectionY / 10, _RangeDirectionY / 10);
            DestinationPos.z = center.position.z + Random.Range(0, -_RangeDirectionZ / 10);
        }
        else
        {
            DestinationPos.x = center.position.x + Random.Range(-_RangeDirectionX, _RangeDirectionX);
            DestinationPos.y = center.position.y + Random.Range(-_RangeDirectionY, _RangeDirectionY);
            DestinationPos.z = center.position.z + Random.Range(0, -_RangeDirectionZ);
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

        if (t >= UpdateTime)
        {
            DirectionUpdateFly();
        }

        //LERP
        CurrentPos = transform.position;

        var directionVector = Vector3.Slerp(CurrentPos, DestinationPos, t * _speed);

        transform.position = directionVector;

        var rotation = Quaternion.LookRotation(directionVector);
        rotation *= _facing;
        transform.rotation = rotation;

        //KILLING WHEN TIME IS OVER
        LifeTimer += Time.deltaTime;

        if (LifeTimer >= _TimeBeforeDeath)
        {
            Destroy(gameObject);
        }
    }

    public float NewRandUpdate() {
	    return Random.Range(0, _MaxRefreshRate);
	}
		
}