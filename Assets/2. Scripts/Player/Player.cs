using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private int hp;
    private int money;
    
    private Player()
    {
        hp = 10;
        money = 0;
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

    public void setMoney(int money)
    {
        this.money = money;
    }

    public int getHp()
    {
        return hp;
    }
    
    public void dameged(int damege)
    {
        this.hp -= hp;
    }
}
