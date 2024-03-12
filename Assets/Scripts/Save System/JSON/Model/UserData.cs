using System;
using Save_System.Model;

namespace Save_System.JSON.Model
{
	[Serializable]
	public class UserData
	{
		// user play games id
		public string id;
		// Json objeleri.
		public Character characterData;
		public Money money;
		public LeaderboardReward leaderboardReward;
	}
}