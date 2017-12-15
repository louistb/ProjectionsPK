using UnityEngine;
using System.Collections;

public class WalkingBees : FlyingBees {

    public float _MaxRefreshRate, _TimeBeforeDeath, _speed;
    public float _RangeDirectionY, _RangeDirectionX, _RangeDirectionZ, _MaxFlyRate;
    public BeesController Controller;

    public float LifeTimer, t, UpdateTime, centerPoint;
    public Vector3 CurrentPos, DestinationPos, AtractPos, DeathPoint;
    public bool InAttraction, Flying,Dying;

    private Quaternion CurrentRotation, DestinatopmRotation;
    private Vector3 FallPosition;

    public GameObject SelectedWall;

    void Awake() {
        Controller = GameObject.Find("Bees_System").GetComponent<BeesController>();

        _MaxRefreshRate = Controller.MaxRefreshRate;
        _TimeBeforeDeath = Controller.TimeBeforeDeathWalk - Controller.TimeBeforeDeath / Random.Range(0, Controller.TimeBeforeDeath / 3);
        _speed = Controller.speedWalk;
        _RangeDirectionX = Controller.RangeDirectionX;
        _RangeDirectionY = Controller.RangeDirectionY;
        _RangeDirectionZ = Controller.RangeDirectionZ;

        Flying = false;
        Dying = false;
    }

    void Start()
    {
        UpdateTime = NewRandUpdate();
        var RendererWall = SelectedWall.GetComponent<Renderer>();
        centerPoint = Random.Range(-1.4f, 1.4f);
        DirectionUpdate();
    }


    void OnCollisionEnter(Collision collision)
    {
        print(collision);
        DirectionUpdate();
    }

    private void DirectionUpdate() {

        UpdateTime = NewRandUpdate();
        t = 0;
        var RendererWall = SelectedWall.GetComponent<Renderer>();
        DestinationPos = RandomPointInBox(RendererWall.bounds.center, RendererWall.bounds.size);
        DestinationPos.z = -0.02f;


        float directonValue = DestinationPos.x - transform.position.x;
        var newRotation = Quaternion.Euler(directonValue * 360, -90f, 90f);
        DestinatopmRotation = newRotation;



    }

    private void FixedUpdate()
    {
        if (Flying == true)
        {
            Fly();
        } else
        {
            Walk();
        }
    }

    public void Walk()
    {
        //GLOBAL 
        t += Time.deltaTime;

        if (t >= UpdateTime)
        {
            if(Dying != true)
            {
                DirectionUpdate();
            }
        }

        //LERP
        CurrentPos = transform.position;
        CurrentRotation = transform.rotation;

        var directionVector = Vector3.Lerp(CurrentPos, DestinationPos, t * _speed);
        var directionAngfle = Quaternion.Lerp(CurrentRotation, DestinatopmRotation, t * (_speed * 4));

        transform.position = directionVector;
        transform.rotation = directionAngfle;

        //KILLING WHEN TIME IS OVER
        LifeTimer += Time.deltaTime;

        if (LifeTimer >= (_TimeBeforeDeath - 4))
        {
            //print("diying");
            FallPosition = new Vector3(CurrentPos.x, CurrentPos.y - 6, CurrentPos.z);
            DestinationPos = FallPosition;
            Dying = true;
            var toChangeRed = GetComponentsInChildren<MeshRenderer>();

            foreach (var bees in toChangeRed)
            {
                bees.material.color = new Color(255, 0, 0);
            }

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
           ((Random.value - 0.5f) * size.x) + centerPoint,
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }

}