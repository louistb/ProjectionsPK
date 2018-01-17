using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingClimax : MonoBehaviour {

    public GameObject FlyingPrefab;
    public Vector3 DestinationFlock;
    public Renderer _FlyZone;

    // Use this for initialization
    void Start() {
        _FlyZone = GameObject.Find("Fly_Zone").GetComponent<Renderer>();
        AllToFly();
        InvokeRepeating("UpdateDirection", 0f, 3f);
    }

    void AllToFly()
    {
            var intheScene = GameObject.FindGameObjectsWithTag("bee");
            foreach (GameObject go in intheScene)
            {
                GameObject newObject = GameObject.Instantiate(FlyingPrefab) as GameObject;
                newObject.transform.parent = go.transform.parent;
                newObject.transform.localPosition = go.transform.localPosition;
                newObject.transform.localRotation = go.transform.localRotation;
                newObject.transform.localScale = go.transform.localScale;
                newObject.GetComponent<FlyingBees>().InAttraction = true;

                DestroyImmediate(go);
            }
    }
	// Update is called once per frame
	void UpdateDirection() {
        DestinationFlock = RandomPointInBox(_FlyZone.bounds.center, _FlyZone.bounds.size);
    }

    private Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {

        return center + new Vector3(
           (Random.value -0.5f) * size.x,
           (Random.value -0.5f) * size.y ,
           (Random.value -0.5f) * size.z
        );
    }
}
