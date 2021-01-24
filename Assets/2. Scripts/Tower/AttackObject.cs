using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    [Header("AttackObject Setting")]
    public float power;
    public float speed;

    [Header("Image Angle Offset")]
    public float angleOffset;

    [HideInInspector]
    public GameObject targetEnemy;

    [HideInInspector]
    public GameObject fatherTower = null; //공격 매개체를 소환한 타워 

    void Update()
    {
        if(targetEnemy != null)
        { 
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
            Vector3 direction = targetEnemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+angleOffset));
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
