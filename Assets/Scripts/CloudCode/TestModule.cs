using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using Unity.Services.Core;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public class TestModule : MonoBehaviour, IPointerClickHandler
{
    public async void OnPointerClick(PointerEventData eventData)
    {
        await WaitForLogin();
        try
        {
            var result = await CloudCodeService.Instance.CallModuleEndpointAsync("CatchMeCloudCode", "ResetLeaderboard", new Dictionary<string, object> { { "name", "user" } });
            Debug.Log(result);
        }
        catch (CloudCodeException e)
        {
            Debug.LogException(e);
        }
    }
    private static async Task WaitForLogin()
    {
        while (!AuthenticationService.Instance.IsAuthorized)
            await Task.Yield(); // Unity'nin ana iş parçacığını bloke etmemek için Yield kullanın.
    }
}
