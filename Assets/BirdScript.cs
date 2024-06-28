using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrenght;
    public LogicManagerScript logicManagerScript;
    public Blinking blinking; //connect this in inspector
    bool birdIsAlive = true;
    bool birdIsInvulnerablee = false;
    float birdIsInvulnerableeTimeLeft = 0f;
    public float InvulnerabilityTime = 2;
    public float CollisionForceMultiplier;
    public float CollisionForceXAxisFactor;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        logicManagerScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManagerScript>();
        blinking.isBlinking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive)
        {
            myRigidbody.velocity = myRigidbody.velocity * Vector2.right + Vector2.up * flapStrenght;
            //   myRigidbody.AddForce(Vector2.up * flapStrenght, ForceMode2D.Impulse);

        }
        if (birdIsInvulnerablee)
        {
            birdIsInvulnerableeTimeLeft -= Time.deltaTime;
            if (birdIsInvulnerableeTimeLeft < 0) { 
            birdIsInvulnerablee = false;
            blinking.isBlinking = false;
            }
        }


        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (myRigidbody.velocity.y > -8)
        {

            if (stateInfo.IsName("Base Layer.Falling") && birdIsAlive)
            {
                animator.Play("FallingReverse", 0, stateInfo.normalizedTime % 1);
                //Debug.Log(stateInfo.normalizedTime.ToString() + " FallingReverse");
            }
            else if (stateInfo.IsName("Base Layer.FallingReverse") && stateInfo.normalizedTime <= 1 && birdIsAlive)
            {
                animator.Play("FallingReverse");
                //Debug.Log(stateInfo.normalizedTime.ToString() + " FallingReverse");
            }

            else if (birdIsAlive) animator.Play("Flying");
            //Debug.Log(stateInfo.normalizedTime.ToString()+ " Flying");

        }
        else
        {
            if (stateInfo.IsName("Base Layer.FallingReverse") && stateInfo.normalizedTime <= 1)
            {
                if (birdIsAlive) { animator.Play("Falling", 0, stateInfo.normalizedTime % 1); }
                else { animator.Play("Dead", 0, stateInfo.normalizedTime % 1); }
                // Debug.Log(stateInfo.normalizedTime.ToString() + " Falling");
            }

            if (birdIsAlive) { animator.Play("Falling"); }
            else { animator.Play("Dead"); }
            //Debug.Log(stateInfo.normalizedTime.ToString() + " Falling");
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"triggered OnCollisionEnter2D for bird");


        if (!birdIsInvulnerablee)
        {
        logicManagerScript.reduceLifePoints();
            birdIsInvulnerablee = true;
            birdIsInvulnerableeTimeLeft = InvulnerabilityTime;
            blinking.isBlinking = true;
        }



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
        logicManagerScript.gravitateToX = true;
    }

}
