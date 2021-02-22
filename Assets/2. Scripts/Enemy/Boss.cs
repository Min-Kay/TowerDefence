using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // Start is called before the first frame update
    private void Awake ()
    {
        spr = GetComponent<SpriteRenderer>();
        canvasTransform = GameObject.FindWithTag("Canvas").GetComponent<Transform>();
        HP = initHP;
        SpawnEnemyHPSlider();
    }

    private void FixedUpdate()
    {
        if (HP <= 0)
        {
            state = State.DIE;
        }
    }

    public void Setup(GameManager gameManager, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;
        //시작위치
        transform.position = wayPoints[currentIndex].position;

        StartCoroutine(EnemyAI());
        StartCoroutine(OnMove());
    }

    protected override IEnumerator EnemyAI()
    {
        while (!GameManager.instance.isGameOver)
        {
            switch (state)
            {
                case State.MOVE:
                    break;
                case State.STOP:
                    break;
                case State.END:
                    GameManager.instance.currentEnemyCount--;
                    Player.getInstance().damaged(attackPower);
                    GameManager.instance.UpdateHP();
                    Destroy(this.gameObject);
                    break;
                case State.DIE:
                    GameManager.instance.currentEnemyCount--;
                    Player.getInstance().ChangeMoney(gold);
                    GameManager.instance.UpdateMoney();
                    Destroy(this.gameObject);
                    break;
            }
            yield return null;
        }
    }

    protected override IEnumerator OnMove()
    {
        NextMoveTo();

        while (state == State.MOVE)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.moveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }


    protected override void NextMoveTo()
    {
        if (currentIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;

            Vector3 direction = wayPoints[currentIndex].position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + angleOffset));

            movement2D.MoveTo(direction.normalized);
        }
        else
        {

            state = State.END;
        }
    }

    protected override void Damaged()
    {
        color = spr.color;
        color.a = 0.4f;
        spr.color = color;
        //0.2초 기다리기
        color.a = 1.0f;
        spr.color = color;
    }

    protected override void SpawnEnemyHPSlider()
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;
        sliderClone.GetComponent<SliderAutoPosition>().Setup(this.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(this.GetComponent<Enemy>());
    }

    protected override IEnumerator EnemyCooldown1(float duration)
    {
        skill1CoolTime = 1;
        while (skill1CoolTime > 0)
        {
            skill1CoolTime -= 1 * Time.smoothDeltaTime / duration;
            yield return null;
        }
    }
    protected override IEnumerator EnemyCooldown2(float duration)
    {
        skill2CoolTime = 1;
        while (skill2CoolTime > 0)
        {
            skill2CoolTime -= 1 * Time.smoothDeltaTime / duration;
            yield return null;
        }
    }

    public override float GetCooltime(int i)
    {
        if (i == 1)
        {
            return skill1CoolTime;
        }
        else if (i == 2)
        {
            return skill2CoolTime;
        }
        else
            return 0;
    }
}
