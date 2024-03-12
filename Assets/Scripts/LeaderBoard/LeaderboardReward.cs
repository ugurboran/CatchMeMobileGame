using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Save_System.Model
{
    public class LeaderboardReward
    {
       public string rewardId;
       public string rewardName;
       public int rewardAmount;
       public bool isRewardGranted;
       public bool isRewardAvaliable;
       public string message;
       public int playerRank;
       public DateTime leaderboardResetDateTime;

       public DateTime liveLeaderboardResetDateTime;

       //public DateTime leaderboardResetDateTime;
    }
}
