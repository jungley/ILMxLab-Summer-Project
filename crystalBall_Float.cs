using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalBall_Float : MonoBehaviour {

    public float delta = 0.02f;  // Amount to move left and right from the start point
    public float speed = 2f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 v = startPos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
