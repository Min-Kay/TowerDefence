using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagemode : MonoBehaviour
{
    public bool isHardmode;
    public int choosenumber;
    public static Stagemode instance = null;

    public bool clearmap1 = false;
    public bool clearmap2 = false;
    public bool clearmap3 = false;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void hardmode(bool mode)
    {
        isHardmode = mode;
    }



    public bool Mapclear(int number)
    {
        if (number == 1 && clearmap1)
        {
            return true;
        }
        else if (number == 2 && clearmap2)
        {
            return true;
        }
        else if (number == 3 && clearmap3)
        {
            return true;
        }
        else
            return false;

    }
}
