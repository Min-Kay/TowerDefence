using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UnSelectedSkillButton : MonoBehaviour
{
    public string skillName;
    private GameObject selectedButton;
    private Skill skill;

    private void Start()
    {
        skill = SkillManager.getInstance().getSkill(skillName);
    }

    public void ChangeSelectedSkill()
    { 
        if (SkillManager.getInstance().getTargetButton().name == "SelectedSkill1")
        {
            selectedButton = GameObject.Find("/PlayerSkillCanvas/SelectedSkill1");
        }
        else
        {
            selectedButton = GameObject.Find("/PlayerSkillCanvas/SelectedSkill2");
        }
        if (skill != null)
        {
            selectedButton.GetComponent<SelectedSkillButton>().changeSkill(this.skill);
        }
    }
}
