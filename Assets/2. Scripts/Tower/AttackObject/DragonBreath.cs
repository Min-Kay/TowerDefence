using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBreath : MonoBehaviour
{
    
    [SerializeField]
    private float angleOffset;
    public float damage = 1.0f;
    public float delay = 0.3f;

    [HideInInspector]
    public GameObject fatherTower;
    [HideInInspector]
    public GameObject target;
    private float initTime;
    private SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        StartCoroutine(FlipFlame());
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + angleOffset));
        }
    }

    IEnumerator FlipFlame()
    {
        while (gameObject)
        {
            if (sp.flipX == true)
            {
                sp.flipX = false;
                yield return new WaitForSeconds(0.1f);
            }
            else if (sp.flipX == false)
            {
                sp.flipX = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            initTime = Time.time + delay;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.CompareTag("Enemy"))
        {
            if(!collision.GetComponent<BurnDamage>())
            {
                collision.gameObject.AddComponent<BurnDamage>();
            }

            if(initTime - Time.time <=0)
            {
                collision.GetComponent<Enemy>().HP -= damage;
                initTime = Time.time + delay;
            }
        }
    }
}
