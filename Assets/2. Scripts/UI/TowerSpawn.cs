using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [Header("TowerSpawner Object")]
    public TowerSpawner towerSpawner;

    [Header("Pay Cost")]
    public MoneyChange moneyChanger;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    [HideInInspector]
    public GameObject currentTower = null;

    private UiCtrl ui;

    private void Awake()
    {
        enabled = false;
        mainCamera = Camera.main;
        ui = GetComponent<UiCtrl>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                int cost = currentTower.GetComponent<TowerCtrl>().price;

                if (hit.transform.CompareTag("Tile") && currentTower != null && cost<=Player.getInstance().getMoney())
                {
                    Player.getInstance().ChangeMoney(-cost);
                    moneyChanger.updateMoney();
                    towerSpawner.spawnTower(hit.transform, currentTower);
                    ui.enabled = true; 
                    enabled = false;
                }
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            ui.enabled = true;
            enabled = false;
        }
    }
   
}
