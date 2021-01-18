using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHPViewer : MonoBehaviour
{
    private Enemy enemyHP;
    private Slider hpSlider;

    public void Setup(Enemy enemyHP)
    {
        this.enemyHP = enemyHP;
        hpSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        hpSlider.value = enemyHP.HP / enemyHP.initHP;
    }
}
