using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;
using Save_System.JSON.Model;
using Save_System.Model;


namespace Save_System.JSON
{
    public class JsonDataService : IDataService
    {
        private readonly string IdKey = "userId";
        private const string FileName = "data.json";


        public bool SaveData<T>(T data, int index, bool encrypted)
        {
            var path = $"{Application.persistentDataPath}/{FileName}";

            try
            {
                if (File.Exists(path))
                {
                    Debug.Log("Data exists. Deleting old file and writing a new one!");
                    File.Delete(path);
                }
                else
                    Debug.Log("Writing file for first time!");
            }
            catch (Exception e)
            {
                Debug.Log($"Unable to save data due to: {e.Message} {e.StackTrace}");
                return false;
            }

            using var stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

            return true;
        }

        public T LoadData<T>(bool encrypted)
        {
            var path = $"{Application.persistentDataPath}/{FileName}";

            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} does not exist!");

            try
            {
                var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return data;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        public Tuple<UserDataRoot, UserData, int> GetCurrentUserData()
        {
            UserDataRoot userDataRoot;
            int index;

            try
            {
                // Load data çalışmazsa catch'e girer. s76
                userDataRoot = LoadData<UserDataRoot>(false);
                // FindIndex bulamazsa -1 döner. s116
                index = userDataRoot.userDatas.FindIndex(user => user.id == PlayerPrefs.GetString(IdKey));
            }
            catch (Exception e)
            {
                userDataRoot = new UserDataRoot
                {
                    userDatas = new List<UserData>
                    {
                        new()
                        {
                            id = PlayerPrefs.GetString(IdKey),
                            characterData = new Character
                            {
                                characterNumber = 0,
                                carTaken = false,
                                skateTaken = false,
                                taxiTaken = false
                            },

                            leaderboardReward = new LeaderboardReward
                            {
                                isRewardGranted = false,
                                isRewardAvaliable = false,
								leaderboardResetDateTime = DateTime.MinValue,
								liveLeaderboardResetDateTime = DateTime.MinValue
							},

                        }

                    }
                };

                index = 0;
                Debug.LogWarning(e);
            }

            if (index == -1)
            {
                index = userDataRoot.userDatas.Count;
                userDataRoot.userDatas.Add(new UserData
                {
                    id = PlayerPrefs.GetString(IdKey),
                    characterData = new Character
                    {
                        characterNumber = 0,
                        carTaken = false,
                        skateTaken = false,
                        taxiTaken = false
                    },

                    leaderboardReward = new LeaderboardReward
                    {
                        isRewardGranted = false,
                        isRewardAvaliable = false,
						leaderboardResetDateTime = DateTime.MinValue,
						liveLeaderboardResetDateTime = DateTime.MinValue
                    }
                });
            }

            return new Tuple<UserDataRoot, UserData, int>(userDataRoot, userDataRoot.userDatas[index], index);
        }

    }
}