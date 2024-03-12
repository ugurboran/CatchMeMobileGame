using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class GPGAuth : MonoBehaviour, IAuth
{
	private const string AuthToken = "authToken";
	private const string UserId = "userId";

	private string authToken;
	public bool IsSignedInCompleted { get; set; }

	public string LogMessage { get; set; }

	public void SignIn()
	{
		LogMessage = "Signing in...";
		PlayGamesPlatform.Instance.Authenticate((status) =>
		{

			Debug.Log($"status: {status}");
			if (status == SignInStatus.Success)
			{
				LogMessage = $"{status}";
				PlayGamesPlatform.Instance.RequestServerSideAccess(true, async token =>
				{
					LogMessage = "Requesting server auth code...";
					authToken = token;

					try
					{
#if UNITY_ANDROID
						LogMessage = "Signing in with Google Play Games...";
						await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authToken);

						PlayerPrefs.SetString(AuthToken, authToken);
						PlayerPrefs.SetString(UserId, AuthenticationService.Instance.PlayerId);
#endif
						LogMessage = $"Signed in with Google Play Games...\nWelcome {AuthenticationService.Instance.PlayerId}";
					}
					catch (AuthenticationException e)
					{
						LogMessage = $"Authentication Exception: {e.Message}";
						Debug.LogError($"Authentication Exception: {e.Message}");
					}
					catch (RequestFailedException e)
					{
						LogMessage = $"Request Failed Exception: {e.Message}";
						Debug.LogError($"Request Failed Exception: {e.Message}");
					}
					IsSignedInCompleted = true;
				});
			}
			else
			{
				LogMessage = $"Failed to retrieve Google play games authorization code";
				Debug.LogError("Failed to retrieve Google play games authorization code");

				authToken = null;
				PlayerPrefs.SetString(AuthToken, null);
				PlayerPrefs.SetString(UserId, null);
				IsSignedInCompleted = true;
			}
		});
	}

	public void SignOut()
	{
		throw new System.NotImplementedException();
	}
}
