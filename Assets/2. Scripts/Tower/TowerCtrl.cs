using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCtrl : MonoBehaviour
{
    [Header("Tower Information")]
    public int price;
    public int upgradePhase = 1;
    public int killCount = 0;

    [Header("Attack Object Prefab")]
    public GameObject attackPrefab;

    [Header("Attack Info")]
    public float attackDelay;
    public AttackMode mode;
   
    [Header("Tower Distance")]
    public float distance;

    //Enemy List
    private GameObject[] enemys;
    private GameObject target;

    public enum AttackMode 
    {
        FirstTarget,
        StrongestTarget
    }

    void Start()
    {
        enemys = null;
        StartCoroutine(TowerAI());
    }

    void upgradeTower()
    {
        upgradePhase++;
    }

    IEnumerator TowerAI()
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

    private void SetFirstTarget()
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

    private void SetStrongestTarget()
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

    private void AttackTarget()
    {
        if (target != null)
        {
            var attackObject = Instantiate(attackPrefab, transform.position, transform.rotation);
            attackObject.GetComponent<AttackObject>().fatherTower = this.gameObject;
            attackObject.GetComponent<AttackObject>().targetEnemy = target;
        }
    }
}
