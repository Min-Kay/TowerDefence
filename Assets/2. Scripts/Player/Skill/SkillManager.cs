using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager {

    private GameObject targetButton;
    private Dictionary<string, Skill> skillMap;
    private SkillManager() { initSkillMap(); }

    private static class SkillManagerHolder
    {
        public static SkillManager instance = new SkillManager();
    } 
   
    public static SkillManager getInstance()
    {
        return SkillManagerHolder.instance;
    }

    private void initSkillMap()
    {
        skillMap = new Dictionary<string, Skill>();
        skillMap.Add("Passive_Sale", new Passive_Sale("sale", "disocunt tower cost", Resources.Load<Sprite>("money-bag"), 0));
        skillMap.Add("Passive_HeartBeat", new Passive_HeartBeat("heart", "When wave start, increase player hp", Resources.Load<Sprite>("health"), 0));
    }

    public GameObject getTargetButton()
    {
        return targetButton;
    }

    public void setTargetButton(GameObject targetButton)
    {
        this.targetButton = targetButton;
    }

    public Skill getSkill(string key)
    {
        return skillMap[key];
    }
}
