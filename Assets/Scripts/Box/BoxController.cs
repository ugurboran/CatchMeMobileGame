using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BoxController : MonoBehaviour
{
    private SoundManager soundManager;
    public CoroutineManager coroutineManager;
    private PlayerController playerController;
    private GameObject jumpButton;
    private GameObject jumpSuperButton;
    private GameObject boxSkillText;
    private GameObject boxHealthText;
    private GameObject boxHealthFullText;
    private GameObject jumpButtonText;
    private GameObject boxImmortalText;
    private GameObject boxSuperJumpSkillText;
    private Image jumpButtonImage;
    private Image jumpSuperButtonImage;
    private TextMeshProUGUI boxSkillTextMessage;
    private TextMeshProUGUI boxHealthTextMessage;
    private TextMeshProUGUI boxHealthFullTextMessage;
    private TextMeshProUGUI boxImmortalSkillTextMessage;
    private TextMeshProUGUI boxSuperJumpSkillTextMessage;
    private Image boxSkillTextImage;
    private Image boxHealthTextImage;
    private Image boxHealthFullTextImage;
    private Image boxImmortalSkillTextImage;
    private Image boxSuperJumpSkillTextImage;
    //private TextMeshProUGUI jumpButtonTextMessage;
    private Image jumpButtonChildImage;
    private Image jumpSuperButtonChildImage;
    private BoxManager boxManager;
    private Health healthScript;
    private bool isSceneTutorial;
    private GameObject immortalButton;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Tutorial"){
            isSceneTutorial = true;
        }
        else{
            isSceneTutorial = false;
        }
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        coroutineManager = GameObject.FindGameObjectWithTag("CoroutineManager").GetComponent<CoroutineManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //jumpButtonText.SetActive(true);
        jumpButton = GameObject.FindGameObjectWithTag("JumpButton");
        jumpSuperButton = GameObject.FindGameObjectWithTag("SuperJumpButton");
        boxSkillText = GameObject.FindGameObjectWithTag("BoxSkillText");
        boxHealthText = GameObject.FindGameObjectWithTag("BoxHealthText");
        boxHealthFullText = GameObject.FindGameObjectWithTag("BoxHealthFullText");
        boxImmortalText = GameObject.FindGameObjectWithTag("BoxImmortalText");
        boxSuperJumpSkillText = GameObject.FindGameObjectWithTag("BoxSuperJumpSkillText");

        immortalButton = GameObject.FindGameObjectWithTag("ImmortalButton").transform.GetChild(2).gameObject;

        //boxSkillText.SetActive(false);
        //boxHealthText.SetActive(false);
        //boxHealthFullText.SetActive(false);

        //jumpButtonText = GameObject.FindGameObjectWithTag("JumpButtonText");
        //jumpButtonText.SetActive(false);
        jumpButtonImage = jumpButton.GetComponent<Image>();
        jumpButtonChildImage = jumpButton.transform.GetChild(1).GetComponent<Image>();

        jumpSuperButtonImage = jumpSuperButton.GetComponent<Image>();
        jumpSuperButtonChildImage = jumpSuperButton.transform.GetChild(1).GetComponent<Image>();

        boxSkillTextMessage = boxSkillText.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        boxHealthTextMessage = boxHealthText.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        boxHealthFullTextMessage = boxHealthFullText.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        boxImmortalSkillTextMessage = boxImmortalText.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        boxSuperJumpSkillTextMessage = boxSuperJumpSkillText.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        boxSkillTextImage = boxSkillText.GetComponent<Image>();
        boxHealthTextImage = boxHealthText.GetComponent<Image>();
        boxHealthFullTextImage = boxHealthFullText.GetComponent<Image>();
        boxImmortalSkillTextImage = boxImmortalText.GetComponent<Image>();
        boxSuperJumpSkillTextImage = boxSuperJumpSkillText.GetComponent<Image>();

        //jumpButtonTextMessage = jumpButtonText.GetComponent<TextMeshProUGUI>();
        boxManager = GameObject.FindGameObjectWithTag("BoxManager").GetComponent<BoxManager>();
        healthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if(other.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("Triggerlandın");
    //         Destroy(other.gameObject);
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("JumpBox"))
        {
            soundManager.SkillTakeSoundOn();
            boxManager.arrowIndicatorTextObject.SetActive(false);
            StartCoroutine(boxManager.ArrowSkillTextAppear());
            //jumpButton.SetActive(true);
            jumpButtonImage.enabled = true;
            jumpButtonChildImage.enabled = true;

            playerController.jumpButtonText.SetActive(true);
            //jumpButtonTextMessage.enabled = true;
            //boxSkillText.SetActive(true);

            boxSkillTextMessage.enabled = true;
            boxSkillTextImage.enabled = true;

            StartCoroutine(SkillTextDisappear());
            //coroutineManager.StartCoroutine(DestroyAfterDelay(gameObject));
            //Destroy(boxManager.arrowIndicator);

            //Destroy(gameObject); // Burada yaptığımda StartCourutine çalışmıyor obje yok olduğu için o yüzden bu satırı startcoroutine içine attım

            // Burada random olarak skill ekle.
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("HealthBox"))
        {
            
            boxManager.arrowIndicatorTextObject.SetActive(false);
            StartCoroutine(boxManager.ArrowSkillTextAppear());

            if (healthScript.health == 3)
            {
                soundManager.NoMoneySoundOn();
                Debug.Log("Maks health u have, take it when u lost one");

                boxHealthFullTextMessage.enabled = true;
                boxHealthFullTextImage.enabled = true;
                //boxHealthFullText.SetActive(true);
                StartCoroutine(SkillHealthTextDisappear());
            }
            else
            {
                soundManager.HealSkillTakeSoundOn();
                healthScript.health++;
                Debug.Log("hEALTH Value :" + healthScript.health);
                //jumpButton.SetActive(true);
                //jumpButtonImage.enabled = true;
                //jumpButtonChildImage.enabled = true;
                //boxSkillText.SetActive(true);

                boxHealthTextMessage.enabled = true;
                boxHealthTextImage.enabled = true;

                //boxHealthText.SetActive(true);

                StartCoroutine(SkillHealthTextDisappear());
                

                //Destroy(gameObject); // Burada yaptığımda StartCourutine çalışmıyor obje yok olduğu için o yüzden bu satırı startcoroutine içine attım

                // Burada random olarak skill ekle.
            }

        }
        else if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("ImmortalBox")){
            Debug.Log("?");
            soundManager.SkillTakeSoundOn();

            boxManager.arrowIndicatorTextObject.SetActive(false);
            StartCoroutine(boxManager.ArrowSkillTextAppear());

            immortalButton.SetActive(true);

            boxImmortalSkillTextMessage.enabled = true;
            boxImmortalSkillTextImage.enabled = true;

            StartCoroutine(SkillImmortalTextDisappear());
        }
        else if(other.gameObject.CompareTag("Player") && gameObject.CompareTag("SuperJumpBox"))
        {
            soundManager.SkillTakeSoundOn();
            boxManager.arrowIndicatorTextObject.SetActive(false);
            StartCoroutine(boxManager.ArrowSkillTextAppear());
            //jumpButton.SetActive(true);
            jumpSuperButtonImage.enabled = true;
            jumpSuperButtonChildImage.enabled = true;

            // playerController.jumpButtonText.SetActive(true);
            if(isSceneTutorial == true){
                playerController.jumpSuperButtonText.SetActive(true);
            }
            
            //jumpButtonTextMessage.enabled = true;
            //boxSkillText.SetActive(true);

            boxSuperJumpSkillTextMessage.enabled = true;
            boxSuperJumpSkillTextImage.enabled = true;

            StartCoroutine(SkillSuperJumpTextDisappear());
            //coroutineManager.StartCoroutine(DestroyAfterDelay(gameObject));
            //Destroy(boxManager.arrowIndicator);

            //Destroy(gameObject); // Burada yaptığımda StartCourutine çalışmıyor obje yok olduğu için o yüzden bu satırı startcoroutine içine attım

            // Burada random olarak skill ekle.
        }
    }

    // private IEnumerator SkillTextDisappear()
    // {
    //     yield return new WaitForSeconds(1.5f);

    //     boxSkillTextMessage.enabled = false;
    //     boxSkillTextImage.enabled = false;
    //     //boxSkillText.SetActive(false);

    //     Destroy(gameObject); // Normalde OnTriggerEnter içinde çalışması lazım.
    //     Destroy(boxManager.arrowIndicator);
    // }

    private IEnumerator SkillTextDisappear()
    {
        // Start the coroutine on the CoroutineManager to destroy the object after delay
        if(isSceneTutorial == true){
            yield return new WaitForSeconds(0.5f);
        }
        else{
            yield return new WaitForSeconds(0.1f);
        }
        
        coroutineManager.StartCoroutine(DestroyAfterTextDelay());
        Destroy(gameObject);
        Destroy(boxManager.arrowIndicator);
    }

    private IEnumerator SkillHealthTextDisappear()
    {
        if(isSceneTutorial == true){
            yield return new WaitForSeconds(0.5f);
        }
        else{
            yield return new WaitForSeconds(0.1f);
        }

        //Destroy(gameObject); // Normalde OnTriggerEnter içinde çalışması lazım.
        coroutineManager.StartCoroutine(DestroyAfterHealthTextDelay());
        Destroy(gameObject);
        Destroy(boxManager.arrowHealthIndicator);

    }

    private IEnumerator SkillImmortalTextDisappear()
    {
        if(isSceneTutorial == true){
            yield return new WaitForSeconds(0.5f);
        }
        else{
            yield return new WaitForSeconds(0.1f);
        }

        //Destroy(gameObject); // Normalde OnTriggerEnter içinde çalışması lazım.

        coroutineManager.StartCoroutine(DestroyAfterImmortalTextDelay());
        
        Destroy(gameObject);
        Destroy(boxManager.arrowImmortalIndicator);

    }

    private IEnumerator SkillSuperJumpTextDisappear()
    {
        // Start the coroutine on the CoroutineManager to destroy the object after delay
        if(isSceneTutorial == true){
            yield return new WaitForSeconds(0.5f);
        }
        else{
            yield return new WaitForSeconds(0.1f);
        }
        

        coroutineManager.StartCoroutine(DestroyAfterSuperJumpTextDelay());
        Destroy(gameObject);
        Destroy(boxManager.arrowSuperJumpIndicator);
    }

    // private IEnumerator DestroyAfterDelay(GameObject obj, float delay = 0.1f)
    // {
    //     yield return new WaitForSeconds(delay);
    //     Destroy(obj);

    //     yield return new WaitForSeconds(1.5f);

    //     boxSkillTextMessage.enabled = false;
    //     boxSkillTextImage.enabled = false;
    // }

    private IEnumerator DestroyAfterTextDelay( float delay = 1.5f)
    {

        yield return new WaitForSeconds(delay);

        boxSkillTextMessage.enabled = false;
        boxSkillTextImage.enabled = false;
    }

    private IEnumerator DestroyAfterHealthTextDelay( float delay = 1.5f)
    {

        yield return new WaitForSeconds(delay);

        boxHealthTextMessage.enabled = false;
        boxHealthFullTextMessage.enabled = false;
        boxHealthTextImage.enabled = false;
        boxHealthFullTextImage.enabled = false;
    }

    private IEnumerator DestroyAfterImmortalTextDelay( float delay = 1.5f)
    {

        yield return new WaitForSeconds(delay);

        boxImmortalSkillTextMessage.enabled = false;
        boxImmortalSkillTextImage.enabled = false;
    }

    private IEnumerator DestroyAfterSuperJumpTextDelay( float delay = 1.5f)
    {

        yield return new WaitForSeconds(delay);

        boxSuperJumpSkillTextMessage.enabled = false;
        boxSuperJumpSkillTextImage.enabled = false;
    }

}
