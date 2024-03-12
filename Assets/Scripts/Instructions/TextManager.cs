using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    private GameObject instructionsTextObject1;
    private GameObject instructionsTextObject2;
    private GameObject instructionsTextObject3;
    private GameObject joystickTextObject;
    private bool tutorialLearned;
    private bool tutorial2Learned;
    private bool tutorial3Learned;
    public GameObject skipButton;
    public bool isSceneTutorial;
    //private GameObject instructionsTextObject;
    //private TextMeshProUGUI instructionsText;

    // Start is called before the first frame update
    void Start()
    {
        tutorialLearned = false;
        tutorial2Learned = false;
        tutorial3Learned = false;

        if(SceneManager.GetActiveScene().name == "Tutorial"){
            skipButton = GameObject.FindGameObjectWithTag("SkipButton");
            skipButton.SetActive(false);
            isSceneTutorial = true;
        }
        else{
            isSceneTutorial = false;
        }

        

        instructionsTextObject1 = GameObject.FindGameObjectWithTag("InstructionsText1");
        instructionsTextObject2 = GameObject.FindGameObjectWithTag("InstructionsText2");
        instructionsTextObject3 = GameObject.FindGameObjectWithTag("InstructionsText3");
        joystickTextObject = GameObject.FindGameObjectWithTag("JoystickTextObject");

        instructionsTextObject1.SetActive(false); 
        instructionsTextObject2.SetActive(false); 
        instructionsTextObject3.SetActive(false);
        joystickTextObject.SetActive(false);

        // instructionsTextObject = GameObject.FindGameObjectWithTag("InstructionsText");
        // instructionsText = instructionsTextObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(InstructionsTextClear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator InstructionsTextClear(){
        
        yield return new WaitForSeconds(1f);
        // instructionsTextObject1.SetActive(true);
        joystickTextObject.SetActive(true);
        // Time.timeScale = 0;

        if(tutorialLearned == false){
            instructionsTextObject1.SetActive(true);
            if(isSceneTutorial == true){
                skipButton.SetActive(true);
                Time.timeScale = 0;
            }
            
            
        }
        yield return new WaitForSeconds(0.5f);
        tutorialLearned = true;

        if(tutorialLearned == true){
            instructionsTextObject1.SetActive(false);
            if(isSceneTutorial == true){
                skipButton.SetActive(false);
            }
            
        }

        // yield return new WaitForSeconds(5f);
        // instructionsTextObject1.SetActive(false);
        

        yield return new WaitForSeconds(1f);
        instructionsTextObject2.SetActive(true);
        // Time.timeScale = 0;

        if(tutorial2Learned == false){
            if(isSceneTutorial == true){
                skipButton.SetActive(true);
                Time.timeScale = 0;
            }
            
        }
        yield return new WaitForSeconds(0.5f);
        tutorial2Learned = true;

        if(tutorialLearned == true){
            instructionsTextObject2.SetActive(false);
            if(isSceneTutorial == true){
                skipButton.SetActive(false);
            }
        }

        // yield return new WaitForSeconds(5f);
        // instructionsTextObject2.SetActive(false);

        yield return new WaitForSeconds(1f);
        instructionsTextObject3.SetActive(true);
        // Time.timeScale = 0;

        if(tutorial3Learned == false){
            if(isSceneTutorial == true){
                skipButton.SetActive(true);
                Time.timeScale = 0;
            }
            
        }
        yield return new WaitForSeconds(0.5f);
        tutorial3Learned = true;

        if(tutorial3Learned == true){
            instructionsTextObject3.SetActive(false);
            if(isSceneTutorial == true){
                skipButton.SetActive(false);
            }
        }

        // yield return new WaitForSeconds(5f);
        // instructionsTextObject3.SetActive(false);

        joystickTextObject.SetActive(false);
        //skipButton.SetActive(true);
    }
}
