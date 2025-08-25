using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;
using System.IO;
using Random = System.Random;

public class LogicManagerScript : MonoBehaviour
{
    public int playerScore;
    public int lifePoints;
    public Text scoreText;
    //public Text lifeText;
    public GameObject gameOverScreen;
    public GameObject hiScoresScreen;
    public GameObject scoreTextObject;
    //public GameObject lifeTextObject;
    public Boolean gravitateToX;
    public float gravitateToXDelayTime = 1.0f;
    private string filePath;

    private static string[] adjectives = {
    "Brave", "Swift", "Fierce", "Silent", "Lucky", "Mighty", "Shadow", "Thunder", "Wicked", "Clever",
    "Fearless", "Vengeful", "Stormy", "Blazing", "Phantom", "Ghostly", "Daring", "Savage", "Arcane", "Legendary",
    "Cunning", "Noble", "Iron", "Glorious", "Epic", "Lunar", "Solar", "Infernal", "Doomed", "Radiant"
};

    private static string[] firstNames = {
    "Arin", "Leo", "Nova", "Dante", "Rex", "Kai", "Orion", "Zane", "Luna", "Sage",
    "Ezra", "Xander", "Juno", "Axel", "Mira", "Kane", "Riven", "Selene", "Talon", "Vera",
    "Cyrus", "Ember", "Athena", "Draven", "Lyra", "Gideon", "Seraph", "Zyra", "Blaise", "Nyx",
    "Finn", "Aurora", "Skye", "Kira", "Jax", "Rowan", "Milo", "Phoenix", "Cleo", "Ash",
    "Soren", "Lyric", "Faye", "Quinn", "Atlas", "Leif", "Sable", "Rune", "Rhea", "Ryder",
    "Cassian", "Zephyr", "Vale", "Indigo", "Storm", "Vesper", "Nyssa", "Echo", "Aspen", "River"
};

    private static string[] lastNames = {
    "Storm", "Blackwood", "Dragonsbane", "Hawke", "Nightshade", "Frost", "Wolfe", "Shadowfang", "Raven", "Ironheart",
    "Duskbane", "Bloodfang", "Moonrider", "Darkmoor", "Firebrand", "Silverclaw", "Grimshaw", "Thunderstrike", "Emberfall", "Voidwalker",
    "Deathwhisper", "Soulreaper", "Stormrider", "Ruinblade", "Ironfang", "Windchaser", "Frostborn", "Netherbane", "Starborn", "Shadowveil",
    "Skylark", "Nightstalker", "Ashthorne", "Winterbane", "Drake", "Hollows", "Gravekeeper", "Riftwalker", "Bloodstone", "Falconcrest",
    "Stormbreaker", "Ironclad", "Viperfang", "Shadewalker", "Evermore", "Darkspire", "Runeblade", "Mournwind", "Hellfire", "Flameborne",
    "Thunderclap", "Ghostmane", "Wildwood", "Blackstone", "Wolfhunter", "Bloodhawk", "Mistwalker", "Blackspire", "Shadowrend", "Frostwolf"
};


    private void Start()
    {
        AdManager.Instance.LoadInterstitialAd();
        //DifficultySettings difficultySettings = DifficultySettings.Instance;  
        gameOverScreen.SetActive(false);
        hiScoresScreen.SetActive(false);
        scoreTextObject.SetActive(true);
        //lifeTextObject.SetActive(true);
        //TODO

        filePath = Path.Combine(Application.persistentDataPath, "highscore.json");
        Debug.Log("High Score File Path: " + filePath);
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
        //lifeText.text = lifePoints.ToString();
        switch (lifePoints)
        {
            case 2:
                GameObject heart3 = GameObject.Find("heartUI_3");
                if (heart3 != null)
                {
                    Destroy(heart3);
                }
                break;
            case 1:
                GameObject heart2 = GameObject.Find("heartUI_2");
                if (heart2 != null)
                {
                    Destroy(heart2);
                }
                break;
            case 0:
                GameObject heart1 = GameObject.Find("heartUI_1");
                if (heart1 != null)
                {
                    Destroy(heart1);
                }
                break;
            default:
                Console.WriteLine("Invalid life points value.");
                break;
        }
    }

    public void restartGame()
    {
        AdManager.Instance.ShowInterstitialAd();
        Debug.Log("restartGame() function triggerd");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameOver()
    {
        SaveHighScore(GenerateRandomName(), playerScore);
        DifficultySettings.Instance.resetDifficultyToDefault();
        gameOverScreen.SetActive(true);
        hiScoresScreen.SetActive(true);
        scoreTextObject.SetActive(false);
        //lifeTextObject.SetActive(false);
    }
    public void applicationQuit()
    {
        Application.Quit();
    }

    private static Random random = new Random();

    public static string GenerateRandomName()
    {
        string adjective = adjectives[random.Next(adjectives.Length)];
        string firstName = firstNames[random.Next(firstNames.Length)];
        string lastName = lastNames[random.Next(lastNames.Length)];
        int number = random.Next(10, 99);

        return $"{adjective}{firstName} {lastName}{number}";
    }

    [System.Serializable]
    public class HighScoreData
    {
        public string playerName;
        public int highScore;
        public string timestamp;
        public bool newScore;
    }

    [System.Serializable]
    public class HighScoreList
    {
        public List<HighScoreData> scores = new List<HighScoreData>();
    }

    public void SaveHighScore(string name, int score)
    {
        HighScoreList highScoreList = LoadHighScores();

        foreach (var scoreInList in highScoreList.scores)
        {
            scoreInList.newScore = false;
        }

        HighScoreData newScore = new HighScoreData
        {
            playerName = name,
            highScore = score,
            timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            newScore = true
        };

        highScoreList.scores.Add(newScore);

        highScoreList.scores.Sort((a, b) => b.highScore.CompareTo(a.highScore));

        if (highScoreList.scores.Count > 10)
        {
            highScoreList.scores.RemoveAt(10);
        }

        string json = JsonUtility.ToJson(highScoreList, true);

        File.WriteAllText(filePath, json);

        Debug.Log("Updated High Scores:\n" + json);

        Text scoreText = hiScoresScreen.GetComponent<Text>();

        scoreText.text = FormatHighScores(highScoreList);
    }

    public HighScoreList LoadHighScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<HighScoreList>(json);

        }
        return new HighScoreList();
    }
    public string FormatHighScores(HighScoreList highScoreList)
    {

        string formattedScores = "";

        foreach (var score in highScoreList.scores)
        {
            string scoreText = score.newScore
    ? $"<b>{score.highScore}</b>"
    : $"{score.highScore}";

            formattedScores += $"{scoreText} - {score.playerName} - {score.timestamp}\n";
        }
        return formattedScores;
    }

}
