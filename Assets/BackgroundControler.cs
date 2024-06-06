using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControler : MonoBehaviour
{

    private float startPosition, length;
    public float paralaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.x - paralaxEffect * Time.deltaTime;

        transform.position = new Vector2 (startPosition + distance, transform.position.y);

        if (distance < startPosition - length)
        {
            transform.position = new Vector2(startPosition, transform.position.y);
            Debug.Log("Infinite scroll condition triggered. distance: " + distance + " startPosition: " + startPosition + " length: " + length);
        }


    }
}
