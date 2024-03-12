using System;
using System.Threading.Tasks;
using System.Collections;
using DG.Tweening;
using Save_System.JSON;
using Save_System.Model;
using Save_System.JSON.Model;
//using Scene;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

namespace Save_System.Cloud
{
	public class Login : MonoBehaviour
	{
		// [SerializeField] private CloudSaveSystem cloudSaveSystem;
		[SerializeField] private CloudSave cloudSave;
		private readonly IDataService dataService = new JsonDataService();
		
		private readonly string IdKey = "userId";
		public const string guestUserId = "Guest";
		
		[SerializeField] private Button buttonLoginGooglePlay;
		[SerializeField] private Button buttonLoginGuest;
		
		[SerializeField] private TMP_Text textLog;
		[SerializeField] private GameObject imageLoading;
		[SerializeField] private GameObject MenuButtons;
		//public bool isLogSuccess = false;
		//public bool isLogGuestSuccess = false;
		
		//private bool isLoadOrSaveCompleted = false;

		private void Start() {
			if(LoginHolder.isLogSuccess || LoginHolder.isLogGuestSuccess){
				HideButtons();
				MenuButtons.SetActive(true);
			}
			else{
				ShowButtons();
			}
		}

		private void OnEnable()
		{
			buttonLoginGooglePlay.onClick.AddListener(() =>
			{
				UpdateLogText("Logging in...");
				StartCoroutine(nameof(LoginCloud));
			});
			buttonLoginGuest.onClick.AddListener(LoginAsGuest);
		}

		private IEnumerator LoginCloud()
		{
			
			HideButtons();

			yield return StartCoroutine(cloudSave.SignIn());

			ShowButtons();
			if (AuthenticationService.Instance.IsAuthorized)
			{
				Debug.Log("Authorized?");
				Task<PlayerInfo> t = AuthenticationService.Instance.GetPlayerInfoAsync();

				if (t.IsCompletedSuccessfully)
				{
					PlayerPrefs.SetString(IdKey, t.Result.Id);
					MenuButtons.SetActive(false);
					
				}
				//SceneManager.Instance.LoadLevel(1);

				// var userInfo = dataService.GetCurrentUserData();
        		// UserDataRoot userDataRoot = userInfo.Item1;
        		// UserData userData = userInfo.Item2;
        		// int index = userInfo.Item3;

				DisableLogText();
				HideButtons();
				MenuButtons.SetActive(true);
				UpdateLogTextOnly("Login Successfull.");
				LoginHolder.isLogSuccess = true;
				//isLoadOrSaveCompleted = true;
				//TO-DO Login gerçekleşirse save çek, else kısmına gşremezse try again veya guest 
			} else 
			{ 
				Debug.Log("belirleyici bir şey");
				textLog.text = "Login failed try again or login as Guest!";
				var loadingTransform = imageLoading.transform;
				loadingTransform.DOKill();
				loadingTransform.eulerAngles = Vector3.zero;
				imageLoading.SetActive(false);
			}
		}


		private void LoginAsGuest()
		{
			HideButtons();

			// CloudSaveSystem.userId = "Guest";
			PlayerPrefs.SetString("userId", guestUserId);

			var (userDataRoot, userData, index) = dataService.GetCurrentUserData();

			userData.id = guestUserId;

			userDataRoot.userDatas[index] = userData;
			dataService.SaveData(userDataRoot, index, false);
			
			//SceneManager.Instance.LoadLevel(1);
			MenuButtons.SetActive(true);
			LoginHolder.isLogGuestSuccess = true;

		}

		// private IEnumerator LoginWithGooglePlay()
		// {
		// 	HideButtons();
		// 	cloudSave.SignIn();

		//    yield return new WaitUntil(() => isLoadOrSaveCompleted);

		//    isLoadOrSaveCompleted = false;

		// 	if (Social.localUser.authenticated)
		// 	{
		// 		ShowButtons();
		// 		//SceneManager.Instance.LoadLevel(1);
		// 	}
		// }

		public void UpdateLogText(string message)
		{
			textLog.gameObject.SetActive(true);
			textLog.text = message;
			imageLoading.SetActive(true);
			imageLoading.transform.DORotate(new Vector3(0, 0, -360), 2f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
		}

		public void UpdateLogTextOnly(string message)
		{
			textLog.gameObject.SetActive(true);
			textLog.text = message;
			//imageLoading.SetActive(true);
			//imageLoading.transform.DORotate(new Vector3(0, 0, -360), 2f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
		}

		public void DisableLogText()
		{
			textLog.gameObject.SetActive(false);
			//textLog.text = message;
			imageLoading.SetActive(false);
			//imageLoading.transform.DORotate(new Vector3(0, 0, -360), 2f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
		}

		public void HideButtons()
		{
			buttonLoginGooglePlay.gameObject.SetActive(false);
			buttonLoginGuest.gameObject.SetActive(false);
		}

		public void ShowButtons()
		{
			buttonLoginGooglePlay.gameObject.SetActive(true);
			buttonLoginGuest.gameObject.SetActive(true);
			// imageLoading.SetActive(false);
		}
	}
}