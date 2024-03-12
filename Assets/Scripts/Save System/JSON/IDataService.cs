using System;
using System.Collections.Generic;
using UnityEngine;
using Save_System.JSON.Model;
using Save_System.Model;

namespace Save_System
{
	public interface IDataService
	{
		bool SaveData<T>(T data, int index, bool encrypted);
		T LoadData<T>(bool encrypted);
		Tuple<UserDataRoot, UserData, int> GetCurrentUserData();
	}
}
