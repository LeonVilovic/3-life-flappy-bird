using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTransformScript : MonoBehaviour
{
    public float moveSpeed = 4;
    public float deadZone = -45;

    void Start()
    {
        Debug.Log($"PipeTransformScript prams: moveSpeed - " + moveSpeed + " dead zone - ");
    }
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            //Debug.Log($"Pipe destroyed");
            Destroy(gameObject);
        }
    }
}
