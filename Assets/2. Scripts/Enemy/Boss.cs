using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float Speed;

    public override void Setup(GameManager gameManager, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        Speed = movement2D.moveSpeed;
        isBoss = true;

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;
        //시작위치
        transform.position = wayPoints[currentIndex].position;

        StartCoroutine(EnemyAI());
        StartCoroutine(OnMove());
        StartCoroutine(Rush());
    }


    IEnumerator Rush()
    {
        while (state == State.MOVE)
        {
            //Debug.Log("Rush가 굴러가는중");
            movement2D.moveSpeed += 3;
            Invoke("ResetSpeed", 0.5f);
            StartCoroutine(EnemyCooldown1(skill1Delay));
            yield return new WaitForSeconds(skill1Delay);
        }
    }

    public void ResetSpeed()
    {
        movement2D.moveSpeed -= 3;
    }
}
