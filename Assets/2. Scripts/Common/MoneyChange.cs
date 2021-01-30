using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyChange : MonoBehaviour
{
    private Text moneyText;

    void Start()
    {
        moneyText = GetComponentInChildren<Text>();
    }

    public void updateMoney()
    {
        moneyText.text = Player.getInstance().getMoney().ToString();
    }
}
