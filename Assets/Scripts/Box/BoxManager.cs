using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BoxManager : MonoBehaviour
{
    private TextManager textManager;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject healthBoxPrefab;
    [SerializeField] private GameObject immortalBoxPrefab;
    [SerializeField] private GameObject superJumpBoxPrefab;
    [SerializeField] private GameObject arrowIndicatorPrefab;
    private float boxInterval = 10f;
    private float healthBoxInterval = 40f;
    private float boxLifetime = 30f;
    private float healthBoxLifeTime = 30f;
    private float immortalBoxInterval = 20f;
    private float immortalBoxLifetime = 30f;
    private float superJumpBoxInterval = 5f;
    private float superJumpBoxLifetime = 30f;
    private GameObject spawnedBox;
    private GameObject spawnedhealthBox;
    private GameObject spawnedImmortalBox;
    private GameObject spawnedSuperJumpBox;
    public GameObject arrowIndicator;
    public GameObject arrowHealthIndicator;
    public GameObject arrowImmortalIndicator;
    public GameObject arrowSuperJumpIndicator;
    private PlayerController playerController; // Reference to the PlayerController script
    public GameObject arrowIndicatorTextObject;
    //public GameObject arrowIndicatorTextObject1;
    public GameObject tutorialCompletedTextObject;
    private TextMeshProUGUI tutorialCompletedTextMessage;
    private Image tutorialCompletedTextImage;
    private bool tutorialLearned;
    private bool tutorial2Learned;
    private bool tutorial3Learned;
    private bool tutorial4Learned;
    private bool tutorial5Learned;
    public bool isSceneTutorial;
    public bool isTutoComplete;
    public bool isTutoComplete2;
    public bool isTutoComplete3;
    public bool isTutoComplete4;
    public GameObject tutorial2StartButton;
    //public GameObject skipButton2;
    //private TextMeshProUGUI arrowIndicatorText;


    // Start is called before the first frame update
    void Start()
    {
        isTutoComplete = false;
        isTutoComplete2 = false;
        isTutoComplete3 = false;
        isTutoComplete4 = false;
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            textManager = GameObject.FindGameObjectWithTag("TextManager").GetComponent<TextManager>();
            isSceneTutorial = true;
            tutorialCompletedTextObject = GameObject.FindGameObjectWithTag("TutorialCompletedText");
            tutorialCompletedTextMessage = tutorialCompletedTextObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            tutorialCompletedTextImage = tutorialCompletedTextObject.GetComponent<Image>();
            tutorial2StartButton = GameObject.FindGameObjectWithTag("Tutorial2StartButton");
            tutorial2StartButton.SetActive(false);
        }
        else
        {
            isSceneTutorial = false;
        }


        tutorialLearned = false;
        tutorial2Learned = false;
        tutorial3Learned = false;
        tutorial4Learned = false;
        tutorial5Learned = false;


        // skipButton2 = GameObject.FindGameObjectWithTag("SkipButton2");
        // skipButton2.SetActive(false);


        // Find and store the PlayerController script attached to the player object
        playerController = FindObjectOfType<PlayerController>();

        arrowIndicatorTextObject = GameObject.FindGameObjectWithTag("ArrowIndicatorText");
        //arrowIndicatorTextObject1 = GameObject.FindGameObjectWithTag("ArrowIndicatorText1");
        arrowIndicatorTextObject.SetActive(false);
        //arrowIndicatorTextObject1.SetActive(false);
        //arrowIndicatorText = arrowIndicatorTextObject.GetComponent<TextMeshProUGUI>();

        StartCoroutine(SpawnBox(boxInterval, boxPrefab));
        StartCoroutine(SpawnHealthBox(healthBoxInterval, healthBoxPrefab));
        StartCoroutine(SpawnImmortalBox(immortalBoxInterval, immortalBoxPrefab));
        StartCoroutine(SpawnSuperJumpBox(superJumpBoxInterval, superJumpBoxPrefab));

        //StartCoroutine(KillAllSkillTexts());
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the arrow indicator to point towards the spawned box (if it exists)
        if (spawnedBox != null && arrowIndicator != null)
        {
            Vector3 directionToBox = spawnedBox.transform.position - playerController.transform.position;
            arrowIndicator.transform.rotation = Quaternion.LookRotation(directionToBox, Vector3.up);
        }

        if (spawnedhealthBox != null && arrowHealthIndicator != null)
        {
            Vector3 directionToHealthBox = spawnedhealthBox.transform.position - playerController.transform.position;
            arrowHealthIndicator.transform.rotation = Quaternion.LookRotation(directionToHealthBox, Vector3.up);
        }

        if (spawnedImmortalBox != null && arrowImmortalIndicator != null)
        {
            Vector3 directionToImmortalBox = spawnedImmortalBox.transform.position - playerController.transform.position;
            arrowImmortalIndicator.transform.rotation = Quaternion.LookRotation(directionToImmortalBox, Vector3.up);
        }

        if (spawnedSuperJumpBox != null && arrowSuperJumpIndicator != null)
        {
            Vector3 directionToSuperJumpBox = spawnedSuperJumpBox.transform.position - playerController.transform.position;
            arrowSuperJumpIndicator.transform.rotation = Quaternion.LookRotation(directionToSuperJumpBox, Vector3.up);
        }


        StartCoroutine(KillArrowIndicatorSkill());

    }

    private IEnumerator SpawnBox(float interval, GameObject box)
    {

        yield return new WaitForSeconds(interval);
        //spawnedBox = Instantiate(box, new Vector3(Random.Range(-50f, 50), 1, Random.Range(-50f, 50)), Quaternion.identity);
        spawnedBox = Instantiate(box, new Vector3(Random.Range(-40f, 40), 1, Random.Range(-40f, 40)), Quaternion.identity);

        //arrowIndicatorText.enabled = true;


        // Instantiate the arrow indicator and set its position to match the player's position
        arrowIndicator = Instantiate(arrowIndicatorPrefab, playerController.transform.position, Quaternion.identity);


        if (tutorialLearned == false)
        {

            arrowIndicatorTextObject.SetActive(true);
            if (isSceneTutorial == true)
            {
                textManager.skipButton.SetActive(true);
                Time.timeScale = 0;
            }



            tutorialLearned = true;

        }

        yield return new WaitForSeconds(0.1f);
        tutorialLearned = true;



        if (tutorialLearned == true)
        {

            arrowIndicatorTextObject.SetActive(false);
            if (isSceneTutorial == true)
            {
                textManager.skipButton.SetActive(false);
                isTutoComplete = true;

                //StartCoroutine(Denemedeneme());
            }

        }

        // Attach the arrow indicator to the player (optional, you may want to adjust the arrow's position relative to the player)
        playerController.transform.position = playerController.transform.position;
        arrowIndicator.transform.parent = playerController.transform;

        yield return new WaitForSeconds(boxLifetime);
        //arrowIndicatorText.enabled = false;
        //arrowIndicatorTextObject.SetActive(false);
        Destroy(spawnedBox);
        Destroy(arrowIndicator);
        StartCoroutine(SpawnBox(interval, box));
    }

    private IEnumerator SpawnHealthBox(float interval, GameObject box)
    {

        yield return new WaitForSeconds(interval);
        //spawnedhealthBox = Instantiate(box, new Vector3(Random.Range(-50f, 50), 1, Random.Range(-50f, 50)), Quaternion.identity);
        spawnedhealthBox = Instantiate(box, new Vector3(Random.Range(-40f, 40), 1, Random.Range(-40f, 40)), Quaternion.identity);
        //arrowIndicatorText.enabled = true;


        // Instantiate the arrow indicator and set its position to match the player's position
        arrowHealthIndicator = Instantiate(arrowIndicatorPrefab, playerController.transform.position, Quaternion.identity);


        if (tutorial2Learned == false)
        {

            arrowIndicatorTextObject.SetActive(true);
            if (isSceneTutorial == true)
            {
                textManager.skipButton.SetActive(true);
                Time.timeScale = 0;
            }



            tutorial2Learned = true;


        }
        yield return new WaitForSeconds(0.1f);
        tutorial2Learned = true;

        if (tutorial2Learned == true)
        {

            arrowIndicatorTextObject.SetActive(false);
            if (isSceneTutorial == true)
            {
                textManager.skipButton.SetActive(false);
                isTutoComplete2 = true;
                StartCoroutine(Denemedeneme());
            }

        }



        // Attach the arrow indicator to the player (optional, you may want to adjust the arrow's position relative to the player)
        playerController.transform.position = playerController.transform.position;
        arrowHealthIndicator.transform.parent = playerController.transform;

        yield return new WaitForSeconds(healthBoxLifeTime);
        //arrowIndicatorText.enabled = false;
        //arrowIndicatorTextObject.SetActive(false);
        Destroy(spawnedhealthBox);
        Destroy(arrowHealthIndicator);
        StartCoroutine(SpawnHealthBox(interval, box));

    }

    private IEnumerator SpawnImmortalBox(float interval, GameObject box)
    {

        yield return new WaitForSeconds(interval);
        //spawnedhealthBox = Instantiate(box, new Vector3(Random.Range(-50f, 50), 1, Random.Range(-50f, 50)), Quaternion.identity);
        spawnedImmortalBox = Instantiate(box, new Vector3(Random.Range(-40f, 40), 1, Random.Range(-40f, 40)), Quaternion.identity);
        //arrowIndicatorText.enabled = true;


        // Instantiate the arrow indicator and set its position to match the player's position
        arrowImmortalIndicator = Instantiate(arrowIndicatorPrefab, playerController.transform.position, Quaternion.identity);

        //TUTORIAL PART
        if (tutorial3Learned == false)
        {

            arrowIndicatorTextObject.SetActive(true);
            if (isSceneTutorial == true)
            {
                textManager.skipButton.SetActive(true);
                Time.timeScale = 0;
            }



            tutorial2Learned = true;


        }
        yield return new WaitForSeconds(0.1f);
        tutorial3Learned = true;

        if (tutorial3Learned == true)
        {

            arrowIndicatorTextObject.SetActive(false);
            if (isSceneTutorial == true)
            {
                textManager.skipButton.SetActive(false);
                StartCoroutine(Denemedeneme());
            }

        }



        // Attach the arrow indicator to the player (optional, you may want to adjust the arrow's position relative to the player)
        playerController.transform.position = playerController.transform.position;
        arrowImmortalIndicator.transform.parent = playerController.transform;

        yield return new WaitForSeconds(immortalBoxLifetime);
        //arrowIndicatorText.enabled = false;
        //arrowIndicatorTextObject.SetActive(false);
        Destroy(spawnedImmortalBox);
        Destroy(arrowImmortalIndicator);
        StartCoroutine(SpawnImmortalBox(interval, box));

    }

    private IEnumerator SpawnSuperJumpBox(float interval, GameObject box)
    {

        yield return new WaitForSeconds(interval);
        //spawnedhealthBox = Instantiate(box, new Vector3(Random.Range(-50f, 50), 1, Random.Range(-50f, 50)), Quaternion.identity);
        spawnedSuperJumpBox = Instantiate(box, new Vector3(Random.Range(-40f, 40), 1, Random.Range(-40f, 40)), Quaternion.identity);
        //arrowIndicatorText.enabled = true;


        // Instantiate the arrow indicator and set its position to match the player's position
        arrowSuperJumpIndicator = Instantiate(arrowIndicatorPrefab, playerController.transform.position, Quaternion.identity);

        //TUTORIAL PART
        if (tutorial4Learned == false)
        {

            arrowIndicatorTextObject.SetActive(true);
            if (isSceneTutorial == true)
            {
                textManager.skipButton.SetActive(true);
                Time.timeScale = 0;
            }



            tutorial2Learned = true;


        }
        yield return new WaitForSeconds(0.1f);
        tutorial4Learned = true;

        if (tutorial4Learned == true)
        {

            arrowIndicatorTextObject.SetActive(false);
            if (isSceneTutorial == true)
            {
                textManager.skipButton.SetActive(false);
                StartCoroutine(Denemedeneme());
            }

        }



        // Attach the arrow indicator to the player (optional, you may want to adjust the arrow's position relative to the player)
        playerController.transform.position = playerController.transform.position;
        arrowSuperJumpIndicator.transform.parent = playerController.transform;

        yield return new WaitForSeconds(superJumpBoxLifetime);
        //arrowIndicatorText.enabled = false;
        //arrowIndicatorTextObject.SetActive(false);
        Destroy(spawnedSuperJumpBox);
        Destroy(arrowSuperJumpIndicator);
        StartCoroutine(SpawnSuperJumpBox(interval, box));

    }

    public IEnumerator ArrowSkillTextAppear()
    {
        yield return new WaitForSeconds(0.5f);

        if (tutorial5Learned == false)
        {

            //arrowIndicatorTextObject1.SetActive(true);
            if (isSceneTutorial == true)
            {
                textManager.skipButton.SetActive(true);
                Time.timeScale = 0;
            }


        }

        yield return new WaitForSeconds(0.1f);
        tutorial5Learned = true;

        if (tutorial5Learned == true)
        {
            //yield return new WaitForSeconds(0.5f);
            //arrowIndicatorTextObject1.SetActive(false);
            if (isSceneTutorial == true)
            {
                textManager.skipButton.SetActive(false);
                StartCoroutine(Denemedeneme());

            }

        }

        // tutorialLearned = true;
        // tutorial2Learned = true;
        // tutorial3Learned = true;
        // tutorial4Learned = true;
        // tutorial5Learned = true;

        // StartCoroutine(Denemedeneme());

    }

    public IEnumerator Denemedeneme()
    {
        yield return new WaitForSeconds(5f);
        //arrowIndicatorTextObject1.SetActive(false);

        if (isTutoComplete == true && isTutoComplete2 == true)
        {
            Debug.Log("TutoCompleted");

            //yield return new WaitForSeconds(10f);
            tutorialCompletedTextMessage.enabled = true;
            tutorialCompletedTextImage.enabled = true;
            tutorial2StartButton.SetActive(true);
            Time.timeScale = 0;
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(1);
        }



    }

    public IEnumerator KillAllSkillTexts()
    {
        yield return new WaitForSeconds(20f);
        arrowIndicatorTextObject.SetActive(false);
        //yield return new WaitForSeconds(2f);
        //arrowIndicatorTextObject1.SetActive(false);
    }

    public IEnumerator KillArrowIndicatorSkill()
    {
        yield return new WaitForSeconds(45f);
        //tutorialLearned = true;
        arrowIndicatorTextObject.SetActive(false);
        //arrowIndicatorTextObject1.SetActive(false);
        playerController.jumpButtonText.SetActive(false);
    }
}
