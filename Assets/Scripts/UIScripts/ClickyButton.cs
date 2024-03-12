using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickyButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image imagePlay;
	[SerializeField] private Sprite playDefaultSprite, playPressedSprite;
    //[SerializeField] private AudioClip compressedClip, uncompressedClip;
    //[SerializeField] private AudioSource source;

    // Play butonuna basılmayı aktif etme
	public void OnPointerDown(PointerEventData eventData)
	{
		imagePlay.sprite =
			playPressedSprite; // Play butonunun butona tıklanmış veya basılmış gibi olan sprite halini image depğerine tanımlama
        //source.PlayOneShot(compressedClip);
		//OnUIButtonClicked?.Invoke();
	}

	// Play Butonuna basılmamış halini simule etme
	void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
	{
		imagePlay.sprite = playDefaultSprite; // Play butonunun normal sprite halini image değerine tanımlama
        //source.PlayOneShot(uncompressedClip);
		//OnUIPlayButtonClicked?.Invoke();
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
