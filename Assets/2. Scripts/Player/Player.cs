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
    
<<<<<<< HEAD
    public static Player getInstance()
=======
    public Player getInstance()
>>>>>>> 7c49708d9de75a40dfea46c0b874a4bbf732cb0f
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
