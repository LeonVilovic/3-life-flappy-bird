using UnityEngine;


public class SinMover : MonoBehaviour
{
    public enum Axis
    {
        X,
        Y
    }

    public float amplitude = 1f;
    public float frequency = 1f;
    public Axis selectedAxis = Axis.Y;

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        switch (selectedAxis)
        {
            case Axis.X:
                float newX = startPosition.x + Mathf.Sin(Time.time * frequency) * amplitude;
                transform.position = new Vector3(newX, startPosition.y, startPosition.z);
                break;

            case Axis.Y:
                float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
                transform.position = new Vector3(startPosition.x, newY, startPosition.z);
                break;
            default:
                Debug.Log("Unknown axis");
                break;
        }

    }
}