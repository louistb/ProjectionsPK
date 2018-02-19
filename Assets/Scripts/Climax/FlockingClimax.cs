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
    void OnEnable() {
        FlyZone = GameObject.Find("Fly_Zone");
        AllToFly();
    }

    void AllToFly()
    {
        var intheScene = GameObject.FindGameObjectsWithTag("bee");
            foreach (GameObject go in intheScene)
            {
			if (go.GetComponent<iTween> () != null) {

	                GameObject newObject = GameObject.Instantiate(FlyingPrefab) as GameObject;

	            //newObject.GetComponent<FlyingBees>().Resize(2);
	                newObject.GetComponent<FlyingBees>().nbOfPointsinPath = 2;
	                newObject.GetComponent<FlyingBees>()._speed = 5f;

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

	                if (go.GetComponent<WalkingBees>() != null)
	                    go.GetComponent<WalkingBees>().KillMeClimax();

	                if (go.GetComponent<FlyingBees>() != null)
	                    go.GetComponent<FlyingBees>().KillMeClimax();

	                DestroyImmediate(go);
				}
            }
    }
	public void Exit() {
	
	}
}
