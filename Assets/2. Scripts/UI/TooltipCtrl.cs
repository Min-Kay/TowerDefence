using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipCtrl : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("ToolTip Field")]
    public GameObject tooltip;
    public Text name;
    public Text info;

    private TowerBaseCtrl tower;

    private void Start()
    {
        tooltip.SetActive(false);
    }

    public void ShowTooltip()
    {
        tooltip.SetActive(true);
        tooltip.transform.position = new Vector3(transform.position.x, transform.position.y - 10.0f, transform.position.z);

        if (transform.name == "Skill 1")
        {
            name.text = tower.skill1Name.ToString();
            info.text = tower.skill1Tooltip.ToString();
        }
        else if (transform.name == "Skill 2")
        {
            name.text = tower.skill2Name.ToString();
            info.text = tower.skill2Tooltip.ToString();
        }
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tower = GameManager.instance.targetTower;
        if(tower != null)
        {
            ShowTooltip();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }
}
