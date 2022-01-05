using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public static int currentSpawned = 0;
    public static bool spawnCorrected = true;
    private Vector3[] spawnPoint = new Vector3[4];

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (EnemyMovement.spawnDifference)
            {
                spawnCorrected = false;

            } else if (currentSpawned < 5)
            {
                float randomSpawnX = Random.Range(-33.0f, 33.0f);
                float randomSpawnZ = Random.Range(-25.0f, 25.0f);
                int randomSpawn = Random.Range(0, 4);
                spawnPoint[0] = new Vector3(33, transform.position.y, randomSpawnZ);
                spawnPoint[1] = new Vector3(-33, transform.position.y, randomSpawnZ);
                spawnPoint[2] = new Vector3(randomSpawnX, transform.position.y, 25);
                spawnPoint[3] = new Vector3(randomSpawnX, transform.position.y, -25);
                Instantiate(enemyPrefab, spawnPoint[randomSpawn], transform.rotation);
                //Debug.Log(spawnPoint[randomSpawn].ToString());
                currentSpawned += 1;
            } else
            {
                currentSpawned = GameObject.FindGameObjectsWithTag("Enemy").Length;
                spawnCorrected = true;
            }
            yield return new WaitForSeconds(1);
        }
    }


}
