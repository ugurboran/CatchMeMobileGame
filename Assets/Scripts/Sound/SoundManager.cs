using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private AudioSource policeSirenAudioSource;
    //[SerializeField]private AudioSource gameOverAudioSource;
    [SerializeField] private AudioSource enemyCollisionAudioSource;
    [SerializeField] private AudioSource characterWuhuAudioSource;
    [SerializeField] private AudioSource buttonClickAudioSource;
    [SerializeField] private AudioSource passSoundAudioSource;
    [SerializeField] private AudioSource moneySoundAudioSource;
    [SerializeField] private AudioSource noMoneySoundAudioSource;
    [SerializeField] private AudioSource scoreSoundAudioSource;
    [SerializeField] private AudioSource gameOverSoundAudioSource;
    [SerializeField] private AudioSource skillTakeSoundAudioSource;
    [SerializeField] private AudioSource healSkillTakeSoundAudioSource;
    [SerializeField] private AudioSource skillButtonSoundAudioSource;
    [SerializeField] private AudioSource countDownSoundAudioSource;
    [SerializeField] private AudioSource countDownGoSoundAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "GameScene" || SceneManager.GetActiveScene().name == "Tutorial"){
            policeSirenAudioSource = GetComponent<AudioSource>();
            StartCoroutine(PoliceSirenAudioPlay());
            //gameOverAudioSource.Play();
        }
        else if(SceneManager.GetActiveScene().name == "GameOverScene"){
            gameOverSoundAudioSource.Play();
        }
        else if(SceneManager.GetActiveScene().name == "LeaderBoardScene"){
            scoreSoundAudioSource.Play();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyCollisionSoundOn(){
        enemyCollisionAudioSource.Play();
    }

    public void CharacterWuhuSoundOn(){
        characterWuhuAudioSource.Play();
    }

    public void ButtonClickSoundOn(){
        buttonClickAudioSource.Play();
    }

    public void PassSoundOn(){
        passSoundAudioSource.Play();
    }

    public void MoneySoundOn(){
        moneySoundAudioSource.Play();
    }

    public void NoMoneySoundOn(){
        noMoneySoundAudioSource.Play();
    }

    public void SkillTakeSoundOn(){
        skillTakeSoundAudioSource.Play();
    }

    public void HealSkillTakeSoundOn(){
        healSkillTakeSoundAudioSource.Play();
    }

    public void SkillButtonSoundOn(){
        skillButtonSoundAudioSource.Play();
    }

    public void CountdownSoundOn(){
        countDownSoundAudioSource.Play();
    }

    public void CountdownGoSoundOn(){
        countDownGoSoundAudioSource.Play();
    }

    public IEnumerator PoliceSirenAudioPlay(){

        yield return new WaitForSeconds(10f);
        
        policeSirenAudioSource.Play();

        yield return new WaitForSeconds(10f);

        policeSirenAudioSource.Pause();
        
        StartCoroutine(PoliceSirenAudioPlay());
        
    }

}
