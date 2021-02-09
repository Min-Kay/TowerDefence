using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTower : TowerCtrl
{
    public float skill1Duration;
    public float skill1Damage;
    public float skill1Distance;


    IEnumerator DragonBreath()
    {
        while(!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {

            if (!target && skill1Distance<=Vector2.Distance(transform.position,target.transform.position))
            {
                //브레스 작동
                //만약 스킬 UI 작동 시 이미지 변화 작동
                yield return new WaitForSecondsRealtime(skill1Duration);
            }
            else
                yield return null;

        }

        
    }
}
