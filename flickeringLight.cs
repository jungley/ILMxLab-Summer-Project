using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script to make chandelier light flicker
public class flickeringLight : MonoBehaviour
{
    public Light lt;
    public float speed = 2.5f;
    public float minIntensity = 0.25f;
    public float maxIntensity = 0.5f;

    float random;

    void Start()
    {
        lt = GetComponent<Light>();
        random = Random.Range(0.0f, 65535.0f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time*speed);
        lt.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
  
    }
}
