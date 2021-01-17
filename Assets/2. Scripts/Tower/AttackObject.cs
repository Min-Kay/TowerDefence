using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    [Header("AttackObject Setting")]
    public GameObject targetEnemy;
    public int power;
    public float speed;

    [HideInInspector]
    public GameObject fatherTower = null; //공격 매개체를 소환한 타워 

    void Update()
    {
        if(targetEnemy != null)
        { 
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
       
    }
}
