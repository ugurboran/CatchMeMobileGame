using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVibrationController : MonoBehaviour
{
    private static bool titreşimAcik = true;

    public static void TitresimAcKapat()
    {
        titreşimAcik = !titreşimAcik;
        Handheld.Vibrate(); // Titreşimi açmak veya kapatmak için gerekli işlemi yapabilirsiniz.
    }
}
