using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
<<<<<<< HEAD
    [Header("Enemy HP")]
    public float initHP;
    public float HP;
    
=======
    [Header("Enemy State")]
    public State state = State.MOVE;
    public float initHP;
    public float HP;
>>>>>>> 7c49708d9de75a40dfea46c0b874a4bbf732cb0f

    [Header("Damage Player")]
    [Tooltip("When an enemy reaches the end of a checkpoint, it damages the player by this power.")]
    public int attackPower;

    [Header("Enemy UI & Image Angle Offset")]
    public GameObject enemyHPSliderPrefab;// ��ü�³�Ÿ���� Slider UI ������
    public float angleOffset;

    private Transform canvasTransform;//UI ǥ���ϴ� canvas ������Ʈ ��ġ

    [Header("Enemy HP UI")]
    public GameObject enemyHPSliderPrefab;// ��ü�³�Ÿ���� Slider UI ������
    
    private Transform canvasTransform;//UI ǥ���ϴ� canvas ������Ʈ ��ġ

    private int wayPointCount;//�̵���� ����
    private Transform[] wayPoints;//�̵���� ����
    private int currentIndex = 0;
    private Movement2D movement2D;

    private Color color;
    private SpriteRenderer spr;

<<<<<<< HEAD
    [Header("playergold")]
    public int gold;
    private int playergold;
=======
>>>>>>> 7c49708d9de75a40dfea46c0b874a4bbf732cb0f
    public enum State
    {
        MOVE,
        STOP,
        DIE
    }

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
<<<<<<< HEAD
        
        
        canvasTransform = GameObject.FindWithTag("Canvas").GetComponent<Transform>();
        HP = initHP;
        //Setup(GameManager.instance.wayPoints);
=======
        canvasTransform = GameObject.FindWithTag("Canvas").GetComponent<Transform>();
        HP = initHP;
        Setup(GameManager.instance.wayPoints);
>>>>>>> 7c49708d9de75a40dfea46c0b874a4bbf732cb0f
        SpawnEnemyHPSlider();
    }

    private void Update()
    {
        if(HP<=0)
        {
            state = State.DIE;
            
        }
    }

    public void Setup(GameManager gameManager,Transform[] wayPoints)
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
                    playergold=Player.getInstance().getMoney();//����� �÷��̾�� ���Ͱ���߰�
                    Player.getInstance().setMoney(playergold + gold);
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
<<<<<<< HEAD

            //���⼭ �����̻��� �־�ߵ�
=======
>>>>>>> 7c49708d9de75a40dfea46c0b874a4bbf732cb0f
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
