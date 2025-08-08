using UnityEngine;

public class SinMover : MonoBehaviour
{
    public float amplitude = 1f;       // How high it moves
    public float frequency = 1f;       // How fast it moves

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate new Y position using sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the new position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}