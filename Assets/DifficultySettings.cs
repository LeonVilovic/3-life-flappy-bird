using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySettings
{

    private static DifficultySettings instance = null;
    private DifficultySettings() { }
    public static DifficultySettings Instance
    {
        get
        {
            if (instance == null)
                instance = new DifficultySettings();
            return instance;
        }
    }

    private float scoreDifficultyMultiplier = 1;
    private int currentScoreThreshold = 400;
    private int[] scoreThresholdArr = new int[] { 900, 1500, 2200, 3000 };
    private int scoreThresholdIndex = 0;
    private PipeSpawnerScrip PipeSpawner = null;

    public int CurrentScoreThreshold { get => currentScoreThreshold; set => currentScoreThreshold = value; }
    public float ScoreDifficultyMultiplier { get => scoreDifficultyMultiplier; set => scoreDifficultyMultiplier = value; }

    public void increasePipeSpawnSpeed()
    {
        if (PipeSpawner == null)
            PipeSpawner = GameObject.FindGameObjectWithTag("PipeSpawner").GetComponent<PipeSpawnerScrip>();
        //Debug.Log("PipeSpawner reference: " + PipeSpawner);

        //PipeSpawner.spawnRate = PipeSpawner.spawnRate - 0.1f;
        if (PipeSpawner.spawnRate > 1.5) { PipeSpawner.spawnRate = PipeSpawner.spawnRate * 0.965f; }

        Debug.Log("PipeSpawner.SpawnRate updated: " + PipeSpawner.spawnRate);
    }

    public void increaseDifficulty()
    {
        scoreDifficultyMultiplier += 0.1f;
        if (scoreThresholdIndex != scoreThresholdArr.Length - 1)
        {
            currentScoreThreshold = scoreThresholdArr[scoreThresholdIndex];
            scoreThresholdIndex++;
        }
        else
        {
            currentScoreThreshold += scoreThresholdArr[scoreThresholdIndex];
        }
        increasePipeSpawnSpeed();
        Debug.Log("increaseDifficulty() currentScoreThreshold:" + currentScoreThreshold.ToString());
    }
}
