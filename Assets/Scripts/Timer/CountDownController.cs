using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownController : MonoBehaviour
{
    public bool countDownOn = false;
    //public int countdownTime;
    public float tempSpeed;
    //public TextMeshProUGUI countdownDisplay;
    //public TextMeshProUGUI countdownDisplayEffect;
    public GameObject GOText1;
    public GameObject GOText2;
    public GameObject GOText3;
    public GameObject GOTextGO;

    public GameObject enemySpawner;
    public PlayerController playerController;
    public SoundManager soundManager;

    private void Start()
    {
        
        GOText2.SetActive(false);
        GOText1.SetActive(false);
        GOTextGO.SetActive(false);

        //enemySpawner.SetActive(false);
        StartCoroutine(CountdownToStart());
        
        tempSpeed = playerController.initialSpeed;
        playerController.initialSpeed = 0f;

    }
    public IEnumerator CountdownToStart()
    {
        countDownOn = true;
        if (playerController.isContinue == true){
            playerController.initialSpeed = 0f;
            playerController.currentSpeed = 0f;
            
        }
        //countdownDisplay.text = countdownTime.ToString();
        //countdownDisplayEffect.text = countdownTime.ToString();
        GOText3.SetActive(true);
        soundManager.CountdownSoundOn();
        yield return new WaitForSeconds(1f);
        GOText2.SetActive(true);
        GOText3.SetActive(false);
        soundManager.CountdownSoundOn();
        yield return new WaitForSeconds(1f);
        GOText1.SetActive(true);
        GOText2.SetActive(false);
        soundManager.CountdownSoundOn();
        yield return new WaitForSeconds(1f);
        GOTextGO.SetActive(true);
        GOText1.SetActive(false);
        soundManager.CountdownGoSoundOn();

        //countdownDisplay.text = "GO!";
        //countdownDisplayEffect.text = "GO!";
        
        if (playerController.isContinue == true)
        {
            if (playerController.isRewardedContinue == true){
                playerController.initialSpeed = playerController.tempRewardedSpeed;
            }
            else{
                playerController.initialSpeed = playerController.tempSpeed;
            }
        }else{
            playerController.initialSpeed = tempSpeed;
            //playerController.currentSpeed = tempSpeed;
        }
        
        //enemySpawner.SetActive(true);

        yield return new WaitForSeconds(1f);
        GOTextGO.SetActive(false);
        GOText1.SetActive(false);
        GOText2.SetActive(false);
        GOText3.SetActive(false);

        


        foreach (GameObject obj in playerController.taggedObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }

        }

        yield return new WaitForSeconds(2f);
        

        playerController.isPlayerImmortal = false;
        countDownOn = false;

        //playerController.playerWithTransport.GetComponent<Collider>().enabled = true;


        //countdownDisplay.gameObject.SetActive(false);
        //countdownDisplayEffect.gameObject.SetActive(false);


    }
}
