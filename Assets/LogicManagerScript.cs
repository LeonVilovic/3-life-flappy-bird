using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class LogicManagerScript : MonoBehaviour
{
    public int playerScore;
    public int lifePoints;
    public Text scoreText;
    public Text lifeText;
    public GameObject gameOverScreen;
    public Boolean gravitateToX;
    public float gravitateToXDelayTime = 1.0f;
    public PipeSpawnerScrip PipeSpawner;
    private DifficultySettings difficultySettings;
    
    

    public void manageDifficulty()
    {

        increasePipeSpawnSpeed();
    }


    private void Start()
    {
        //DifficultySettings difficultySettings = DifficultySettings.Instance;  
        gameOverScreen.SetActive(false);
        //TODO
        PipeSpawner = GameObject.FindGameObjectWithTag("PipeSpawner").GetComponent<PipeSpawnerScrip>();
        Debug.Log("PipeSpawner reference: " + PipeSpawner);
    }


    [ContextMenu("Increase Score")]
    public void addScore()
    {
        playerScore = playerScore + (int)(100 * DifficultySettings.Instance.ScoreDifficultyMultiplier);
        scoreText.text = playerScore.ToString();
        if (playerScore > DifficultySettings.Instance.CurrentScoreThreshold) { manageDifficulty(); }
    }

    public void reduceLifePoints()
    {
        lifePoints = lifePoints - 1;
        lifeText.text = lifePoints.ToString();
    }

    public void restartGame()
    {
        Debug.Log("restartGame() function triggerd");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }
    public void applicationQuit()
    {
        Application.Quit();
    }

    public void increasePipeSpawnSpeed()
    {
        //PipeSpawner.spawnRate = PipeSpawner.spawnRate - 0.1f;
        if (PipeSpawner.spawnRate > 1.5) { PipeSpawner.spawnRate = PipeSpawner.spawnRate * 0.965f; }

        Debug.Log("PipeSpawner.SpawnRate updated: " + PipeSpawner.spawnRate);
    }

}
