using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int hp;
    private int money;
    private GameObject playerSkill1;
    private GameObject playerSkill2;
    
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
    
    public void damaged(int damage)
    {
        this.hp -= damage;
    }
}
