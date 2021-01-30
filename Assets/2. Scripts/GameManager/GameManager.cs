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
    public GameObject enemyPrefab;
    public float spawnTime;
    public Transform[] wayPoints;

    private TowerCtrl rayTargetTower = null;

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

    public void GameStart()
    {
        if (!isGameStart)
        {
            isGameStart = true;
            StartCoroutine(SpawnEnemy()); 
        } 
    }

    private IEnumerator SpawnEnemy()
    {
        while (!isGameOver)
        {
            Instantiate(enemyPrefab);

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public TowerCtrl targetTower
    { 
        get
        {
            return rayTargetTower;
        }
        set
        {
            rayTargetTower = value;
        }
    }

  
}
