using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{

    public Wave[] waves;

    [SerializeField]
    private GameManager enemySpawner;
    private int currentWaveIndex = -1;

    public void StartWave()
    {
        if(currentWaveIndex < waves.Length-1 && enemySpawner.currentEnemyCount == 0 )
        {
            GameManager.instance.UpdateWave();
            currentWaveIndex++;
            enemySpawner.isGameStart = false;
            enemySpawner.GameStart(waves[currentWaveIndex]);
        }
        
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}

