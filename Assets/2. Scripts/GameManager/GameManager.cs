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
    public int playerMoney;

    [Header("Enemy Spawn")]
    public GameObject enemyPrefab;
    //public float spawnTime;
    public Transform[] wayPoints;
    public Wave currentWave;

    [Header("UI Controll")]
    public HpChanger hpPanel;
    public MoneyChange moneyPanel;

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
        if (Player.getInstance().getHp() <= 0)
        {
            isGameOver = true;
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
