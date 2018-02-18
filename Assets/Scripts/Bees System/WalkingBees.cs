﻿using UnityEngine;
using System.Collections;
using System;


public class WalkingBees : MonoBehaviour {

    public float _MaxRefreshRate, _TimeBeforeDeath, _speed;
    public float _RangeDirectionY, _RangeDirectionX, _RangeDirectionZ;
    public BeesController Controller;
    public float LifeTimer, t, UpdateTime, centerPoint;
    public string beeId;
    private Renderer _FlyZone;
    public Vector3[] destinations;
    public int nbOfPointsinPath = 12;

    public bool InAttraction, Flying,Dying;

    private Hashtable param = new Hashtable();

    public GameObject SelectedWall;

    void Awake() {
        Controller = GameObject.Find("Bees_System").GetComponent<BeesController>();
        _TimeBeforeDeath = Controller.TimeBeforeDeathWalk - Controller.TimeBeforeDeath / UnityEngine.Random.Range(0, Controller.TimeBeforeDeath / 3);
        _speed = Controller.speedWalk;
        beeId = Guid.NewGuid().ToString();
    }

    void Start()
    {

        destinations = new Vector3[nbOfPointsinPath];
        UpdatePathItems();

        param.Add("name", beeId);
        param.Add("oncompletetarget", gameObject);
        param.Add("oncomplete", "UpdatePath");
        param.Add("path", destinations);
        param.Add("speed", _speed);
        param.Add("orienttopath", true);
        param.Add("movetopath", true);
        param.Add("easetype", iTween.EaseType.linear);

        iTween.MoveTo(gameObject, param);
    }

    public void UpdatePath() {

        iTween.StopByName(beeId);

        UpdatePathItems();

        param.Remove("path");
        param.Add("path", destinations);

        iTween.MoveTo(gameObject, param);

    }

    public void UpdatePathItems()
    {
        var RendererWall = SelectedWall.GetComponent<Renderer>();
        for (var i = 0; i < nbOfPointsinPath; i++)
        {
            destinations[i] = RandomPointInBox(RendererWall.bounds.center, RendererWall.bounds.size);
            destinations[i].z = -0.533f;
        }

    }
    public void KillMeClimax()
    {
        iTween.StopByName(beeId);
        Destroy(gameObject);
    }


    public Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3(
           ((UnityEngine.Random.value - 0.5f) * size.x / 3),
           (UnityEngine.Random.value - 0.5f) * size.y / 3,
           (UnityEngine.Random.value - 0.5f) * size.z / 3
        );
    }

}