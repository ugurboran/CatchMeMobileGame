using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Unity.VisualScripting;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private CountDownController countDownController;
    public Transform player;
    NavMeshAgent nMeshAgent;

    [SerializeField] private float initialSpeed = 20.0f;
    [SerializeField] private float maxSpeed = 40.0f;
    [SerializeField] private float speedIncreaseRate = 0.2f;
    private int collisionCounter;
    private GameObject colliisionCounterTextObject;
    private TextMeshProUGUI collisionCounterText;

    public GameObject collisionParticle; // Particle System referansı
    
    // Start is called before the first frame update
    void Start()
    {
        nMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Capsule").transform;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        countDownController = GameObject.FindGameObjectWithTag("CountDownController").GetComponent<CountDownController>();
        //collisionParticle = GameObject.FindGameObjectWithTag("ExplosionParticle");
        
        nMeshAgent.speed = initialSpeed;
        if(countDownController.countDownOn){
            Destroy(gameObject);
        }
        else if(!countDownController.countDownOn){
            //initialSpeed = 15f;
            nMeshAgent.speed = initialSpeed;
        }

        colliisionCounterTextObject = GameObject.FindGameObjectWithTag("CollisionCounterText");
        collisionCounterText = colliisionCounterTextObject.GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        nMeshAgent.destination = player.position;
        
        // Hız artışı
        nMeshAgent.speed = speedIncreaseRate * Time.deltaTime;

        // Hız sınırlaması
        nMeshAgent.speed = Mathf.Clamp(nMeshAgent.speed, initialSpeed, maxSpeed);
        
    }

    void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        
        enemySpawner.collisionCounter++;
        
        //collisionCounterText.text = enemySpawner.collisionCounter.ToString();
        collisionCounterText.text = (enemySpawner.collisionCounter/2).ToString(); // 2 enemy scripti bu satırı çalıştırdığı için counter çift artmaması için bölü 2 uygulandı.
        //enemyCollisionAudioSource.Play();
        soundManager.EnemyCollisionSoundOn();
        // İki nesne çarpıştıysa bu kod bloğu çalışır.

        // Particle System'i oluştur
        GameObject effect = Instantiate(collisionParticle, collision.contacts[0].point, Quaternion.identity);
        
        //effect.Play();

        // Particle System'i yok et
        //Destroy(effect.gameObject, effect.main.duration);
        // Debug.Log("Obstacle ile çarpışma gerçekleşti.");
        Destroy(gameObject);
        
        // Çarpıştığınız nesneye göre yapmak istediğiniz işlemleri burada gerçekleştirebilirsiniz.
    }
}
}
