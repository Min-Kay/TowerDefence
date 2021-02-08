using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDamage : MonoBehaviour
{
    public float duration = 5.0f;
    public float delay = 0.5f;
    public float damage = 1.0f;

    private float initTime;
    [SerializeField]
    private float remainTime;
    private Enemy enemy;
    private SpriteRenderer sprite;
    private Color initColor;
    public TowerBaseCtrl fatherTower;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        sprite = enemy.GetComponent<SpriteRenderer>();
        initTime = Time.time + duration;
        initColor = sprite.color;
        StartCoroutine("Burn");
    }

    private void Update()
    {
        remainTime = initTime - Time.time;
    }

    IEnumerator Burn()
    {
        while (!GameManager.instance.isGameClear || !GameManager.instance.isGameOver)
        {
            if(initTime - Time.time >= 0) 
            {
                enemy.HP -= damage;
                if(enemy.HP <= 0)
                {
                    fatherTower.killCount++; 
                }
                enemy.Invoke("Damaged", 0.2f);
                sprite.color = new Color(1, 0, 0, 1);
                yield return new WaitForSeconds(delay);
            }
            else if(enemy.HP <=0)
            {
                Destroy(GetComponent<BurnDamage>());
                yield return null;
            }
            else
            {
                sprite.color = initColor;
                Destroy(GetComponent<BurnDamage>());
                yield return null;
            }
        }
    }


}
