using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Game State")]
    public bool isGameStart = false; 
    public bool isGameOver = false;

    [Header("Player")]
    public int playerHp;

    [Header("Enemy Spawn")]
    //public GameObject enemyPrefab;
    //public float spawnTime;
    public Transform[] wayPoints;

    [Header("Wave")]
    public int MaxWaveCount;//최대 웨이브수
    public int WaveCount=0;//몇 웨이브
    public int currentEnemyCount;//현재 남은적수
    public Wave currentWave;//웨이브 표시

    private WaveSystem wavesystem;
    
    private void Awake()
    {
        wavesystem = GetComponent<WaveSystem>();
        MaxWaveCount=wavesystem.waves.Length;//최대웨이스추가
        if (instance == null)
        {
            instance = this;
        }    
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (playerHp <= 0)
        {
            isGameOver = true;
        }
    }

    public void GameStart(Wave wave)
    {
        currentWave = wave;
        if (!isGameStart)
        {
            isGameStart = true;
            WaveCount++;
            StartCoroutine(SpawnEnemy()); 
        } 
    }

    private IEnumerator SpawnEnemy()
    {
        int spawnEnemyCount = 0;
        while (!isGameOver && spawnEnemyCount < currentWave.maxEnemyCount)
        {
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>();
            
            enemy.Setup(this, wayPoints);
            spawnEnemyCount++;
            currentEnemyCount++;//적 생성시 count추가
            yield return new WaitForSeconds(currentWave.spawnTime);
            
        }
    }
  
}
