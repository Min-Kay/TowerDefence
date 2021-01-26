using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelect : MonoBehaviour
{
    [Header("Tower Spawn Object")]
    public GameObject towerSpawn;

    [Header("Tower PreFab")]
    public GameObject tower;

    public void TowerButtonClick()
    {
        towerSpawn.GetComponent<TowerSpawn>().currentTower = tower;
    }
}
