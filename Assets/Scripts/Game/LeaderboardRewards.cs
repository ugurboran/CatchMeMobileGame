using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Game.Managers;
using Save_System.Cloud;
using Save_System.JSON;
using Save_System.Model;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using UnityEngine;
using UnityEngine.UI;
using Save_System;
using Save_System.JSON.Model;


namespace Game
{
    /// <summary>
    /// Bu class ile oyuna giriş yapıldığında leaderboard ödüllerini kontrol ediyoruz.
    /// </summary>
    public class LeaderboardRewards : MonoBehaviour
    {
        private readonly IDataService dataService = new JsonDataService();

        [SerializeField] private CloudSave cloudSave;

        [SerializeField] private GameObject leaderboardRewardPanel;
        [SerializeField] private TMP_Text textMessage;
        [SerializeField] private Button buttonClaim;

        private const string ModuleName = "CatchMeCloudCode";

        private async void Start()
        {
			await WaitForLogin();
			Debug.Log("try öncesi");
			Debug.Log("" + AuthenticationService.Instance.PlayerId);
            try
            {
                string response = await CloudCodeService.Instance.CallModuleEndpointAsync<string>(
                    ModuleName,
                    "GetLatestLeaderboardVersion",
                    new Dictionary<string, object> { { "leaderboardId", "my-first-leaderboard" } }
                );
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA: " + response);

				Debug.Log("try girdi");
                (UserDataRoot userDataRoot, UserData userData, int index) = dataService.GetCurrentUserData();
				Debug.Log("try girdi 2");
                // leaderboard zamanı geçmemişse
                if (!(userData.leaderboardReward.leaderboardResetDateTime <= DateTime.Now))
                {
                    Debug.Log($"leaderboardResetDateTime: {userData.leaderboardReward.leaderboardResetDateTime}\nNow: {DateTime.UtcNow}");
                    Debug.Log("ififififififiififififiifiif");
                    return;
                }
				Debug.Log("if girdi 1");

                if (userData.leaderboardReward.liveLeaderboardResetDateTime <= DateTime.Now)
                {
                    Debug.Log("updateupdateupdateupdate " + userData.leaderboardReward.liveLeaderboardResetDateTime + " " + DateTime.UtcNow);
                    // LeaderboardReward leaderboardReward = await CloudCodeService.Instance.CallModuleEndpointAsync<LeaderboardReward>(
                    // ModuleName,
                    // "UpdateDates",
                    // new Dictionary<string, object> { { "leaderboardId", "my-first-leaderboard" } }
                    // );

                    // userData.leaderboardReward.leaderboardResetDateTime = leaderboardReward.leaderboardResetDateTime;
                    // userData.leaderboardReward.liveLeaderboardResetDateTime = leaderboardReward.liveLeaderboardResetDateTime;
                    // userData.leaderboardReward.isRewardAvaliable = leaderboardReward.isRewardAvaliable;
                    // userData.leaderboardReward.isRewardGranted = leaderboardReward.isRewardGranted;
                }
				
				Debug.Log("if girdi 2");
                // LeaderboardReward reward =
                // await CloudCodeService.Instance.CallModuleEndpointAsync<LeaderboardReward>(
                // ModuleName,
                // "GetLeaderboardReward",
                // new Dictionary<string, object> { { "leaderboardId", "my-first-leaderboard" } }
                // );

            //     if (reward.isRewardAvaliable && reward.isRewardGranted == false)
            //     {
			// 		Debug.Log("if girdi 3");
            //         //MoneyManager.Instance.AddMoney(userData.leaderboardReward.rewardAmount);

            //         //userData.money.moneyAmount += MoneyManager.Instance.MoneyValue;
            //         reward.isRewardAvaliable = false;
            //         reward.isRewardGranted = true;

            //         userData.leaderboardReward = reward;

            //         userDataRoot.userDatas[index] = userData;
            //         if (dataService.SaveData(userDataRoot, index, false) && AuthenticationService.Instance.IsAuthorized)
            //             _ = cloudSave.SaveData();

            //         leaderboardRewardPanel.SetActive(true);
            //         textMessage.text = reward.message;
            //         buttonClaim.onClick.AddListener(() => leaderboardRewardPanel.SetActive(false));
            //     }

            //     else
            //     {
            //         leaderboardRewardPanel.SetActive(false);
			// 		Debug.Log("else girdi 1");
            //     }
            }
            catch (CloudCodeException e)
            {
				Debug.Log("catch girdi 1");
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<bool> WaitForLogin()
        {
            float duration = 0f;
            float maxDuration = 180f;
            while (!AuthenticationService.Instance.IsAuthorized && duration <= maxDuration)
            {
                await Task.Delay(250);
                duration += Time.deltaTime;
            }

            return duration <= 60f ? true : false;
        }
    }
}
