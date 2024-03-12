using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    private bool isPaused = false;

    private Button pauseButton; // UI düğmesi
    private Button skipButton; // UI düğmesi
    private Image playImage;

    private Image pauseImage;
    public TextManager textManager;

    void Start()
    {


        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            // skipButton = GameObject.FindGameObjectWithTag("SkipButton").GetComponent<Button>();
            skipButton = textManager.skipButton.GetComponent<Button>();
            skipButton.onClick.AddListener(Continue);
        }
        else
        {
            pauseButton = GameObject.FindGameObjectWithTag("PauseButton").GetComponent<Button>();
            pauseImage = pauseButton.transform.GetChild(0).GetComponent<Image>();
            playImage = pauseButton.transform.GetChild(1).GetComponent<Image>();
            pauseImage.enabled = true;
            playImage.enabled = false;
            pauseButton.onClick.AddListener(TogglePause);
        }



    }

    void TogglePause()
    {
        if (isPaused)
        {
            pauseImage.enabled = true;
            playImage.enabled = false;
            ResumeGame();
        }
        else
        {
            pauseImage.enabled = false;
            playImage.enabled = true;
            PauseGame();
        }
    }

    void Continue()
    {
        ResumeGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}
