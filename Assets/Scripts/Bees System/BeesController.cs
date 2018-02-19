using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeesController : MonoBehaviour {

    [Header("Bees System Propolis")]

    [Header("Prefabs")]
    public GameObject FlyingBees;
    public GameObject WalkingBees;
    public GameObject FlyingBlue;

    [Header("Inital Settings")]
    public int NbOfBees;
    public int FlyingPourcentage;

    [Header("Burst Walk")]
    public int NbOfBeesBurstWalk;

    [Header("Burst Fly")]
    public int NbOfBeesBurstFly;

    [Header("Flying")]
    public float MaxRefreshRate;
    public float TimeBeforeDeath;
    public float speed;
    public Renderer FlyZone;

    [Header("Walking")]
    public float MaxRefreshRateWalk; 
    public float TimeBeforeDeathWalk;
    public float speedWalk;
  
	[Header("Auto Balance Flying and Walking")]
	public int MaxFlying; 
	public int MaxWakling;
	public int TimeTocheck;

    //PRIVATE 
    private GameObject[] intheScene;
    private GameObject[] Walls;
    private GameObject[] SpawnPoints;

    private void Start()
    {
        Walls = GameObject.FindGameObjectsWithTag("ZoneWalk");
        SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
		//ON HOLD FOR NOW
		//InvokeRepeating ("CheckBees()", 0f, TimeTocheck);
        Init();
    }

    void Update () {
		
		if(Input.GetKeyDown(KeyCode.I)) {
			Init();
		}

        if (Input.GetKeyDown(KeyCode.Y))
        {
            burst("Walking");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            burst("FlyingBlue");
        }

        if (Input.GetKeyDown (KeyCode.D)) {
			intheScene = GameObject.FindGameObjectsWithTag ("bee");

			foreach (var bees in intheScene) {

                if (bees.GetComponent<WalkingBees>() != null)  
                    bees.GetComponent<WalkingBees>().KillMeClimax();

                if (bees.GetComponent<FlyingBees>() != null)
                    bees.GetComponent<FlyingBees>().KillMeClimax();

                Destroy (bees);
			}
		}
	}

	public void CheckBees() {
		
		var allBees = GameObject.FindGameObjectsWithTag("bee"); 
		var countWalk = 0;
		var countFlyng = 0;

		foreach (var bee in allBees) {
			if (bee.GetComponent<WalkingBees> () != null)
				countWalk ++;
			if (bee.GetComponent<FlyingBees> () != null)
				countWalk ++;
		}
			
		if (countFlyng < MaxFlying)
			InternalInit (MaxWakling - countFlyng, 100);

	}

	public void Init()  {
		InternalInit (NbOfBees, FlyingPourcentage);
	}

	public void InternalInit(int Nb,int Pourc) {
		for (var i = 1; i <= Nb; i++) {
            var RandomRate = Random.Range(0, 100);
            var newRotation = Quaternion.Euler(-90f, 0f, 0f);
            var Spawn = SpawnPoints[Random.Range(0, SpawnPoints.Length)];

			if (RandomRate < Pourc)
            {
                GameObject NewBee = Instantiate(FlyingBees, Spawn.transform.position, newRotation, transform) as GameObject;
                NewBee.tag = "bee";
            } else
            {
                var selectedWall = Walls[Random.Range(0, Walls.Length)];
                GameObject NewBee = Instantiate(WalkingBees, Spawn.transform.position, newRotation, transform) as GameObject;
                print(selectedWall);
                NewBee.GetComponent<WalkingBees>().SelectedWall = selectedWall;
                NewBee.tag = "bee";
            }
		}
	}

    public void burst(string type)
    {

        var arraySelected = 0;
        var selectedObj = new Object();

        if (type == "Walking") {
            arraySelected = NbOfBeesBurstWalk;
            selectedObj = WalkingBees;
        }

		if (type == "FlyingBlue") {
            arraySelected = NbOfBeesBurstFly;
            selectedObj = FlyingBlue;
        }

        if (type == "Flying") {
            arraySelected = NbOfBeesBurstFly;
            selectedObj = WalkingBees;
        }

        for (var i = 1; i <= arraySelected; i++)
        {
            var newRotation = Quaternion.Euler(-90f, 0f, 0f);
            var Spawn = SpawnPoints[Random.Range(0, SpawnPoints.Length)];

            GameObject NewBee = Instantiate(selectedObj, Spawn.transform.position, newRotation, transform) as GameObject;

            if (type == "Walking")
            {
                var selectedWall = Walls[Random.Range(0, Walls.Length)];
                NewBee.GetComponentInChildren<WalkingBees>().SelectedWall = selectedWall;
            }

            NewBee.tag = "bee";
            
        }
    }

}
