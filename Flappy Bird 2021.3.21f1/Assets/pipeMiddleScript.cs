using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeMiddleScript : MonoBehaviour
{
    public LogicManagerScript LogicManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        LogicManagerScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LogicManagerScript.addScore();
    }
}
