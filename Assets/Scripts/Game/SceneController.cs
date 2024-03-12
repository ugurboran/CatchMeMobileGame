using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene(){
        SceneManager.LoadScene(1);
    }

    public void LoadGameScene(){
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void LoadMenuScene(){
        SceneManager.LoadScene(0);
    }

    public void LoadTutorialScene(){
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }
    public void LoadShopScene(){
        SceneManager.LoadScene(4);
    }

    public void LoadLeaderboardScene(){
        SceneManager.LoadScene(5);
    }

    public void LoadGameOverScene(){
        SceneManager.LoadScene(2);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
