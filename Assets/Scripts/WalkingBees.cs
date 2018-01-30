﻿using UnityEngine;
using System.Collections;

public class WalkingBees : MonoBehaviour {

    public float _MaxRefreshRate, _TimeBeforeDeath, _speed;
    public float _RangeDirectionY, _RangeDirectionX, _RangeDirectionZ;
    public BeesController Controller;

    public float LifeTimer, t, UpdateTime, centerPoint;
    public Vector3 CurrentPos, DestinationPos, AtractPos, DeathPoint;

    private Renderer _FlyZone;

    public bool InAttraction, Flying,Dying;

    private Quaternion CurrentRotation, DestinatopmRotation;
    private Vector3 FallPosition;

    public GameObject SelectedWall;

    void Awake() {
        Controller = GameObject.Find("Bees_System").GetComponent<BeesController>();

        _TimeBeforeDeath = Controller.TimeBeforeDeathWalk - Controller.TimeBeforeDeath / Random.Range(0, Controller.TimeBeforeDeath / 3);
        _speed = Controller.speedWalk;
        _FlyZone = Controller.FlyZone;
        _MaxRefreshRate = Controller.MaxRefreshRateWalk;
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

    private void DirectionUpdateFly()
    {
        UpdateTime = NewRandUpdate();
        t = 0;
        DestinationPos = RandomPointInBox(_FlyZone.bounds.center, _FlyZone.bounds.size);
    }

    void OnCollisionEnter(Collision collision)
    {
        DirectionUpdate();
    }

    private void DirectionUpdate() {

        UpdateTime = NewRandUpdate();
        t = 0;
        var RendererWall = SelectedWall.GetComponent<Renderer>();
        DestinationPos = RandomPointInBox(RendererWall.bounds.center, RendererWall.bounds.size);
        DestinationPos.z = -0.3595f;



    }

    private void FixedUpdate()
    {
            Walk();
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

        var directionVector = Vector3.Lerp(CurrentPos, DestinationPos, t * _speed);

        transform.position = directionVector;

        transform.LookAt(DestinationPos,Vector3.back);

        //KILLING WHEN TIME IS OVER
        LifeTimer += Time.deltaTime;

        if (LifeTimer >= (_TimeBeforeDeath - 10))
        {
            //print("diying");
            FallPosition = new Vector3(CurrentPos.x, CurrentPos.y - 2, CurrentPos.z);
            DestinationPos = FallPosition;
            Dying = true;

            if (LifeTimer >= _TimeBeforeDeath)
            {
                Destroy(gameObject);
            }
        }
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