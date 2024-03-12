using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Authentication;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI highestScoreText;
    //public LeaderboardsSample leaderboardsSample;
    //public TextMeshProUGUI scoresText;

    void Start()
    {
        float score = ScoreManager.instance.GetCurrentScore();
        //float highestScore = ScoreManager.instance.GetHighestScore();
        //float highestScore = PlayerPrefs.GetFloat("HighestScore", 0);
        int roundedScore = Mathf.RoundToInt((float)score);
        scoreText.text = "" + roundedScore.ToString();
        //highestScoreText.text = "Highest Score: " + highestScore.ToString();


        //await leaderboardsSample.AddScore();
        //await leaderboardsSample.GetScores();
        // StartCoroutine(SetScore());
    }

    // public IEnumerator SetScore()
    // {
    //     yield return new WaitUntil(() => AuthenticationService.Instance.IsAuthorized == true);
    //     leaderboardsSample.AddScore();
    //     //leaderboardsSample.GetPlayerScore();
    //     leaderboardsSample.GetScores();
    //     //leaderboardsSample.GetPaginatedScores();
    //     //scoresText = leaderboardsSample.GetScores();
    // }
}

