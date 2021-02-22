using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagemode : MonoBehaviour
{
    public bool isHardmode;
    public int choosenumber;
    public static Stagemode instance = null;

    public bool clearnormalmap1 = false;
    public bool clearhardmap1 = false;
    public bool clearmap1nodamage = false;
    public bool clearnormalmap2 = false;
    public bool clearhardmap2 = false;
    public bool clearmap2nodamage = false;
    public bool clearnormalmap3 = false;
    public bool clearhardmap3 = false;
    public bool clearmap3nodamage = false;
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



    public int Mapclear(int number)
    {
        int star=0;
        if (number == 1)
        {
            if (clearnormalmap1)
            {
                star++;
            }
            if (clearhardmap1)
            {
                star++;
            }
            if (clearmap1nodamage)
            {
                star++;
            }
            return star;
        }
        else if (number == 2)
        {
            if (clearnormalmap2)
            {
                star++;
            }
            if (clearhardmap2)
            {
                star++;
            }
            if (clearmap2nodamage)
            {
                star++;
            }
            return star;
        }
        else if (number == 3)
        {
            if (clearnormalmap3)
            {
                star++;
            }
            if (clearhardmap3)
            {
                star++;
            }
            if (clearmap3nodamage)
            {
                star++;
            }
            return star;
        }
        else
            return star;

    }
}
