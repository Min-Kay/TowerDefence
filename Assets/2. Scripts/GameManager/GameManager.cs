using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Game State")]
    public bool isGameOver = false;

    [Header("Player")]
    public int playerHp;

    [Header("Enemy Spawn")]
    public GameObject enemyPrefab;
    public float spawnTime;
    public Transform[] wayPoints;

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

        StartCoroutine(SpawnEnemy());
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (playerHp <= 0)
        {
            isGameOver = true;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (!isGameOver)
        {
            GameObject clone = Instantiate(enemyPrefab);
            Enemy enemy = clone.GetComponent<Enemy>();

            //enemy.Setup(wayPoints);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
