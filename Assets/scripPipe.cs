using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scripPipe : MonoBehaviour
{
    public float moveSpeed = 4;
    public float deadZone = -45;

    void Start()
    {
        
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
