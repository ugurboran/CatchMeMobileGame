using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using TMPro;

public class LeaderboardsSample : MonoBehaviour
{
    // Create a leaderboard with this ID in the Unity Dashboard
    const string LeaderboardId = "my-first-leaderboard";
    //public TextMeshProUGUI scoresText;

    string VersionId { get; set; }
    int Offset { get; set; }
    int Limit { get; set; }
    int RangeLimit { get; set; }
    List<string> FriendIds { get; set; }

    public async void AddScore()
    {
        float score = ScoreManager.instance.GetCurrentScore();
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    // public async void GetScores()
    // {
    //     var scoresResponse =
    //         await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
    //     Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    //     scoresText.text = JsonConvert.SerializeObject(scoresResponse);
    // }

    public async Task<LeaderboardScoresPage> GetScores()
	{
		LeaderboardScoresPage scoresResponse =
			await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
		Debug.Log(JsonConvert.SerializeObject(scoresResponse));
		
		return scoresResponse;
	}

    public async Task<LeaderboardScoresPage> GetPaginatedScores()
    {
        Offset = 0;
        Limit = 20;
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions{Offset = Offset, Limit = Limit});
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

        return scoresResponse;
    }

    // public async void GetPlayerScore()
    // {
    //     var scoreResponse = 
    //         await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
    //     Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        
    // }

    public async Task<LeaderboardEntry> GetPlayerScore()
	{
		LeaderboardEntry scoreResponse =
			await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
		Debug.Log(JsonConvert.SerializeObject(scoreResponse));

        return scoreResponse;
	
		//return (int)scoreResponse.Score;
	}

    public async void GetVersionScores()
    {
        var versionScoresResponse =
            await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId);
    Debug.Log(JsonConvert.SerializeObject(versionScoresResponse));
    }
}
