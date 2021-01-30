using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCtrl : MonoBehaviour
{

    [Header("Tower Canvas")]
    public Canvas towerUi;
    public Canvas towerMenu;

    private TowerCtrl tower;
    private bool isUiActive;

    [Header("Tower UI")]
    public Text towerName;
    public Image image;
    public Text resellCost;
    public Text upgradeCost;
    public Text upgrade;
    public Text attackPower;
    public Text killCount;

    void Start()
    {
        ShowTowerMenu();
    }

    private void Update()
    {
        if(isUiActive)
        {
            SetTowerStatus();
        }
    }

    public void ShowTowerUi()
    {
        towerMenu.gameObject.SetActive(false);
        towerUi.gameObject.SetActive(true);
        isUiActive = true;
    }

    public void ShowTowerMenu()
    {
        towerUi.gameObject.SetActive(false);
        towerMenu.gameObject.SetActive(true);
        isUiActive = false;
    }

    public void SetTowerStatus()
    {
        tower = GameManager.instance.targetTower;

        towerName.text = tower.towerName;
        image.sprite = tower.GetComponent<SpriteRenderer>().sprite;
        resellCost.text = (tower.price / 2).ToString();
        upgradeCost.text = tower.upgradeCost.ToString();
        upgrade.text = "Upgrade : " + tower.upgradePhase;
        attackPower.text = "Power : " + tower.attackPrefab.GetComponent<AttackObject>().power;
        killCount.text = "Kill Count : " + tower.killCount;
    }
}
