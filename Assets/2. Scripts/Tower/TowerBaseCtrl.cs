using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseCtrl : MonoBehaviour
{
    [Header("Tower Information")]
    public string towerName;
    public int price;
    public int upgradePhase = 1;
    public int maxUpgrade;
    public int upgradeCost;
    public int killCount = 0;

    [Header("Attack Info")]
    public GameObject attackPrefab;
    public float attackDelay;
    public AttackMode mode;
    public float power;
    public float speed;
    public float distance;

    //Enemy List
    protected GameObject[] enemys = null;
    protected GameObject target;

    [Header("Skill 1 Info")]
    public string skill1Name;
    public Sprite skill1Sprite;
    public float skill1Delay;
    [TextArea]
    public string skill1Tooltip;

    [Header("Skill 2 Info")]
    public string skill2Name;
    public Sprite skill2Sprite;
    public float skill2Delay;
    [TextArea]
    public string skill2Tooltip;

    public enum AttackMode 
    {
        FirstTarget,
        StrongestTarget
    }

    public virtual void UpgradeTower() { }
    protected virtual void AttackTarget() { }

    protected virtual IEnumerator TowerAI() { yield return null; }

    protected virtual void SetFirstTarget() { }

    protected virtual void SetStrongestTarget() { } 

    protected virtual void ActiveSkill1() { }
    protected virtual void ActiveSkill2() { }


}

