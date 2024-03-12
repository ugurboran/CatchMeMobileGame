using Newtonsoft.Json;
using Save_System;
using Save_System.JSON;
using Save_System.JSON.Model;
using Save_System.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Authentication;

public class CloudSave : MonoBehaviour
{
	[SerializeField] private GPGAuth gpgAuth;
	private readonly IDataService dataService = new JsonDataService();

	private IAuth auth;

	private void Start()
	{
		//TO-DO : Comment these or not ? Test it...
		//UnityServices.InitializeAsync();
		




		
		//StartCoroutine(SignIn());
	}

	public IEnumerator SignIn()
	{
#if UNITY_ANDROID
		auth = gpgAuth;
#endif
		auth.SignIn();

		yield return new WaitUntil(() => auth.IsSignedInCompleted == true);

		var loadAllDataTask = LoadAllData().ContinueWith((loadedData) =>
		{
			// SaveToJson(loadedData.Result["userData"]);
		});

		Debug.Log("XXX");
		yield return new WaitUntil(() => loadAllDataTask.IsCompleted);
		Debug.Log("XXX-2");
	}

	/// <summary>
	/// Save data to cloud save.
	/// </summary>
	/// <returns></returns>
	public async Task SaveData()
	{
		var data = GetJsonData();

		await CloudSaveService.Instance.Data.ForceSaveAsync(data);
	}

	/// <summary>
	/// Get the data from json file.
	/// </summary>
	/// <returns>json data key and value dict.</returns>
	private Dictionary<string, object> GetJsonData()
	{
		try
		{
			UserData userData;
			(_, userData, _) = dataService.GetCurrentUserData();
			var data = JsonConvert.SerializeObject(userData, Formatting.Indented, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});

			return new Dictionary<string, object> { {"userData", data } };
		}
		catch (Exception e)
		{
			Debug.LogException(e);
			throw;
		}
	}

	/// <summary>
	/// Save data to cloud save.
	/// </summary>
	/// <param name="data">A Dictionary property which desired has key and value.</param>
	/// <returns></returns>
	public async Task SaveData(Dictionary<string, object> data)
	{
		await CloudSaveService.Instance.Data.ForceSaveAsync(data);
	}

	/// <summary>
	/// Loads all saved data from cloud.
	/// </summary>
	/// <returns>Loaded data</returns>
	public async Task<Dictionary<string, string>> LoadAllData()
	{
		var data = await CloudSaveService.Instance.Data.LoadAllAsync();

		return data;
	}

	/// <summary>
	/// Loads the data according to the given key.
	/// </summary>
	/// <param name="key">The key value of the Hashset<string> type of the data to be loaded.</param>
	/// <returns>Loaded data as Dictionary<key = string, value = string></returns>
	public async Task<Dictionary<string, string>> LoadData(HashSet<string> key)
	{
		var loadedData = await CloudSaveService.Instance.Data.LoadAsync(key);

		return loadedData;
	}
}