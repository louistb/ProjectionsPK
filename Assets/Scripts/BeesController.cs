using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeesController : MonoBehaviour {

    [Header("Bees System Propolis")]

    [Header("Prefabs")]
    public GameObject FlyingBees;
    public GameObject WalkingBees;

    [Header("Inital Settings")]
    public int NbOfBees;
    public int FlyingPourcentage;
    public bool auto;

    [Header("Burst Settings")]
    public int NbOfBeesBurst;
    public int FlyingPourcentageBurst;

    [Header("Flying")]
    public float MaxRefreshRate;
    public float TimeBeforeDeath;
    public float speed;
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

        Init();
    }

    void Update () {
		
		if(Input.GetKeyDown(KeyCode.I)) {
            Init();
		}

        if (Input.GetKeyDown(KeyCode.B))
        {
            burst();
        }

        if (Input.GetKeyDown (KeyCode.D)) {
			intheScene = GameObject.FindGameObjectsWithTag ("bee");
			foreach (var bees in intheScene) {
				Destroy (bees);
			}
		}
	}


	public void Init() {
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

    public void burst()
    {
        for (var i = 1; i <= NbOfBeesBurst; i++)
        {
            var RandomRate = Random.Range(0, 100);
            var newRotation = Quaternion.Euler(-90f, 0f, 0f);
            var Spawn = SpawnPoints[Random.Range(0, SpawnPoints.Length)];

            if (RandomRate < FlyingPourcentageBurst)
            {
                GameObject NewBee = Instantiate(FlyingBees, Spawn.transform.position, newRotation, transform) as GameObject;
                NewBee.tag = "bee";
            }
            else
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
