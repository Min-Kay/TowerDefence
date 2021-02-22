using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy State")]
    public State state = State.MOVE;
    public float initHP;
    public float HP;

    [Header("Damage Player")]
    [Tooltip("When an enemy reaches the end of a checkpoint, it damages the player by this power.")]
    public int attackPower;

    [Header("Enemy UI & Image Angle Offset")]
    public GameObject enemyHPSliderPrefab;// 적체력나타내는 Slider UI 프리팹
    public float angleOffset;

    public Transform canvasTransform;//UI 표현하는 canvas 오브젝트 위치

    public int wayPointCount;//이동경로 갯수
    public Transform[] wayPoints;//이동경로 정보
    public int currentIndex = 0;
    public Movement2D movement2D;

    public Color color;
    public SpriteRenderer spr;
    public bool isBoss = false;
    [Header("Enemy Gold")]
    public int gold;

    [Header("Skill 1 Info")]
    public string skill1Name;
    public Sprite skill1Sprite;
    public float skill1Delay;
    protected float skill1CoolTime = 0;
    [TextArea]
    public string skill1Tooltip;

    [Header("Skill 2 Info")]
    public string skill2Name;
    public Sprite skill2Sprite;
    public float skill2Delay;
    protected float skill2CoolTime = 0;
    [TextArea]
    public string skill2Tooltip;

    public enum State
    {
        MOVE,
        STOP,
        END,
        DIE
    }

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        canvasTransform = GameObject.FindWithTag("Canvas").GetComponent<Transform>();
        if(Stagemode.instance.isHardmode)
        {
            HP = initHP*2;
        }
        else
        {
            HP = initHP;
        }
        
        SpawnEnemyHPSlider();
    }

    private void FixedUpdate()
    {
        if(HP<=0)
        {
            state = State.DIE;
        }
    }

    public virtual void Setup(GameManager gameManager, Transform[] wayPoints)
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

    protected IEnumerator EnemyAI()
    {
        while(!GameManager.instance.isGameOver)
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

    protected IEnumerator OnMove()
    {
        NextMoveTo();
        //Debug.Log("움직이는중!");
        while (state == State.MOVE)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.moveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }

    protected void NextMoveTo()
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

    protected void Damaged()
    {
        color = spr.color;
        color.a = 0.4f;
        spr.color = color;
        //0.2초 기다리기
        color.a = 1.0f;
        spr.color = color;
    }

    protected void SpawnEnemyHPSlider()
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;
        sliderClone.GetComponent<SliderAutoPosition>().Setup(this.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(this.GetComponent<Enemy>());
    }

    protected IEnumerator EnemyCooldown1(float duration) 
    {
        skill1CoolTime = 1;
        while (skill1CoolTime > 0)
        {
            skill1CoolTime -= 1 * Time.smoothDeltaTime / duration;
            yield return null;
        }
    }
    protected IEnumerator EnemyCooldown2(float duration) 
    {
        skill2CoolTime = 1;
        while (skill2CoolTime > 0)
        {
            skill2CoolTime -= 1 * Time.smoothDeltaTime / duration;
            yield return null;
        }
    }

    public float GetCooltime(int i) 
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
