using UnityEngine;

public class SlideInAnimation : MonoBehaviour
{
    public Vector3 direction = Vector3.left; // Default: slide in from left
    public float distance = 300f;            // How far away it starts
    public float speed = 5f;                 // How fast it moves

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private bool isSliding = true;

    void Start()
    {
        // Save the final position
        targetPosition = transform.localPosition;

        // Set the starting position away from target
        startPosition = targetPosition + direction.normalized * distance;
        transform.localPosition = startPosition;
    }

    void Update()
    {
        if (isSliding)
        {
            // Move towards target
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);

            // Stop when reached
            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.01f)
            {
                transform.localPosition = targetPosition;
                isSliding = false;
            }
        }
    }
}
