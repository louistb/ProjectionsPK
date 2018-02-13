//using UnityEngine;
//using System.Collections;
//using System;

//public class MoveSample : MonoBehaviour {

//    public Vector3[] destinations;
//    public int nbOfPointsinPath = 12;
//    public Renderer flyzone;
//    Hashtable param = new Hashtable();

//    void Start()
//    {
//        destinations = new Vector3[nbOfPointsinPath];

//        param.Add("oncompletetarget", gameObject);
//        param.Add("oncomplete", "UpdatePath");
//        param.Add("path", destinations);
//        param.Add("speed",100);
//        param.Add("easetype", iTween.EaseType.linear);

//        iTween.MoveTo(gameObject, param);
//    }

//    public void UpdatePath()
//    {
//        iTween.Stop();

//        for (var i = 0; i < nbOfPointsinPath; i++)
//        {
//            destinations[i] = RandomPointInBox(flyzone.bounds.center, flyzone.bounds.size);
//        }

//        param.Remove("path");
//        param.Add("path", destinations);
//        iTween.MoveTo(gameObject, param);

//    }

//    public Vector3 RandomPointInBox(Vector3 center, Vector3 size)
//    {
//        return center + new Vector3(
//           ((UnityEngine.Random.value - 0.5f) * size.x),
//           (UnityEngine.Random.value - 0.5f) * size.y,
//           (UnityEngine.Random.value - 0.5f) * size.z
//        );
//    }

//}

