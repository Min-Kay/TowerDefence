using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill
{
    [Header("Skill Property")]
    public string skillName;
    public string skillInfo;
    public Image skillImage;
    public float skillCooltime;

    public Skill(string skillName, string skillInfo, Image skillImage, float skillCooltime)
    {
        this.skillName = skillName;
        this.skillInfo = skillInfo;
        this.skillImage = skillImage;
        this.skillCooltime = skillCooltime;
    }

    public void ChangeSkill(string skillName, string skillInfo, Image skillImage, float skillCooltime) {
        var currentSkillName = this.skillName;
        this.skillName = skillName;
        this.skillInfo = skillInfo;
        this.skillImage = skillImage;
        this.skillCooltime = skillCooltime;

        Player.getInstance().ChangeSkill(this, currentSkillName);
    } 
    
    protected virtual void ActiveSkill() { }
}
