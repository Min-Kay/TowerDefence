using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UnSelectedSkillButton : MonoBehaviour
{
    [Header("Skill property")]
    public string skillName;
    public string skillInfo;
    public Image skillImage;
    public float skillCooltime;

    private GameObject selectedButton;
    private Skill skill;

    private void Start()
    {
        if(EventSystem.current.currentSelectedGameObject.name == "SelectedSkill1")
        {
            selectedButton = GameObject.Find("/PlayerSkillCanvas/SelectedSkill1");
        }
        else
        {
            selectedButton = GameObject.Find("/PlayerSkillCanvas/SelectedSkill2");
        }

        skill = new ExampleSkill1(skillName, skillInfo, skillImage, skillCooltime);
    }

    public void ChangeSelectedSkill()
    {
        selectedButton.GetComponent<SelectedSkillButton>().changeSkill(this.skill);
    }
}
