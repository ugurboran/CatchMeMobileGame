using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundController : MonoBehaviour
{
    private static bool sesAcik = true;

    public static void SesiAcKapat()
    {
        sesAcik = !sesAcik;
        AudioListener.volume = sesAcik ? 1 : 0;
    }
}
