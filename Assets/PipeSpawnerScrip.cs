using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScrip : MonoBehaviour
{
    public GameObject gameObjectForSpawn;

    public float spawnRate;

    private float timer = 0;

    private float spawnOffset = 10;

    public float SpawnRate { get; set; }
    public float SpawnOffset { get => spawnOffset; set => spawnOffset = value; }
    public float Timer { get => timer; set => timer = value; }


    // Start is called before the first frame update
    void Start()
    {
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

         Instantiate(gameObjectForSpawn, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}
