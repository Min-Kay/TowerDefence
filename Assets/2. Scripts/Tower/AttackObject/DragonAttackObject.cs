using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackObject : AttackObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetEnemy.gameObject && !collision.GetComponent<BurnDamage>())
        {
            collision.gameObject.AddComponent<BurnDamage>();
        }
    }
}
