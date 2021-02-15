using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackObject : AttackObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject==targetEnemy)
        {
            collision.GetComponent<Enemy>().HP -= power;
            if (collision.GetComponent<Enemy>().HP <= 0)
            {
                fatherTower.GetComponent<TowerBaseCtrl>().killCount++;
            }

            if (collision.gameObject == targetEnemy.gameObject && !collision.GetComponent<BurnDamage>())
            {
                collision.gameObject.AddComponent<BurnDamage>();
                collision.GetComponent<BurnDamage>().fatherTower = gameObject.GetComponent<TowerBaseCtrl>();
            }

            Destroy(gameObject);
            collision.GetComponent<Enemy>().Invoke("Damaged", 0.2f);
        }
    }
}
