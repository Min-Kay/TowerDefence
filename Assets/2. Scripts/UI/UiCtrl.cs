using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCtrl : MonoBehaviour
{
    [Header("Sound")]
    public AudioListener audioListener;
    public Image soundButton;
    public Sprite soundOn;
    public Sprite soundOff;
    private bool isSoundOff = false;


    [Header("Tower Canvas")]
    public Canvas towerUi;
    public Canvas towerMenu;
    public Canvas option;

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
    public Text mode;

    void Start()
    {
        ShowTowerMenu();
    }

    private void Update()
    {
        if (isUiActive)
        {
            SetTowerStatus();
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            ShowTowerMenu();
        }
    }

    public void ShowTowerUi()
    {
        towerMenu.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
        towerUi.gameObject.SetActive(true);
        isUiActive = true;
    }

    public void ShowTowerMenu()
    {
        towerUi.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
        towerMenu.gameObject.SetActive(true);
        isUiActive = false;
    }

    public void ShowOption()
    {
        towerUi.gameObject.SetActive(false);
        towerMenu.gameObject.SetActive(false);
        option.gameObject.SetActive(true);
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
        attackPower.text = "Power : " + tower.GetComponent<TowerCtrl>().power;
        killCount.text = "Kill Count : " + tower.killCount;
        mode.text = tower.mode.ToString();
    }

    public void ChangeTowerMode()
    {
        switch (tower.mode)
        {
            case TowerCtrl.AttackMode.FirstTarget:
                tower.mode = TowerCtrl.AttackMode.StrongestTarget;
                mode.text = tower.mode.ToString();
                break;
            case TowerCtrl.AttackMode.StrongestTarget:
                tower.mode = TowerCtrl.AttackMode.FirstTarget;
                mode.text = tower.mode.ToString();
                break;
            default:
                break;
        }
    }

    public void UpgradeButton()
    {
        if (tower.upgradeCost <= Player.getInstance().getMoney() && tower.upgradePhase < tower.maxUpgrade)
        {
            Player.getInstance().ChangeMoney(-tower.upgradeCost);
            GameManager.instance.UpdateMoney();
            tower.UpgradeTower();
        }
    }

    public void ResellButton()
    {
        Player.getInstance().ChangeMoney(tower.price / 2);
        GameManager.instance.UpdateMoney();
        ShowTowerMenu();
        Destroy(tower.gameObject);
    }

    public void SoundButton()
    {
        if(isSoundOff)
        {
            isSoundOff = false;
            soundButton.sprite = soundOn;
            audioListener.enabled = true;
        }
        else if(!isSoundOff)
        {
            isSoundOff = true;
            soundButton.sprite = soundOff;
            audioListener.enabled = false;
        }
    }

    public void RestartButton()
    {

    }

    public void ExitButton()
    {

    }
}
