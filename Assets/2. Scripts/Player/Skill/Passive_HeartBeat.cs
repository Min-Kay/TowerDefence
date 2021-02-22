using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive_HeartBeat : Skill
{
    public Passive_HeartBeat(string skillName, string skillInfo, Sprite skillImage, float skillCooltime) : base(skillName, skillInfo, skillImage, skillCooltime) { }


    public override float PassiveSkill(float value)
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.playerHp += 10;
        }

        return base.PassiveSkill(value);
    }
}
