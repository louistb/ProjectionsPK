using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingClimax : MonoBehaviour {

    public GameObject FlyingPrefab;
    public Vector3 DestinationFlock;
    public Renderer _FlyZone;
    public GameObject FlyZone;
    public Material trailMat;
	public float startWidth, endWidth,time;

    // Use this for initialization
    void Start() {
        FlyZone = GameObject.Find("Fly_Zone");
        AllToFly();
    }

    void AllToFly()
    {
        FlyZone.GetComponent<Animator>().SetBool("Toflock",true);
        var intheScene = GameObject.FindGameObjectsWithTag("bee");
            foreach (GameObject go in intheScene)
            {
                GameObject newObject = GameObject.Instantiate(FlyingPrefab) as GameObject;
                newObject.transform.parent = go.transform.parent;
                newObject.transform.localPosition = go.transform.localPosition;
                newObject.transform.localRotation = go.transform.localRotation;
                newObject.transform.localScale = go.transform.localScale;
                newObject.AddComponent<TrailRenderer>();
                newObject.tag = "bee";
            	var TrailRendererObj = newObject.GetComponent<TrailRenderer>();
				TrailRendererObj.endWidth = endWidth;
				TrailRendererObj.startWidth = startWidth;
                TrailRendererObj.material = trailMat;
				TrailRendererObj.time = time;
            go.GetComponent<FlyingBees>().KillMe();
                DestroyImmediate(go);
            }
    }
	public void Exit() {
	
	}
}
