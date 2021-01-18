using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy HP")]
    public float initHP;
    public float HP;

    [Header("Damage Player")]
    [Tooltip("When an enemy reaches the end of a checkpoint, it damages the player by this power.")]
    public int attackPower;

    public State state = State.MOVE;

    [Header("Enemy HP UI")]
    public GameObject enemyHPSliderPrefab;// ��ü�³�Ÿ���� Slider UI ������
    
    private Transform canvasTransform;//UI ǥ���ϴ� canvas ������Ʈ ��ġ

    private int wayPointCount;//�̵���� ����
    private Transform[] wayPoints;//�̵���� ����
    private int currentIndex = 0;
    private Movement2D movement2D;

    private Color color;
    private SpriteRenderer spr;

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
        Setup(GameManager.instance.wayPoints);
        SpawnEnemyHPSlider();
    }

    private void Update()
    {
        if(HP<=0)
        {
            state = State.DIE;
        }
    }

    public void Setup(Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;
        //������ġ
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
                    //NextMoveTo();
                    break;
                case State.STOP:
                    break;
                case State.DIE:
                    Destroy(this.gameObject);
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

            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            GameManager.instance.playerHp -= attackPower;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "AttackObject" && collision.GetComponent<AttackObject>().targetEnemy == this.gameObject)
        {
            HP -= collision.GetComponent<AttackObject>().power;
            if (HP <= 0)
            {
                collision.GetComponent<AttackObject>().fatherTower.GetComponent<TowerCtrl>().killCount++;
            }
            Destroy(collision.gameObject);
            color = spr.color;
            color.a = 0.4f;
            spr.color = color;
            Invoke("Damaged", 0.2f);
        }
    }

    private void Damaged()
    {
        color.a = 1.0f;
        spr.color = color;
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