// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     [SerializeField] private GameObject enemyPrefab;
//     public int collisionCounter;

//     private float enemyInterval = 0.5f;

//     // Start is called before the first frame update
//     void Start()
//     {
//         StartCoroutine(SpawnEnemy(enemyInterval, enemyPrefab));
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     private IEnumerator SpawnEnemy(float interval, GameObject enemy)
//     {
//         yield return new WaitForSeconds(interval);
//         GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-50f, 50), 0, Random.Range(-50f, 50)), Quaternion.identity);
//         StartCoroutine(SpawnEnemy(interval, enemy));
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public int collisionCounter;

    private float enemyInterval = 0.5f;
    private Transform playerTransform; // Reference to the player's transform
    private float minDistanceToPlayer = 10f; // Minimum distance from player

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has a tag "Player"
        StartCoroutine(SpawnEnemy(enemyInterval, enemyPrefab));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        
        // Calculate a position that is not too close to the player
        Vector3 spawnPosition = GetRandomSpawnPosition();
        
        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition;
        do
        {
            randomPosition = new Vector3(Random.Range(-50f, 50), 0, Random.Range(-50f, 50));
        } while (Vector3.Distance(randomPosition, playerTransform.position) < minDistanceToPlayer);
        
        return randomPosition;
    }
}

