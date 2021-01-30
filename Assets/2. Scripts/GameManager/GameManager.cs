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
    public float spawnTime;
    public Transform[] wayPoints;

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

    public void GameStart()
    {
        if (!isGameStart)
        {
            moneyPanel.updateMoney();
            UpdateHP();
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

    public void UpdateHP()
    {
        hpPanel.updateHp(Player.getInstance().getHp());
    }

    public void UpdateMoney()
    {
        moneyPanel.updateMoney();
    }



}
