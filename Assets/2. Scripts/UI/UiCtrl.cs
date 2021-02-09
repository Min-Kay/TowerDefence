using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Canvas gameOver;
    public Canvas EnemyUi;

    [Header("Win Or Lose")]
    public GameObject win;
    public GameObject lose;

    private TowerCtrl tower;
    private bool isUiActive;
    public bool isEnemyUiActive =false;

    [Header("Tower UI")]
    public Text towerName;
    public Image image;
    public Text resellCost;
    public Text upgradeCost;
    public Text upgrade;
    public Text attackPower;
    public Text killCount;
    public Text mode;
    public Image skill1 = null;
    public Image skill2 = null;

    private Enemy enemy;
    [Header("Enemy UI")]
    public Text EnemyName;
    public Image Enemyimage;
    public Text EnemyHp;
    public Text EnemyattackPower;
    public Text EnemyGold;

    private int gameSpeed = 1;

    void Start()
    {
        gameOver.gameObject.SetActive(false);
        ShowTowerMenu();
    }

    private void Update()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            if (isUiActive)
            {
                SetTowerStatus();
                //Debug.Log("타워UI띄우기");
            }

            if(isEnemyUiActive)
            {
                SetEnemyStatus();
                //Debug.Log("적UI띄우기");
            }

            if (Input.GetMouseButtonDown(1))
            {
                ShowTowerMenu();
                //Debug.Log("타워메뉴띄우기");
            }
        }
        else
        {
            ShowGameOver();
        }
    }

    public void ShowTowerUi()
    {
        towerMenu.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
        EnemyUi.gameObject.SetActive(false);
        towerUi.gameObject.SetActive(true);
        isUiActive = true;
        isEnemyUiActive = false;
    }

    public void ShowEnemyUi()
    {
        towerMenu.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
        EnemyUi.gameObject.SetActive(true);
        towerUi.gameObject.SetActive(false);
        isEnemyUiActive = true;
        isUiActive = false;
    }
    public void ShowTowerMenu()
    {
        Time.timeScale = 1;
        towerUi.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
        EnemyUi.gameObject.SetActive(false);
        towerMenu.gameObject.SetActive(true);
        isUiActive = false;
        isEnemyUiActive = false;
    }

    public void ShowOption()
    {
        Time.timeScale = 0;
        towerUi.gameObject.SetActive(false);
        towerMenu.gameObject.SetActive(false);
        EnemyUi.gameObject.SetActive(false);
        option.gameObject.SetActive(true);
        isUiActive = false;
    }

    public void ShowGameOver()
    {
        gameOver.gameObject.SetActive(true);
        towerUi.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
        EnemyUi.gameObject.SetActive(false);
        towerMenu.gameObject.SetActive(false);

        if (GameManager.instance.isGameOver)
        {
            win.SetActive(false); 
        }
        else if(GameManager.instance.isGameClear)
        {
            lose.SetActive(false);
        }

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
        skill1.sprite = tower.skill1Sprite;
        skill2.sprite = tower.skill2Sprite;
    }

    public void SetEnemyStatus()
    {
        enemy = GameManager.instance.targetEnemy;
        if(enemy.HP ==0)
        {
            isEnemyUiActive = false;
            ShowTowerMenu();
        }
        else
        {
            EnemyName.text = enemy.name;
            Enemyimage.sprite = enemy.GetComponent<SpriteRenderer>().sprite;
            EnemyHp.text = "HP : " + enemy.HP;
            EnemyattackPower.text = "Damage : " + enemy.attackPower;
            EnemyGold.text = "Gold : " + enemy.gold;
        }
        
    }

    public void ChangeTowerMode()
    {
        switch (tower.mode)
        {
            case TowerBaseCtrl.AttackMode.FirstTarget:
                tower.mode = TowerBaseCtrl.AttackMode.StrongestTarget;
                mode.text = tower.mode.ToString();
                break;
            case TowerBaseCtrl.AttackMode.StrongestTarget:
                tower.mode = TowerBaseCtrl.AttackMode.FirstTarget;
                mode.text = tower.mode.ToString();
                break;
            default:
                break;
        }
    }

    public void UpgradeButton()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            if (tower.upgradeCost <= Player.getInstance().getMoney() && tower.upgradePhase < tower.maxUpgrade)
            {
                Player.getInstance().ChangeMoney(-tower.upgradeCost);
                GameManager.instance.UpdateMoney();
                tower.UpgradeTower();
            }
        }
    }

    public void ResellButton()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            Player.getInstance().ChangeMoney(tower.price / 2);
            GameManager.instance.UpdateMoney();
            ShowTowerMenu();
            Destroy(tower.gameObject);
        }
    }

    public void SoundButton()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            if (isSoundOff)
            {
                isSoundOff = false;
                soundButton.sprite = soundOn;
                audioListener.enabled = true;
            }
            else if (!isSoundOff)
            {
                isSoundOff = true;
                soundButton.sprite = soundOff;
                audioListener.enabled = false;
            }
        }
    }

    public void GameSpeedButton()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            switch (gameSpeed)
            {
                case 1:
                    Time.timeScale = ++gameSpeed;
                    break;
                case 2:
                    Time.timeScale = --gameSpeed;
                    break;
            }
        }
    }

    public void ExitButton()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
               UnityEngine.Application.Quit();
        #endif
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("TowerDefence");
    }

    /*public void isEnemyActive(bool change)
    {
        isEnemyUiActive = change;
    }*/
}

