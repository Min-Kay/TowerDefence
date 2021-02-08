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

    [Header("Win Or Lose")]
    public GameObject win;
    public GameObject lose;

    private TowerBaseCtrl tower;
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
    public Image skill1;
    public Image cooldown1;
    public Image skill2;
    public Image cooldown2;

    private int gameSpeed = 1;

    void Awake()
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
            }

            if (Input.GetMouseButtonDown(1))
            {
                ShowTowerMenu();
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
        towerUi.gameObject.SetActive(true);
        isUiActive = true;
    }

    public void ShowTowerMenu()
    {
        Time.timeScale = 1;
        towerUi.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
        towerMenu.gameObject.SetActive(true);
        isUiActive = false;
    }

    public void ShowOption()
    {
        Time.timeScale = 0;
        towerUi.gameObject.SetActive(false);
        towerMenu.gameObject.SetActive(false);
        option.gameObject.SetActive(true);
        isUiActive = false;
    }

    public void ShowGameOver()
    {
        gameOver.gameObject.SetActive(true);
        towerUi.gameObject.SetActive(false);
        option.gameObject.SetActive(false);
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
        cooldown1.sprite = tower.skill1Sprite;
        cooldown2.sprite = tower.skill2Sprite;
        InitCooldown(cooldown1);
        InitCooldown(cooldown2);
        cooldown1.fillAmount = tower.GetCooltime(1);
        cooldown2.fillAmount = tower.GetCooltime(2);
    }

    void InitCooldown(Image skill)
    {
        skill.color = new Color(0, 0, 0, 0.6f);
        skill.type = Image.Type.Filled;
        skill.fillMethod = Image.FillMethod.Radial360;
        skill.fillOrigin = (int)Image.Origin360.Top;
        skill.fillClockwise = false;
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
            GameManager.instance.targetTower = null;
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
}

