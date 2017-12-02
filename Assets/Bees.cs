using UnityEngine;
using System.Collections;

public class Bees : MonoBehaviour {

	public float _MaxRefreshRate,_TimeBeforeDeath,_speed;
	public float _RangeDirectionY,_RangeDirectionX,_RangeDirectionZ;
	public BeesController Controller;

	public float LifeTimer,t,UpdateTime;
	public Vector3 CurrentPos, DestinationPos,AtractPos;
	public bool InAttraction;

	void Awake() {
		Controller = GameObject.Find ("Bees_System").GetComponent<BeesController>();

		_MaxRefreshRate = Controller.MaxRefreshRate;
		_TimeBeforeDeath = Controller.TimeBeforeDeath - Controller.TimeBeforeDeath / Random.Range(0,Controller.TimeBeforeDeath/3);
		_speed = Controller.speed;
		_RangeDirectionX = Controller.RangeDirectionX;
		_RangeDirectionY = Controller.RangeDirectionY;
		_RangeDirectionZ = Controller.RangeDirectionZ;
	}

	void Start () 
	{
		UpdateTime = NewRandUpdate();
		DirectionUpdate();
	}
		

	void OnCollisionEnter(Collision collision)
	{
		print (collision);
		DirectionUpdate ();
	}

	private void DirectionUpdate() {
		UpdateTime = NewRandUpdate();
		t = 0;	
		if (InAttraction = true) {
			DestinationPos.x = AtractPos.x + Random.Range (-_RangeDirectionX / 10, _RangeDirectionX / 10);
			DestinationPos.y = AtractPos.y + Random.Range (-_RangeDirectionY / 10, _RangeDirectionY / 10);
			DestinationPos.z = AtractPos.z + Random.Range (0,-_RangeDirectionZ / 10);
		} else {
			DestinationPos.x = transform.position.x + Random.Range (-_RangeDirectionX, _RangeDirectionX);
			DestinationPos.y = transform.position.y + Random.Range (-_RangeDirectionY, _RangeDirectionY);
			DestinationPos.z = transform.position.z + Random.Range (0,-_RangeDirectionZ);
		}
	}

	private void FixedUpdate () 
	{
		//GLOBAL 
		t += Time.deltaTime;

		if (t >= UpdateTime) {
			DirectionUpdate();
		}

		//LERP
		CurrentPos = transform.position;

		transform.position = Vector3.Slerp(CurrentPos,DestinationPos, t * _speed);

		//KILLING WHEN TIME IS OVER
		LifeTimer += Time.deltaTime;

		if (LifeTimer >= _TimeBeforeDeath) {
			Destroy (gameObject);
		}
	}

	public float NewRandUpdate() {
		return Random.Range(0, _MaxRefreshRate);
	}
		
}