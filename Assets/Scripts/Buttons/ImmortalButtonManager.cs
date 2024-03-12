using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalButtonManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject immortalButton;
    private ParticleSystem immortalityParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        immortalButton = GameObject.FindGameObjectWithTag("ImmortalButton").transform.GetChild(2).gameObject;
        immortalityParticleSystem = GameObject.FindGameObjectWithTag("ImmortalityParticleSystem").GetComponent<ParticleSystem>();
        immortalityParticleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void immortalButtonActivate(){
        StartCoroutine(ImmortalButtonSkillActivate());
    }
    public IEnumerator ImmortalButtonSkillActivate(){
        playerController.isPlayerImmortal = true;

        immortalityParticleSystem.Play();

        immortalButton.SetActive(false);

        yield return new WaitForSeconds(5f);

        playerController.isPlayerImmortal = false;
        immortalityParticleSystem.Stop();
    }
}
