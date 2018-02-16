﻿using UnityEngine;
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
    public bool InAttraction, Dying;
    private string beeId;
    private float time;

    private Hashtable param = new Hashtable();

    void Awake() {
        Controller = GameObject.Find("Bees_System").GetComponent<BeesController>();
        _TimeBeforeDeath = Controller.TimeBeforeDeath - Controller.TimeBeforeDeath / UnityEngine.Random.Range(0, Controller.TimeBeforeDeath / 3);
        _speed = Controller.speed;
        _FlyZone = Controller.FlyZone;
        beeId = Guid.NewGuid().ToString();
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
        iTween.StopByName(beeId);
        var currentPos = transform.position;
        iTween.MoveTo(gameObject,new Vector3(currentPos.x,currentPos.y - 10,currentPos.z), killTime);
        StartCoroutine(KillAfterDelay(killTime));
    }

    public IEnumerator KillAfterDelay(float killbefore)
    {
        yield return new WaitForSecondsRealtime(killbefore);
        iTween.Stop();
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
    }
    public void Resize(int size)
    {
        System.Array.Resize(ref destinations, size);
    }

    public void KillMeClimax()
    {
        iTween.StopByName(beeId);
    }
    public void UpdatePath()
    {
        iTween.StopByName(beeId);

        UpdatePathInPoints();

        param.Remove("path");
        param.Add("path", destinations);

        iTween.MoveTo(gameObject, param);

    }

    public void UpdatePathInPoints()
    {
        for (var i = 0; i < nbOfPointsinPath; i++)
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