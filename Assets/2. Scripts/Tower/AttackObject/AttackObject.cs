using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    [HideInInspector]
    public float power;

    private float speed;

    [Header("Image Angle Offset")]
    public float angleOffset;

    [HideInInspector]
    public GameObject targetEnemy;

    [HideInInspector]
    public GameObject fatherTower = null; //공격 매개체를 소환한 타워 

    private Color color;
    private void Start()
    {
        power = fatherTower.GetComponent<TowerCtrl>().power;
        speed = fatherTower.GetComponent<TowerCtrl>().speed;
    }

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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == targetEnemy)
        {
            if (collision.GetComponent<Enemy>().isBoss)
            {
                collision.GetComponent<Enemy>().HP -= (power-5);
            }
            else
            {
                collision.GetComponent<Enemy>().HP -= power;
            }
            
            if (collision.GetComponent<Enemy>().HP <= 0)
            {
                fatherTower.GetComponent<TowerBaseCtrl>().killCount++;
            }
            color = collision.GetComponent<Enemy>().spr.color;
            color.a = 0.4f;
            collision.GetComponent<Enemy>().spr.color = color;
            collision.GetComponent<Enemy>().Invoke("Damaged", 0.2f);
            Destroy(gameObject);
        }
    }
}
