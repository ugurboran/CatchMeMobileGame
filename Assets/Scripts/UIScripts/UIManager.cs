using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame(){
        pausePanel.SetActive(true);
        Time.timeScale = 0;

    }

    public void ContinueGame(){
        pausePanel.SetActive(false);
        Time.timeScale = 1;

    }
}
