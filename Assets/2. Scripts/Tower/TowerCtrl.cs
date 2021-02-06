using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCtrl : TowerBaseCtrl
{

    void Start()
    {
        StartCoroutine(TowerAI());
        StartCoroutine(ActiveSkill());
    }

    public override void UpgradeTower()
    {
        if (upgradePhase < maxUpgrade)
        {
            upgradePhase++;
            upgradeCost *= 2;
            power += 10;
        }
    }

    protected override IEnumerator TowerAI()
    {
        while (!GameManager.instance.isGameOver)
        {
            switch (mode)
            {
                case AttackMode.FirstTarget:
                    SetFirstTarget();
                    AttackTarget();
                    yield return new WaitForSeconds(attackDelay);
                    break;
                case AttackMode.StrongestTarget:
                    SetStrongestTarget();
                    AttackTarget();
                    yield return new WaitForSeconds(attackDelay);
                    break;
            }
            yield return null;
        }
    }

    protected override void SetFirstTarget()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemys != null)
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                if(distance >= Vector2.Distance(enemys[i].transform.position, this.gameObject.transform.position))
                {
                    target = enemys[i];
                    break;
                }
            }
        }
    }

    protected override void SetStrongestTarget()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> targetList = new List<GameObject>();

        if (enemys != null)
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                if (distance >= Vector2.Distance(enemys[i].transform.position, this.gameObject.transform.position))
                {
                    targetList.Add(enemys[i]);
                }
            }
            
            if(targetList.Count != 0)
            {
                GameObject strongest = targetList[0];

                for (int i = 1; i < targetList.Count; i++)
                {
                    if (targetList[i].GetComponent<Enemy>().initHP > strongest.GetComponent<Enemy>().initHP)
                    {
                        strongest = targetList[i];
                    }
                }

                target = strongest;
            }
        }
    }

    protected override void AttackTarget()
    {
        if (target != null)
        {
            var attackObject = Instantiate(attackPrefab, transform.position, transform.rotation);
            attackObject.GetComponent<AttackObject>().fatherTower = this.gameObject;
            attackObject.GetComponent<AttackObject>().targetEnemy = target;
        }
    }

    private IEnumerator ActiveSkill()
    {
        ActiveSkill2();
        yield return new WaitForSeconds(skill2Delay);
    }

    protected override void ActiveSkill2()
    {
        if(target != null)
        {

        }
    }
}
