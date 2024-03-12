using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInitializer : MonoBehaviour
{
    private void Start()
    {
        // Tüm sahnelerdeki ses kontrolünü sağlamak için GlobalSesKontrolu script'ini ekleyin.
        GlobalSoundController globalSoundController = FindObjectOfType<GlobalSoundController>();
        if (globalSoundController != null)
        {
            Button sesKontrolButonu = GetComponent<Button>();
            sesKontrolButonu.onClick.AddListener(GlobalSoundController.SesiAcKapat);
        }
    }
}
