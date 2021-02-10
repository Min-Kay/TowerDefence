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

    private Transform canvasTransform;//UI 표현하는 canvas 오브젝트 위치

    private int wayPointCount;//이동경로 갯수
    private Transform[] wayPoints;//이동경로 정보
    private int currentIndex = 0;
    private Movement2D movement2D;

    private Color color;
    private SpriteRenderer spr;

    [Header("Enemy Gold")]
    public int gold;

    public enum State
    {
        MOVE,
        STOP,
        DIE
    }

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        canvasTransform = GameObject.FindWithTag("Canvas").GetComponent<Transform>();
        HP = initHP;
        SpawnEnemyHPSlider();
    }

    private void Update()
    {
        if(HP<=0)
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

    IEnumerator EnemyAI()
    {
        while(!GameManager.instance.isGameOver)
        {
            switch (state) 
            {
                case State.MOVE:
                    break;
                case State.STOP:
                    break;
                case State.DIE:
                    Destroy(this.gameObject);
                    GameManager.instance.currentEnemyCount--;
                    Player.getInstance().ChangeMoney(gold);
                    GameManager.instance.UpdateMoney();
                    break;
            }
            yield return null;
        }
    }

    private IEnumerator OnMove()
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

    private void NextMoveTo()
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
            GameManager.instance.currentEnemyCount--;
            Player.getInstance().damaged(attackPower);
            GameManager.instance.UpdateHP();
            Destroy(this.gameObject);
        }
    }

    private void Damaged()
    {
        color.a = 1.0f;
        spr.color = color;
    }


    public void changeState(State state){
        this.state = state;
    }

    private IEnumerator MoveAgain()
    {
        yield return new WaitForSeconds(2);
        this.state = State.MOVE;
        StartCoroutine(OnMove());
    }

    private void SpawnEnemyHPSlider()
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;
        sliderClone.GetComponent<SliderAutoPosition>().Setup(this.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(this.GetComponent<Enemy>());
    }
}
