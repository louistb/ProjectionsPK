using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisePiston : MonoBehaviour
{
    Vector3 startPos;

    private float amplitude = 0.3f;
    private float period;
    private bool NoiseMove;

    protected void Start()
    {
        startPos = transform.position;
        period = Random.Range(1f,3f);
    }

    public void Update()
    {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
        transform.position = startPos + Vector3.forward * distance;
    }


}