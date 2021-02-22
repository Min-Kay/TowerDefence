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
    public WaveChanger wavePanel;

    [Header("Wave")]
    public int MaxWaveCount;//최대 웨이브수
    public int WaveCount = 0;//몇 웨이브
    public int currentEnemyCount;//현재 남은적수

    private WaveSystem wavesystem;

    private TowerCtrl rayTargetTower = null;
    private Enemy rayTargetEnemy = null;
    private MainMenuUiCtrl mainmenuui;

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
        mainmenuui = GetComponent<MainMenuUiCtrl>();
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
        else if ((MaxWaveCount == WaveCount) && currentEnemyCount == 0)
        {
            isGameClear = true;
            if (Stagemode.instance.choosenumber == 1)
            {
                Stagemode.instance.clearmap1= true;
            }
            else if (Stagemode.instance.choosenumber == 2)
            {
                Stagemode.instance.clearmap2 = true;
            }
            else if (Stagemode.instance.choosenumber == 3)
            {
                Stagemode.instance.clearmap3 = true;
            }
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
        int enemyIndex = 0;
        int spawnEnemyCount = 0;
        int halfcount = currentWave.maxEnemyCount / currentWave.enemyPrefabs.Length;
        int halfcountplus = halfcount;//prefab종류가 다양해질때 더해질숫자
        while (!isGameOver && spawnEnemyCount < currentWave.maxEnemyCount)
        {
            if (currentWave.enemyPrefabs.Length > 1)
            {

                if (spawnEnemyCount <= halfcount)
                {

                    if (spawnEnemyCount == halfcount && halfcount != currentWave.maxEnemyCount)
                    {
                        enemyIndex++;
                        halfcount = halfcount + halfcountplus;

                    }

                }
            }
            else
            {
                enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);//랜덤으로하는 출력
                //하나라서 어차피 1개만 나옴 위식안해도 초깃값 0
            }
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

    public Enemy targetEnemy
    {
        get
        {
            return rayTargetEnemy;
        }
        set
        {
            rayTargetEnemy = value;
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

    public void UpdateWave()
    {
        wavePanel.updateWave(MaxWaveCount, WaveCount);
    }

}
