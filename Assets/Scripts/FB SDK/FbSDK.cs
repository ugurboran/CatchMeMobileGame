using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FbSDK : MonoBehaviour
{
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            Debug.LogWarning("Facebook SDK Initializing...");
            FB.Init(InitCallback, OnHideUnity);
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            Debug.LogWarning("Facebook SDK initialized successfully!");
        }
        else
            Debug.LogWarning("Failed to Initialize the Facebook SDK");
    }

    private void OnHideUnity(bool isUnityShown)
    {
        if (!isUnityShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
