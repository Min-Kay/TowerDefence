using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpChanger : MonoBehaviour
{
    private Text hpText;
    private void Start()
    {
        hpText = GetComponentInChildren<Text>();
    }
    public void updateHp(int hp)
    {
        hpText.text = hp.ToString();
    }
}
