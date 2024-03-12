using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private float score = 0f;
    private float highestScore = 0f;
    // Anahtarlar için string değişkenler tanımlıyoruz
    private const string ScoreKey = "CurrentScore";
    private const string HighestScoreKey = "HighestScore";
    private float scorePerMillisecond = 0.1f;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //scoreText = GetComponent<TextMeshProUGUI>();
        if (SceneManager.GetActiveScene().name == "GameScene" || SceneManager.GetActiveScene().name == "Tutorial"){
            scoreText.text = "Score: 0"; // Set the initial score text
        }
        
        // Oyun başladığında PlayerPrefs'ten en yüksek skoru alıyoruz
        highestScore = PlayerPrefs.GetFloat(HighestScoreKey, 0);
    }

    private void Update()
    {
        float deltaScore = scorePerMillisecond * Time.deltaTime * 1000f; // Convert deltaTime to milliseconds
        score += deltaScore;
        int roundedScore = Mathf.RoundToInt((float)score);
        
        // Update the score text
        if (SceneManager.GetActiveScene().name == "GameScene" || SceneManager.GetActiveScene().name == "Tutorial"){

            scoreText.text = "Score: " + roundedScore.ToString();
        }
        

        if (score > highestScore)
        {
            UpdateScore();
        }
    }

    public void UpdateScore(){
        highestScore = score;
        PlayerPrefs.SetFloat(HighestScoreKey, highestScore);
        PlayerPrefs.Save();
    }

    public float GetCurrentScore()
    {
        return score;
    }

    public float GetHighestScore()
    {
        return highestScore;
    }
}


