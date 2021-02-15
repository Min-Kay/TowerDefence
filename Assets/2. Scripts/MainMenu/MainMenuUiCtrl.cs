using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUiCtrl : MonoBehaviour
{
    
    public static bool isHardmode = false;

    [Header("Canvas")]
    public Canvas StageUi;

    [Header("Stage")]
    public Text Stage;
    public Image emptystar1;
    public Image emptystar2;
    public Image emptystar3;
    public Image fullstar1;
    public Image fullstar2;
    public Image fullstar3;
    
    
    public void NormalStart()
    {
        SceneManager.LoadScene("TowerDefence");
    }

    public void HardStart()
    {
        //Àû ÇÇ 1.5¹è
        isHardmode = true;
        SceneManager.LoadScene("TowerDefence");
    }
}
