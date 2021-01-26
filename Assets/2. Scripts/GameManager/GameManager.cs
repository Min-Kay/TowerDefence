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
    public Wave currentWave;//웨이브 표시

    private void Awake()
    {
        if(instance == null)
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

<<<<<<< HEAD
    public void GameStart(Wave wave)
    {
        currentWave = wave;
=======
    public void GameStart()
    {
>>>>>>> 7c49708d9de75a40dfea46c0b874a4bbf732cb0f
        if (!isGameStart)
        {
            isGameStart = true;
            StartCoroutine(SpawnEnemy()); 
        } 
    }

    private IEnumerator SpawnEnemy()
    {
        int spawnEnemyCount = 0;
        while (!isGameOver && spawnEnemyCount < currentWave.maxEnemyCount)
        {
<<<<<<< HEAD
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>();
            
            enemy.Setup(this, wayPoints);
            spawnEnemyCount++;
            yield return new WaitForSeconds(currentWave.spawnTime);
            
=======
            Instantiate(enemyPrefab);

            yield return new WaitForSeconds(spawnTime);
>>>>>>> 7c49708d9de75a40dfea46c0b874a4bbf732cb0f
        }
    }
  
}
