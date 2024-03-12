using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public GameObject enemySpawner;
    public GameObject[] taggedObjects;
    public CountDownController countDownController;
    public Health healthScript;
    public float tempSpeed;
    public float tempRewardedSpeed;
    public bool isRewardedContinue;
    public bool isContinue;

    //[SerializeField] private InputActionReference movementControl;
    //[SerializeField] private InputActionReference jumpControl;

    [SerializeField] public float currentSpeed = 5.0f;
    [SerializeField] public float initialSpeed = 5.0f;
    [SerializeField] private float maxSpeed = 20.0f;
    [SerializeField] private float speedIncreaseRate = 0.1f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 4f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraMainTransform;
    private Transform playerTransform;
    private PlayerControls playerInput;
    private Transform child;
    private Transform eyes;
    public long lastCollisionMoment;
    private GameObject jumpButton;
    private GameObject jumpSuperButton;
    private Image jumpButtonImage;
    private Image jumpSuperButtonImage;
    private Image jumpButtonChildImage;
    private Image jumpSuperButtonChildImage;
    public GameObject collisionParticle;
    public GameObject jumpButtonText;
    public GameObject jumpSuperButtonText;
    //public GameObject jumpSuperButtonText;
    private bool isSceneTutorial;
    private GameObject tutorialRestartTextObject;
    private TextMeshProUGUI tutorialRestartTextMessage;
    private Image tutorialRestartTextImage;
    private GameObject tutorialRestartButton;
    public bool isPlayerImmortal;
    public GameObject playerWithTransport;
    public Sprite heartSprite;
    public Image heartImage;
    public GameObject heartImageObject;
    public SoundManager soundManager;
    public GameObject AdsPanel;
    public Button interstitialAdsButton;
    public Button rewardedAdsButton;
    public GameObject intersitialAdsObject;
    public GameObject rewardedAdsObject;
    //private TextMeshProUGUI jumpButtonTextMessage;

    private void Awake()
    {
        playerInput = new PlayerControls();
        controller = GetComponent<CharacterController>();

        jumpButton = GameObject.FindGameObjectWithTag("JumpButton");
        jumpButtonImage = jumpButton.GetComponent<Image>();
        jumpButtonChildImage = jumpButton.transform.GetChild(1).GetComponent<Image>();


        jumpSuperButton = GameObject.FindGameObjectWithTag("SuperJumpButton");
        jumpSuperButtonImage = jumpSuperButton.GetComponent<Image>();
        jumpSuperButtonChildImage = jumpSuperButton.transform.GetChild(1).GetComponent<Image>();


    }


    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
    private void Start()
    {

        playerWithTransport = GameObject.FindGameObjectWithTag("Player");

        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();



        if (GameObject.FindGameObjectWithTag("CarTeam"))
        {
            //Debug.Log("Özellikleri aktifle");
            healthScript.numOfHearts = 4;
            healthScript.health = 4;
            //healthScript.hearts[3].sprite = heartSprite;
            //healthScript.hearts.Length = 4;
            heartImageObject.SetActive(true);
            healthScript.hearts[3] = heartImage;

            playerWithTransport.GetComponent<CharacterController>().radius = 2f;
            playerWithTransport.GetComponent<CharacterController>().height = 0f;
            playerWithTransport.GetComponent<CharacterController>().center = new Vector3(0f, 2.25f, 0f);
        }

        if (GameObject.FindGameObjectWithTag("SkateTeam"))
        {
            //Debug.Log("Özellikleri aktifle 2");
            initialSpeed = 10.0f;
            maxSpeed = 25.0f;
        }

        if (GameObject.FindGameObjectWithTag("TaxiTeam"))
        {
            //Debug.Log("Özellikleri aktifle 3");
            rotationSpeed = 90f;
        }
        //isPlayerImmortal = true;
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            isSceneTutorial = true;
            tutorialRestartTextObject = GameObject.FindGameObjectWithTag("TutorialRestartText");
            tutorialRestartTextMessage = tutorialRestartTextObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            tutorialRestartTextImage = tutorialRestartTextObject.GetComponent<Image>();
            tutorialRestartButton = GameObject.FindGameObjectWithTag("TutorialRestartButton");
            tutorialRestartButton.SetActive(false);
        }
        else
        {
            isSceneTutorial = false;

            isRewardedContinue = false;
            isContinue = false;
            taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");
            intersitialAdsObject.transform.GetChild(0).gameObject.SetActive(false);
            intersitialAdsObject.transform.GetChild(1).gameObject.SetActive(false);
            intersitialAdsObject.GetComponent<Image>().enabled = false;
            rewardedAdsObject.transform.GetChild(0).gameObject.SetActive(false);
            rewardedAdsObject.transform.GetChild(1).gameObject.SetActive(false);
            rewardedAdsObject.GetComponent<Image>().enabled = false;
            AdsPanel.SetActive(false);
        }
        //Time.timeScale = 1;
        child = transform.GetChild(0).transform;
        playerTransform = gameObject.transform;
        lastCollisionMoment = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        //Debug.Log(lastCollisionMoment);
        currentSpeed = initialSpeed;

        jumpButtonText = GameObject.FindGameObjectWithTag("JumpButtonText");

        if (isSceneTutorial == true)
        {
            jumpSuperButtonText = GameObject.FindGameObjectWithTag("JumpSuperButtonText");
            jumpSuperButtonText.SetActive(false);
        }

        //jumpButtonTextMessage = jumpButtonText.GetComponent<TextMeshProUGUI>();
        jumpButtonText.SetActive(false);

        //jumpSuperButtonText = GameObject.FindGameObjectWithTag("JumpSuperButtonText");
    }

    void Update()
    {

        // Hız artışı
        currentSpeed += speedIncreaseRate * Time.deltaTime;

        // Hız sınırlaması
        currentSpeed = Mathf.Clamp(currentSpeed, initialSpeed, maxSpeed);

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movementInput = playerInput.Player.Movement.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);
        move = Vector3.ClampMagnitude(move, 1f); // Hareket vektörünü normalize ediyoruz.

        if (move.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }



        controller.Move(move * Time.deltaTime * currentSpeed);

        // //Changes the height position of the player..
        // if (playerInput.Player.Jump.triggered && groundedPlayer)
        // {
        //     //playerTransform.localScale = playerTransform.localScale * 2;

        //     // Scale the GameObject to the target scale over the specified duration
        //     //playerTransform.DOScale(Vector3.one * 2f, 2f).SetEase(Ease.OutQuad);

        //     float scaleDuration = 1f;
        //     float originalScale = 1f;
        //     float targetScale = 2f;

        //     // Scale the GameObject to the target scale over scaleDuration seconds with ease-out quad easing function
        //     playerTransform.DOScale(Vector3.one * targetScale, scaleDuration).SetEase(Ease.OutQuad)
        //         .OnComplete(() =>
        //         {
        //             // Once the scaling up is complete, start the scaling down
        //             playerTransform.DOScale(Vector3.one * originalScale, scaleDuration).SetEase(Ease.OutQuad);
        //         });


        //     playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        //     StartCoroutine(DelayedJumpScale(1f));
        //     jumpButtonImage.enabled = false;
        //     jumpButtonChildImage.enabled = false;

        // }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        //controller.Move(playerVelocity.x * playerTransform.forward * Time.deltaTime); // Hız ile Rotasyon yavaş bir dönüş mekaniği dene

        // if(movementInput != Vector2.zero){
        //     playerTransform.rotation = Quaternion.Euler(0f, Mathf.Sign(movementInput.x) * 90f, 0f);
        // }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // İki nesne çarpıştıysa bu kod bloğu çalışır.

            GameObject effect = Instantiate(collisionParticle, collision.contacts[0].point, Quaternion.identity);
            long temp = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            Debug.Log("health = " + healthScript.health);

            if (isPlayerImmortal == true)
            {

                Debug.Log("Immortal");
            }
            else
            {
                if (temp - 2000 > lastCollisionMoment)
                {
                    healthScript.health--;
                    //Vibrator.Vibrate();
                    //Vibrator.Vibrate(1000);
                    Handheld.Vibrate();
                    lastCollisionMoment = temp;
                }

                if (healthScript.health == 0)
                {
                    if (isSceneTutorial == true)
                    {
                        tutorialRestartTextMessage.enabled = true;
                        tutorialRestartTextImage.enabled = true;
                        tutorialRestartButton.SetActive(true);
                        Time.timeScale = 0;
                    }
                    else
                    {
                        Time.timeScale = 0;
                        AdsPanel.SetActive(true);
                        isContinue = true;
                        intersitialAdsObject.transform.GetChild(0).gameObject.SetActive(true);
                        intersitialAdsObject.transform.GetChild(1).gameObject.SetActive(true);
                        rewardedAdsObject.transform.GetChild(0).gameObject.SetActive(true);
                        rewardedAdsObject.transform.GetChild(1).gameObject.SetActive(true);

                        intersitialAdsObject.GetComponent<Image>().enabled = true;
                        rewardedAdsObject.GetComponent<Image>().enabled = true;
                        //Debug.Log("Game Over");
                        //SceneManager.LoadScene(2);
                    }

                    //Time.timeScale = 0;
                    //TryAgainButton.SetActive(true);
                }

                // Çarpıştığınız nesneye göre yapmak istediğiniz işlemleri burada gerçekleştirebilirsiniz.
            }

        }
    }

    public void ContinueGame()
    {
        AdsPanel.SetActive(false);
        Time.timeScale = 1;
        healthScript.health = 1;
        interstitialAdsButton.interactable = false;

        tempSpeed = currentSpeed;

        isPlayerImmortal = true;

        taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj in taggedObjects)
        {
            obj.SetActive(false);
        }

        //playerWithTransport.GetComponent<Collider>().enabled = false;
        currentSpeed = 0f;
        //enemySpawner.SetActive(false);
        StartCoroutine(countDownController.CountdownToStart());
        //currentSpeed = tempSpeed;

        Debug.Log(" speed =============" + currentSpeed);

    }

    public void ContinueGameRewarded()
    {
        AdsPanel.SetActive(false);
        Time.timeScale = 1;
        healthScript.health = 2;
        rewardedAdsButton.interactable = false;

        tempRewardedSpeed = currentSpeed;

        isPlayerImmortal = true;
        isRewardedContinue = true;

        taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj in taggedObjects)
        {
            obj.SetActive(false);
        }
        currentSpeed = 0f;

        //enemySpawner.SetActive(false);
        StartCoroutine(countDownController.CountdownToStart());

        //currentSpeed = tempRewardedSpeed;

        Debug.Log(" speed =============" + currentSpeed);
    }

    public void DeactivateAdsButtons()
    {
        intersitialAdsObject.transform.GetChild(0).gameObject.SetActive(false);
        intersitialAdsObject.transform.GetChild(1).gameObject.SetActive(false);
        intersitialAdsObject.GetComponent<Image>().enabled = false;
        rewardedAdsObject.transform.GetChild(0).gameObject.SetActive(false);
        rewardedAdsObject.transform.GetChild(1).gameObject.SetActive(false);
        rewardedAdsObject.GetComponent<Image>().enabled = false;
    }

    IEnumerator DelayedJumpScale(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        playerTransform.localScale = playerTransform.localScale / 2;
    }

    public void ButonJumpActivate()
    {
        //playerTransform.localScale = playerTransform.localScale * 2;

        // Scale the GameObject to the target scale over the specified duration
        //playerTransform.DOScale(Vector3.one * 2f, 2f).SetEase(Ease.OutQuad);

        float scaleDuration = 1f;
        float originalScale = 1f;
        float targetScale = 2f;

        // Scale the GameObject to the target scale over scaleDuration seconds with ease-out quad easing function
        playerTransform.DOScale(Vector3.one * targetScale, scaleDuration).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // Once the scaling up is complete, start the scaling down
                playerTransform.DOScale(Vector3.one * originalScale, scaleDuration).SetEase(Ease.OutQuad);
            });


        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        StartCoroutine(DelayedJumpScale(1f));
        jumpButtonImage.enabled = false;
        jumpButtonChildImage.enabled = false;
        jumpButtonText.SetActive(false);
        //jumpButtonTextMessage.enabled = false;
    }

    public void ButonSuperJumpActivate()
    {
        //playerTransform.localScale = playerTransform.localScale * 2;

        // Scale the GameObject to the target scale over the specified duration
        //playerTransform.DOScale(Vector3.one * 2f, 2f).SetEase(Ease.OutQuad);

        float scaleDuration = 2f;
        float originalScale = 1f;
        float targetScale = 7.5f;

        soundManager.CharacterWuhuSoundOn();
        // Scale the GameObject to the target scale over scaleDuration seconds with ease-out quad easing function
        playerTransform.DOScale(Vector3.one * targetScale, scaleDuration).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // Once the scaling up is complete, start the scaling down
                playerTransform.DOScale(Vector3.one * originalScale, scaleDuration).SetEase(Ease.OutQuad);
            });


        playerVelocity.y += Mathf.Sqrt(jumpHeight * -7.5f * gravityValue);

        StartCoroutine(DelayedJumpScale(1f));
        Debug.Log("Why?");
        jumpSuperButtonImage.enabled = false;
        jumpSuperButtonChildImage.enabled = false;
        if (isSceneTutorial == true)
        {
            jumpSuperButtonText.SetActive(false);
        }

        //jumpButtonTextMessage.enabled = false;
    }
}