using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedSkillButton : MonoBehaviour
{
    private Skill skill;
    /*
     * When button clicked display skill choose panel
     */
    public void OnButtonClicked()
    {

    }
    public void changeSkill(Skill skill)
    {
        this.skill = skill;
        skill.ChangeSkill(skill.skillName, skill.skillInfo, skill.skillImage, skill.skillCooltime);
    }
}
