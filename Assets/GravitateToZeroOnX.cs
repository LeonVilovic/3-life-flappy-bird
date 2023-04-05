using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitateToZeroOnX : MonoBehaviour
{
    public float positionSpeed;
    public float rotationSpeed;
    public Rigidbody2D myRigidbody;

    public LogicManagerScript logicManagerScript;
    public float reductionRate = 0.1f;


    private void Start()
    {
        logicManagerScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManagerScript>();

    
    }

    private void FixedUpdate()
    {
        if (logicManagerScript.gravitateToX) {
            // Position
            if (Mathf.Abs(transform.position.x) > 0.001f)
            {
                Vector2 targetPosition = new Vector2(0f, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, positionSpeed * Time.fixedDeltaTime);
            }

            // Rotation
            //if (Quaternion.Angle(transform.rotation, Quaternion.identity) > 0.001f)
            //{
            //    float targetAngle = 0f;
            //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, targetAngle), rotationSpeed * Time.fixedDeltaTime);
            //}


            float currentXVelocity = myRigidbody.velocity.x;
            float targetXVelocity = 0f;
            float reducedXVelocity = Mathf.Lerp(currentXVelocity, targetXVelocity, reductionRate);

            myRigidbody.velocity = new Vector2(reducedXVelocity, myRigidbody.velocity.y);


        }

    }
}
