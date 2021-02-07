using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTower : TowerCtrl
{
    [Header("Skill 1 Addtional Info")]
    [SerializeField]
    private GameObject flamePrefab;
    private GameObject flame;
    public float skill1Distance;
    public float skill1Duration;

    private UiCtrl ui;
    private float initTime;

    private void Awake()
    {
        ui = GameObject.Find("UIManager").GetComponent<UiCtrl>();
        StartCoroutine(TowerAI());
        StartCoroutine(DragonBreath());
    }

    protected override IEnumerator TowerAI()
    {
        while (!GameManager.instance.isGameOver || !GameManager.instance.isGameClear)
        {
            switch (mode)
            {
                case AttackMode.FirstTarget:
                    SetFirstTarget();
                    if(flame != null)
                    {
                        flame.GetComponent<DragonBreath>().target = target;
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
                    if(flame != null)
                    {
                        flame.GetComponent<DragonBreath>().target = target;
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

   IEnumerator DragonBreath()
    {
        while(!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {

            if (target != null && skill1Distance >= Vector2.Distance(transform.position, target.transform.position))
            {
                flame = Instantiate(flamePrefab, transform.position, transform.rotation);
                flame.GetComponent<DragonBreath>().fatherTower = gameObject;
                flame.GetComponent<DragonBreath>().target = target;
                Destroy(flame, skill1Duration);
                initTime = Time.time;
                ui.CallCooldown(ui.cooldown1, skill1Delay);
                yield return new WaitForSeconds(skill1Delay);
            }
            else
                yield return null;
        }
    }
}
