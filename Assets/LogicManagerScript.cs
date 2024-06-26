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
    



    private void Start()
    {
        //DifficultySettings difficultySettings = DifficultySettings.Instance;  
        gameOverScreen.SetActive(false);
        //TODO

    }


    [ContextMenu("Increase Score")]
    public void addScore()
    {
        playerScore = playerScore + (int)(100 * DifficultySettings.Instance.ScoreDifficultyMultiplier);
        scoreText.text = playerScore.ToString();
        if (playerScore > DifficultySettings.Instance.CurrentScoreThreshold) { DifficultySettings.Instance.increaseDifficulty(); }
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



}
