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

    public int CurrentScoreThreshold { get => currentScoreThreshold; set => currentScoreThreshold = value; }
    public float ScoreDifficultyMultiplier { get => scoreDifficultyMultiplier; set => scoreDifficultyMultiplier = value; }
}
