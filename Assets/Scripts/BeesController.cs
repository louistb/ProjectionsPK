using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeesController : MonoBehaviour {

    [Header("Bees System Propolis")]

    [Header("Prefabs")]
    public GameObject FlyingBees;
    public GameObject WalkingBees;

    [Header("General Settings")]
    public int NbOfBees;
    public int FlyingPourcentage;
    public bool auto;

    [Header("Flying")]
    public float MaxRefreshRate;
    public float TimeBeforeDeath;
    public float speed;
    public float MaxFlyRate;
    public float RangeDirectionY;
    public float RangeDirectionX;
    public float RangeDirectionZ;
    public Renderer FlyZone;

    [Header("Walking")]
    public float MaxRefreshRateWalk;
    public float TimeBeforeDeathWalk;
    public float speedWalk;
  


    //PRIVATE 
    private GameObject[] intheScene;
    private GameObject[] Walls;
    private GameObject[] SpawnPoints;

    private void Start()
    {
        if(auto == true)
        {
            InvokeRepeating("NewBees", 0f, TimeBeforeDeathWalk);
        }

        Walls = GameObject.FindGameObjectsWithTag("ZoneWalk");
        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    void Update () {
		
		if(Input.GetKeyDown(KeyCode.Space)) {
			NewBees();
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			intheScene = GameObject.FindGameObjectsWithTag ("bee");
			print (intheScene.Length);
		}

        if (Input.GetKeyDown(KeyCode.G))
        {
            intheScene = GameObject.FindGameObjectsWithTag("bee");
            foreach(var bee in intheScene)
            {
                if(bee.GetComponent<FlyingBees>() == null)
                {
                    print(bee);
                    bee.GetComponent<WalkingBees>().Flying = true;
                }
            }
        }

        if (Input.GetKeyDown (KeyCode.D)) {
			intheScene = GameObject.FindGameObjectsWithTag ("bee");
			foreach (var bees in intheScene) {
				Destroy (bees);
			}
		}
	}
	void NewBees() {
        for (var i = 1; i <= NbOfBees; i++) {
            var RandomRate = Random.Range(0, 100);
            var newRotation = Quaternion.Euler(-90f, 0f, 0f);
            var Spawn = SpawnPoints[Random.Range(0, SpawnPoints.Length)];

            if (RandomRate < FlyingPourcentage)
            {
                GameObject NewBee = Instantiate(FlyingBees, Spawn.transform.position, newRotation, transform) as GameObject;
                NewBee.tag = "bee";
            } else
            {
                var selectedWall = Walls[Random.Range(0, Walls.Length)];
                GameObject NewBee = Instantiate(WalkingBees, Spawn.transform.position, newRotation, transform) as GameObject;
                NewBee.GetComponent<WalkingBees>().SelectedWall = selectedWall;
                NewBee.GetComponent<WalkingBees>().DeathPoint = Spawn.transform.position;
                NewBee.tag = "bee";
            }
		}
	}
			
}
