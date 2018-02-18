using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisePiston : MonoBehaviour
{
    Vector3 startPos;

    private float amplitude = 0.06f;
    private float period;
    private bool NoiseMove;
	private Vector3 NoiseVector;

    protected void Start()
    {
        period = Random.Range(1f,2f);
    }

    public void Update()
    {
		NoiseUpdate();
		transform.position = NoiseVector;
    }

	public void NoiseUpdate() {
		float theta = Time.timeSinceLevelLoad / period;
		float distance = amplitude * Mathf.Sin(theta);
		NoiseVector  = transform.position + Vector3.forward * distance;
	}


}