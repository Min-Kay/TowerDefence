using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int hp;
    private int money;
    private Skill playerSkill1;
    private Skill playerSkill2;
    
    private Player()
    {
        hp = GameManager.instance.playerHp;
        money = GameManager.instance.playerMoney;
    }

    private static class PlayerHolder{
        public static readonly Player instance = new Player();
    }
    
    public static Player getInstance()
    {
        return PlayerHolder.instance;
    }

    public int getMoney()
    {
        return money;
    }

    public void ChangeMoney(int cost)
    {
        this.money += cost;
    }

    public int getHp()
    {
        return hp;
    }
    
    public void setMoney(int Money)
    {
        money = Money;
    }

    public void setHP(int HP)
    {
        hp = HP;
    }

    public void damaged(int damage)
    {
        this.hp -= damage;
    }

    public void ChangeSkill(Skill skill, string name)
    {
        if (name == playerSkill1.skillName)
        {
            playerSkill1 = skill;
        }
        else if (name == playerSkill2.skillName)
        {
            playerSkill2 = skill;
        }
    }
}
