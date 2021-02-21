using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectedSkillButton : MonoBehaviour
{
    public GameObject skillPanel;
    private Skill skill;
    private GameObject selectedButton;

    /*
     * When button clicked display skill choose panel
     */
    public void OnButtonClicked()
    {
        SkillManager.getInstance().setTargetButton(this.gameObject);
        skillPanel.gameObject.SetActive(true);
    }
    public void changeSkill(Skill skill)
    {
        GetComponentInChildren<Text>().text = skill.skillName;
        this.skill = skill;
        skill.ChangeSkill(skill.skillName, skill.skillInfo, skill.skillImage, skill.skillCooltime);
    }

   
}
