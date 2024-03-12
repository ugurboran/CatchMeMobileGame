using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneVibrationInitializer : MonoBehaviour
{
    private void Start()
    {
        // Tüm sahnelerdeki titreşim kontrolünü sağlamak için GlobalTitresimKontrolu script'ini bulun.
        GlobalVibrationController globalTitresimKontrolu = FindObjectOfType<GlobalVibrationController>();
        
        if (globalTitresimKontrolu != null)
        {
            Button titreşimKontrolButonu = GetComponent<Button>();
            titreşimKontrolButonu.onClick.AddListener(() => GlobalVibrationController.TitresimAcKapat());
        }
    }
}
