using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : enemy
{
    public GameObject enemy;
    public GameObject player;
    public GameObject colPlayer;
    public GameObject[] players;
    public Animator animator;
    public GameObject enemyProjectile;
    public bool onWake = false;
    public bool onTrigger = false;
    public bool attackMotionDone = true;
    public bool onFlash = false;
    public bool IsDie = false;
    public Vector3 targetPos;
    public float nowHp;
    public float damaged;
    public Vector3 MoveTowardsVector;
    public float MaxSpeedX;
    public Rigidbody2D rigid;
    public int angle;
    public bool isRun;
    public bool attackCoolTime;



    private enum State
    {
        idle,
        attack,
        die
    }

    private State curState;
    private FSM fsm;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        MaxSpeedX = 2;
        players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0];
        curState = State.idle;
        fsm = new FSM(new IdleState(this, player));

        nowHp = monsterStat.maxHp;
        animator = GetComponent<Animator>();

        onFlash = false;
        IsDie = false;
        attackMotionDone = true;
        attackCoolTime = false;

        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), player.GetComponent<EdgeCollider2D>(), true);

    }


    private void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.000001f, 0);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.000001f, 0);
        if (this.damaged != 0)
        {
            if (!onFlash)
            {
                nowHp -= this.damaged;
                onFlash = true;
                StartCoroutine(FlashWhite());
            }
            this.damaged = 0;
        }
        if (nowHp <= 0)
        {
            if (IsDie == false)
            {
                this.curState = State.die;
                ChangeState(State.die);
                IsDie = true;
            }

        }

        switch (curState)
        {
            case State.idle:

                if (CanSeePlayer())
                {
                    if (attackCoolTime == false)
                    {
                        ChangeState(State.attack);
                    }
                }
                break;
            case State.attack:
                if (attackMotionDone)
                { 
                        if (attackCoolTime == true)
                        {
                            ChangeState(State.idle);
                        }

                }
                break;
            case State.die:
                if (this.gameObject.GetComponent<SpriteRenderer>().color.a <= 0)
                {
                    player.GetComponent<PlayerStat>().PlayerGold += monsterStat.enemyGold;
                    Destroy(this.gameObject);
                }
                break;

        }

        fsm.UpdateState();
    }
    private void FixedUpdate()
    {
      
    }


    IEnumerator FlashWhite()
    {
        while (onFlash)
        {
            this.GetComponent<SpriteRenderer>().material = this.monsterStat.flashMaterial;
            yield return new WaitForSecondsRealtime(0.1f);
            this.GetComponent<SpriteRenderer>().material = this.monsterStat.originalMaterial;

            if (onFlash == false)
            {
                yield break;
            }
            onFlash = false;


        }
        if (onFlash == false)
        {
            yield break;
        }
    }


    private void ChangeState(State nextState)
    {
        curState = nextState;
        switch (curState)
        {
            case State.idle:
                fsm.ChangeState(new IdleState(this, player));
                animator.SetInteger("State", 1);
                break;
            case State.attack:
                fsm.ChangeState(new AttackState(this, player));
                animator.SetInteger("State", 2);
                break;
            case State.die:
                fsm.ChangeState(new DieState(this, player));
                animator.SetInteger("State", 3);
                break;
        }
    }

    private bool CanSeePlayer()
    {
        if (player.GetComponent<PlayerStat>().IsSafeZone == false)
        {

            return true;

        }
        else
            return false;
        //  플레이어 탐지 구현
    }

    public void AttackMagic1()
    {
        attackMotionDone = false;
    }
    public void AttackMagic2()
    {
        enemyProjectile = Instantiate(monsterStat.projectile);
        enemyProjectile.transform.position = this.transform.position;
        enemyProjectile.gameObject.GetComponent<magicCircle>().enemyDamage = monsterStat.enemyDamage;
        enemyProjectile.gameObject.GetComponent<magicCircle>().projectileSpeed = monsterStat.projectileSpeed;

        
    }
    public void AttackMagic3()
    {
        attackMotionDone = true;
        attackCoolTime = true;
        StartCoroutine(attackCoolTimeCor());
    }

        IEnumerator attackCoolTimeCor()
    {

        yield return new WaitForSeconds(Random.Range(5.5f,7.5f));
        attackCoolTime = false;
        yield break;

    }

    public class IdleState : BaseState
    {
        public IdleState(enemy enemy, GameObject player) : base(enemy, player) { }
        public override void OnStateEnter()
        {
            curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }

        public override void OnStateUpdate()
        {
        }

        public override void OnStateExit()
        {
        }
    }

   

    public class AttackState : BaseState
    {
        public AttackState(enemy enemy, GameObject player) : base(enemy, player) { }


        public override void OnStateEnter()
        {

            
        }

        public override void OnStateUpdate()
        {
            int angle = 1;
            if ((curEnemy.transform.position.x - curPlayer.transform.position.x) < 0)
            {
                angle = 1;
            }
            else
            {
                angle = -1;
            }
            if (curEnemy.GetComponent<ghost>().attackMotionDone == true)
            {

                if (angle == 1)
                {
                    curEnemy.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    curEnemy.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
            }
        }

        public override void OnStateExit()
        {
        }
    }

    public class DieState : BaseState
    {
        public DieState(enemy enemy, GameObject player) : base(enemy, player) { }


        public override void OnStateEnter()
        {
            curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }

        public override void OnStateUpdate()
        {

            curEnemy.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ((curEnemy.gameObject.GetComponent<SpriteRenderer>().color.a) - 1 * Time.deltaTime));

        }

        public override void OnStateExit()
        {
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (this.curState != State.die)
        {

            if (col.CompareTag("Attack"))
            {

                this.damaged += col.gameObject.GetComponent<HitBox>().Dmg;

            }
        }

        if (this.curState != State.die && col.CompareTag("PlayerHitBox"))
        {
            player.GetComponent<PlayerStat>().damaged = monsterStat.enemyDamage;

        }
    }




}