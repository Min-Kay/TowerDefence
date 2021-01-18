using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
   public bool isAllow { set; get; }

    private void Awake()
    {
        isAllow = true;
    }
}
