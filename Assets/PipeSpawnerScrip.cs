using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PipeSpawnerScrip : MonoBehaviour
{
    public GameObject gameObjectForSpawn;
    public LogicManagerScript logicManagerScript;

    public float spawnRate;

    private float timer = 0;

    private float spawnOffset = 10;

    public float SpawnRate { get; set; }
    public float SpawnOffset { get => spawnOffset; set => spawnOffset = value; }
    public float Timer { get => timer; set => timer = value; }


    // Start is called before the first frame update
    void Start()
    {
        logicManagerScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManagerScript>();
        spawnPipe();

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            timer = 0;
            spawnPipe();
        }


    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - SpawnOffset;
        float highestPoint = transform.position.y + SpawnOffset;


        Transform child1 = gameObjectForSpawn.transform.GetChild(0);
        Transform child2 = gameObjectForSpawn.transform.GetChild(1);
        Transform child3 = gameObjectForSpawn.transform.GetChild(2);

        UnityEngine.Debug.Log("DifficultySettings.Instance.PipeSpawnerPipeOffset: " + DifficultySettings.Instance.PipeSpawnerPipeOffset);

        child1.localPosition += new Vector3(0, -DifficultySettings.Instance.PipeSpawnerPipeOffset, 0);
        child2.localPosition += new Vector3(0, DifficultySettings.Instance.PipeSpawnerPipeOffset, 0);

        UnityEngine.Debug.Log("child1.localPosition " + child1.localPosition);
        UnityEngine.Debug.Log("child2.localPosition " + child2.localPosition);

        Instantiate(gameObjectForSpawn, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

        //if you dont return gameObjectForSpawn to its initial state the prefab gets alterd in next execution
        child1.localPosition += new Vector3(0, DifficultySettings.Instance.PipeSpawnerPipeOffset, 0);
        child2.localPosition += new Vector3(0, -DifficultySettings.Instance.PipeSpawnerPipeOffset, 0);
 


    }
}
