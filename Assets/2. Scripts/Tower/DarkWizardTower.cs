using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWizardTower : TowerCtrl
{
    private GameObject explosion;
    public GameObject explosionObject;

    private void Awake()
    {
        StartCoroutine(TowerAI());
        StartCoroutine(Stun());
    }

    protected override IEnumerator TowerAI()
    {
        while (!GameManager.instance.isGameOver)
        {
            switch (mode)
            {
                case AttackMode.FirstTarget:
                    SetFirstTarget();
                    if (explosion != null)
                    {
                        Explosion();
                        yield return null;
                    }
                    else
                    {
                        AttackTarget();
                        yield return new WaitForSeconds(attackDelay);
                    }
                    break;
                case AttackMode.StrongestTarget:
                    SetStrongestTarget();
                    if (explosion != null)
                    {
                        Explosion();
                        yield return null;
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
        while (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            if (target != null)
            {
                explosion = Instantiate(explosionObject, target.transform.position, target.transform.rotation);
                Destroy(explosion, 3);
                yield return new WaitForSeconds(skill2Delay);
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator Stun()
    {
        while (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            if (target != null)
            {
                target.GetComponent<Enemy>().changeState(Enemy.State.STOP);
                yield return new WaitForSeconds(skill1Delay);
            }
            else
            {
                yield return null;
            }
        }
    }
}
