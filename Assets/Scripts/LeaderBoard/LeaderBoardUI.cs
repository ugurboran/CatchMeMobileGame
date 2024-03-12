using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private LeaderboardsSample leaderboard;

    [SerializeField] private GameObject leaderboardRow;
    [SerializeField] private GameObject leaderboardTable;
    [SerializeField] private GameObject playerObjectRow;
    [SerializeField] private GameObject playerTable;
    [SerializeField] private Sprite GoldSprite;
    [SerializeField] private Sprite SilverSprite;
    [SerializeField] private Sprite BronzeSprite;

    // private async void Start()
    // {
    //     int scoreValue = 0;
    //     int playerRankValue = 0;
    //     //yield return new WaitUntil (() => AuthenticationService.Instance.IsAuthorized);
    //     Debug.Log("Geçti " + AuthenticationService.Instance.IsAuthorized);
    //     leaderboard.AddScore();
    //     //LeaderboardScoresPage scores = await leaderboard.GetScores();
    //     LeaderboardScoresPage scores = await leaderboard.GetPaginatedScores();
    //     LeaderboardEntry playerEntry = await leaderboard.GetPlayerScore();

    //     GameObject playerRow = Instantiate(playerObjectRow, playerTable.transform);

    //     TMP_Text playertextRank = playerRow.transform.GetChild(0).GetComponent<TMP_Text>();
    //     TMP_Text playertextName = playerRow.transform.GetChild(3).GetComponent<TMP_Text>();
    //     TMP_Text playertextScore = playerRow.transform.GetChild(4).GetComponent<TMP_Text>();

    //     playerRankValue = playerEntry.Rank;
    //     playerRankValue = playerRankValue + 1;
    //     playertextRank.text = playerRankValue.ToString();
    //     playertextName.text = playerEntry.PlayerName;

    //     int roundedPlayerScore = Mathf.RoundToInt((float)playerEntry.Score); // Küsuratı kaldırma işlemi
    //     playertextScore.text = roundedPlayerScore.ToString();




    //     foreach (var score in scores.Results)
    //     {
    //         GameObject row = Instantiate(leaderboardRow, leaderboardTable.transform);

    //         TMP_Text textRank = row.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
    //         TMP_Text textName = row.transform.GetChild(3).GetComponent<TMP_Text>();
    //         TMP_Text textScore = row.transform.GetChild(4).GetComponent<TMP_Text>();

    //         if(score.Rank == 0){
    //             Image GoldImage = row.transform.GetChild(0).GetComponent<Image>();
    //             GoldImage.sprite = GoldSprite;
    //         }
    //         else if(score.Rank == 1){
    //             Image SilverImage = row.transform.GetChild(0).GetComponent<Image>();
    //             SilverImage.sprite = SilverSprite;
    //         }
    //         else if(score.Rank == 2){
    //             Image BronzeImage = row.transform.GetChild(0).GetComponent<Image>();
    //             BronzeImage.sprite = BronzeSprite;
    //         }
    //         else{
    //             row.transform.GetChild(0).GetComponent<Image>().enabled = false;
    //         }

    //         scoreValue = score.Rank;
    //         scoreValue = scoreValue + 1;
    //         textRank.text = scoreValue.ToString();
    //         //textRank.text = score.Rank.ToString();
    // 		textName.text = score.PlayerName;

    // 		//textScore.text = score.Score.ToString();
    //         // Virgülden sonrasını kaldırarak skoru formatla
    //         int roundedScore = Mathf.RoundToInt((float)score.Score); // Küsuratı kaldırma işlemi
    //         textScore.text = roundedScore.ToString();

    //     }
    // }

    private async void OnEnable()
    {
        int scoreValue = 0;
        int playerRankValue = 0;
        //yield return new WaitUntil (() => AuthenticationService.Instance.IsAuthorized);
        Debug.Log("Geçti " + AuthenticationService.Instance.IsAuthorized);
        leaderboard.AddScore();
        //LeaderboardScoresPage scores = await leaderboard.GetScores();
        LeaderboardScoresPage scores = await leaderboard.GetPaginatedScores();
        LeaderboardEntry playerEntry = await leaderboard.GetPlayerScore();

        GameObject playerRow = Instantiate(playerObjectRow, playerTable.transform);

        TMP_Text playertextRank = playerRow.transform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text playertextName = playerRow.transform.GetChild(3).GetComponent<TMP_Text>();
        TMP_Text playertextScore = playerRow.transform.GetChild(4).GetComponent<TMP_Text>();

        playerRankValue = playerEntry.Rank;
        playerRankValue = playerRankValue + 1;
        playertextRank.text = playerRankValue.ToString();
        playertextName.text = playerEntry.PlayerName;

        int roundedPlayerScore = Mathf.RoundToInt((float)playerEntry.Score); // Küsuratı kaldırma işlemi
        playertextScore.text = roundedPlayerScore.ToString();




        foreach (var score in scores.Results)
        {
            GameObject row = Instantiate(leaderboardRow, leaderboardTable.transform);

            TMP_Text textRank = row.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
            TMP_Text textName = row.transform.GetChild(3).GetComponent<TMP_Text>();
            TMP_Text textScore = row.transform.GetChild(4).GetComponent<TMP_Text>();

            if (score.Rank == 0)
            {
                Image GoldImage = row.transform.GetChild(0).GetComponent<Image>();
                GoldImage.sprite = GoldSprite;
            }
            else if (score.Rank == 1)
            {
                Image SilverImage = row.transform.GetChild(0).GetComponent<Image>();
                SilverImage.sprite = SilverSprite;
            }
            else if (score.Rank == 2)
            {
                Image BronzeImage = row.transform.GetChild(0).GetComponent<Image>();
                BronzeImage.sprite = BronzeSprite;
            }
            else
            {
                row.transform.GetChild(0).GetComponent<Image>().enabled = false;
            }

            scoreValue = score.Rank;
            scoreValue = scoreValue + 1;
            textRank.text = scoreValue.ToString();
            //textRank.text = score.Rank.ToString();
            textName.text = score.PlayerName;

            //textScore.text = score.Score.ToString();
            // Virgülden sonrasını kaldırarak skoru formatla
            int roundedScore = Mathf.RoundToInt((float)score.Score); // Küsuratı kaldırma işlemi
            textScore.text = roundedScore.ToString();
        }
    }
}
