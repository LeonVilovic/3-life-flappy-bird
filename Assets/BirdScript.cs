using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrenght;
    public  LogicManagerScript logicManagerScript;
    bool birdIsAlive = true;
    public float CollisionForceMultiplier;
    public float CollisionForceXAxisFactor;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        logicManagerScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive)
        {
            myRigidbody.velocity = myRigidbody.velocity*Vector2.right + Vector2.up * flapStrenght;
            //   myRigidbody.AddForce(Vector2.up * flapStrenght, ForceMode2D.Impulse);
             

        }
        if (myRigidbody.velocity.y > -8)
        {
            animator.Play("Flying");
        }
        else
        {
            animator.Play("FallingEdit");
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
         Debug.Log($"triggered OnCollisionEnter2D for bird");

        logicManagerScript.reduceLifePoints();

        logicManagerScript.gravitateToX = false;
        CancelInvoke("setGravitateToXTrue");
        Invoke("setGravitateToXTrue", logicManagerScript.gravitateToXDelayTime);

        if (logicManagerScript.lifePoints == 0)
        {
            birdIsAlive = false;
            logicManagerScript.gameOver();
            myRigidbody.gravityScale = 6;
            Destroy(GameObject.FindGameObjectWithTag("bottomContainer"));
        }

        Vector2 direction = (transform.position - collision.transform.position).normalized;

        // Apply a force in the opposite direction of the collision

        //   myRigidbody.AddForce(direction * CollisionForceMultiplier, ForceMode2D.Impulse);

        Vector2 XAxisScalingVector = new Vector2(CollisionForceXAxisFactor, 1f);
        myRigidbody.velocity = XAxisScalingVector * direction * CollisionForceMultiplier;
    }
    public void setGravitateToXTrue()
    {
        logicManagerScript.gravitateToX=true;
    }

}
