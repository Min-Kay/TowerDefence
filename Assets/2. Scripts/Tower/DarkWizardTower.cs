using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWizardTower : TowerCtrl
{

    public GameObject explosionObject;
    private GameObject explosionTarget = null;

    private void Awake()
    {
        StartCoroutine(TowerAI());
        StartCoroutine(Explosion());
    }

    protected override IEnumerator TowerAI()
    {
        while (!GameManager.instance.isGameOver || !GameManager.instance.isGameClear)
        {
            switch (mode)
            {
                case AttackMode.FirstTarget:
                    SetFirstTarget();

                    if (explosionTarget == null)
                    {
                        explosionTarget = target;
                    }
                    else
                    {
                        AttackTarget();
                        yield return new WaitForSeconds(attackDelay);
                    }

                    break;
                case AttackMode.StrongestTarget:
                    SetStrongestTarget();
                    if (explosionTarget == null)
                    {
                        explosionTarget = target;
                    }
                    else
                    {
                        AttackTarget();
                        yield return new WaitForSeconds(attackDelay);
                    }
                    break;
            }
            yield return null;
        }
    }

    private IEnumerator Explosion()
    {
        while (!GameManager.instance.isGameOver)
        {
            if (explosionTarget != null)
            {
                var explosion = Instantiate(explosionObject, explosionTarget.transform.position, explosionTarget.transform.rotation);
                Destroy(explosion, 1f);
                StartCoroutine(Cooldown2(skill2Delay));
                yield return new WaitForSeconds(skill2Delay);
                explosionTarget = null;
            }
            else
            {
                yield return null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Movement2D>().ChangeSpeed(0.7f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Movement2D>().InitSpeed();
        }
    }
}
