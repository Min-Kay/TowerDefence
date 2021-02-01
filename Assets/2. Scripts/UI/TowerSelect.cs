using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelect : MonoBehaviour
{
    [Header("Tower Spawn Object")]
    public GameObject towerSpawn;

    [Header("Tower PreFab")]
    public GameObject tower;

    private Text cost;

    private void Start()
    {
        cost = GetComponentInChildren<Text>();
        cost.text = tower.GetComponent<TowerCtrl>().price.ToString();
    }

    public void TowerButtonClick()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            towerSpawn.GetComponent<TowerSpawn>().currentTower = tower;
        }
    }
}
