using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private GameManager enemySpawner;
    private int currentWaveIndex = -1;

    public void StartWave()
    {
        if(currentWaveIndex < waves.Length-1)
        {
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

