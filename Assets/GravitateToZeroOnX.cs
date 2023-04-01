using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitateToZeroOnX : MonoBehaviour
{
    public float positionSpeed;
    public float rotationSpeed;

    private void FixedUpdate()
    {
        // Position
        if (Mathf.Abs(transform.position.x) > 0.01f)
        {
            Vector2 targetPosition = new Vector2(0f, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, positionSpeed * Time.fixedDeltaTime);
        }

        // Rotation
        if (Quaternion.Angle(transform.rotation, Quaternion.identity) > 0.01f)
        {
            float targetAngle = 0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, targetAngle), rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
