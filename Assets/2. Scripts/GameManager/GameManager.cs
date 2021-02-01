using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Game State")]
    public bool isGameStart = false; 
    public bool isGameOver = false;
    public bool isGameClear = false; 

    [Header("Player")]
    public int playerHp;
    public int playerMoney;

    [Header("Enemy Spawn")]
    public GameObject enemyPrefab;
    //public float spawnTime;
    public Transform[] wayPoints;
    public Wave currentWave;

    [Header("UI Controll")]
    public HpChanger hpPanel;
    public MoneyChange moneyPanel;

    [Header("Wave")]
    public int MaxWaveCount;//최대 웨이브수
    public int WaveCount = 0;//몇 웨이브
    public int currentEnemyCount;//현재 남은적수

    private WaveSystem wavesystem;

    private TowerCtrl rayTargetTower = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }    
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
        wavesystem = GetComponent<WaveSystem>();
        MaxWaveCount = wavesystem.waves.Length;//최대웨이브 추가
        //DontDestroyOnLoad(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (Player.getInstance().getHp() <= 0)
        {
            isGameOver = true;
            Time.timeScale = 0;
        }
    }

    public void GameStart(Wave wave)
    {
        currentWave = wave;
        if (!isGameStart)
        {
            UpdateMoney();
            UpdateHP();
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

    public void UpdateHP()
    {
        hpPanel.updateHp(Player.getInstance().getHp());
    }

    public void UpdateMoney()
    {
        moneyPanel.updateMoney();
    }



}
