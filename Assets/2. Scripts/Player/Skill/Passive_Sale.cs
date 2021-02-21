using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passive_Sale : Skill
{
    [Header("Skill percent")]
    public float discountPercent;

    public Passive_Sale(string skillName, string skillInfo, Sprite skillImage, float skillCooltime):base(skillName, skillInfo, skillImage, skillCooltime) { }

    public override float PassiveSkill(float value)
    {
        return value * discountPercent;
    }
}
