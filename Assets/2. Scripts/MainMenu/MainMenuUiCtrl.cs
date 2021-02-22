using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUiCtrl : MonoBehaviour
{

    

    [Header("Canvas")]
    public Canvas StageUi;

    [Header("Stage")]
    public Text Stage;
    public Image image;
    public Image emptystar1;
    public Image emptystar2;
    public Image emptystar3;
    public Image fullstar1;
    public Image fullstar2;
    public Image fullstar3;

    public Image map1;
    public Image map2;
    public Image map3;

    

    private Stage stage;
    private Stage rayTargetStage = null;
    private static int stagenumber;
    public bool isStageUiActive;
    //public Stagemode stagemode;
    
    void Awake()
    {
        
        
        StageUi.gameObject.SetActive(false);
       
    }

    private void Update()
    {
        if(isStageUiActive)
        {
            SetStageStatus();
        }
        if (Input.GetMouseButtonDown(1))
        {
            StageUi.gameObject.SetActive(false);
        }
    }
    public void NormalStart()
    {
        isStageUiActive = false;
        Stagemode.instance.hardmode(false);
        
        if (stagenumber ==1)
        {
            
            SceneManager.LoadScene("TowerDefence");
        }
        else if (stagenumber == 2)
        {
            SceneManager.LoadScene("TowerDefence2");
        }
        else if (stagenumber == 3)
        {
            SceneManager.LoadScene("TowerDefence3");
        }

    }

    public void HardStart()
    {
        isStageUiActive = false;
        Stagemode.instance.hardmode(true);

        //피 1.5배 해줘야됨 다음씬으로 넘겨줘야됨 hardmode여부
        if (stagenumber == 1)
        {
            SceneManager.LoadScene("TowerDefence");
        }
        else if (stagenumber == 2)
        {
            SceneManager.LoadScene("TowerDefence2");
        }
        else if (stagenumber == 3)
        {
            SceneManager.LoadScene("TowerDefence3");
        }
    }

    public void ShowStageUi()
    {
        StageUi.gameObject.SetActive(true);
        isStageUiActive = true;
        
    }

    public void SetStageStatus()
    {
        //Debug.Log("setstage");
        stage = targetStage;
        Stage.text = stage.Stagename;
        stagenumber = stage.stagenumber;
        Stagemode.instance.choosenumber = stagenumber;
        //Debug.Log("클리어검사중" + stagenumber + Stagemode.instance.Mapclear(stagenumber));
        if (Stagemode.instance.Mapclear(stagenumber) == 1)//게임클리어후
        {
            fullstar1.gameObject.SetActive(true);
            fullstar2.gameObject.SetActive(false);
            fullstar3.gameObject.SetActive(false);
            emptystar1.gameObject.SetActive(false);
            emptystar2.gameObject.SetActive(true);
            emptystar3.gameObject.SetActive(true);
        }
        else if (Stagemode.instance.Mapclear(stagenumber) == 2)
        {
            fullstar1.gameObject.SetActive(true);
            fullstar2.gameObject.SetActive(true);
            fullstar3.gameObject.SetActive(false);
            emptystar1.gameObject.SetActive(false);
            emptystar2.gameObject.SetActive(false);
            emptystar3.gameObject.SetActive(true);
        }
        else if (Stagemode.instance.Mapclear(stagenumber) == 3)
        {
            fullstar1.gameObject.SetActive(true);
            fullstar2.gameObject.SetActive(true);
            fullstar3.gameObject.SetActive(true);
            emptystar1.gameObject.SetActive(false);
            emptystar2.gameObject.SetActive(false);
            emptystar3.gameObject.SetActive(false);
        }
        else
        {
            fullstar1.gameObject.SetActive(false);
            fullstar2.gameObject.SetActive(false);
            fullstar3.gameObject.SetActive(false);
            emptystar1.gameObject.SetActive(true);
            emptystar2.gameObject.SetActive(true);
            emptystar3.gameObject.SetActive(true);
        }

        if (Stage.text.CompareTo("1-1")== 0)
        {
            image.sprite = map1.sprite;
            
        }
        else if(Stage.text.CompareTo("1-2") == 0)
        {
            image.sprite = map2.sprite;
            
        }
        else if (Stage.text.CompareTo("1-3") == 0)
        {
            image.sprite = map3.sprite;
        }

    }

    public Stage targetStage
    {
        get
        {
            return rayTargetStage;
        }
        set
        {
            rayTargetStage = value;
        }
    }

    

    public static int getStagenumber()
    {
        return stagenumber;
    }

    
}
