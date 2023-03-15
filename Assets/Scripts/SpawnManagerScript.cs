using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    public int enemyCount;
    public int waveNumber = 1;
    public GameObject powerUpPrefab;

    private float spawnRange = 9;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        }

    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        int enemySpawnKind;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            if (waveNumber<4)
            {
                enemySpawnKind = 0;
                
            }
            else
            {
                enemySpawnKind = Random.Range(0, enemyPrefab.Length);
            }
            Instantiate(enemyPrefab[enemySpawnKind], GenerateSpawnPosition(), enemyPrefab[enemySpawnKind].transform.rotation);
        }
    }

}
