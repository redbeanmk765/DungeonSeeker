using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : enemy
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
        move,
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

                    if (CanAttackPlayer())
                    {
                        // attackMotionDone = true;
                        ChangeState(State.attack);
                    }
                    else
                        ChangeState(State.move);
                }
                break;
            case State.move:
                if (CanSeePlayer())
                {

                    if (CanAttackPlayer())
                    {
                        if (attackCoolTime == false)
                        {
                            ChangeState(State.attack);
                        }
                    }
                }
                else
                {
                    ChangeState(State.idle);
                }
                break;
            case State.attack:
                if (attackMotionDone)
                {
                    if (CanSeePlayer())
                    {
                        if (attackCoolTime == true)
                        {
                            ChangeState(State.move);
                        }

                    }
                    else
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
        if (rigid.velocity.x > MaxSpeedX)
        {
            rigid.velocity = new Vector2(MaxSpeedX, rigid.velocity.y);
        }

        else if (rigid.velocity.x < -1 * MaxSpeedX)
        {
            rigid.velocity = new Vector2(-1 * MaxSpeedX, rigid.velocity.y);
        }
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
            case State.move:
                fsm.ChangeState(new MoveState(this, player));
                animator.SetInteger("State", 2);
                break;
            case State.attack:
                fsm.ChangeState(new AttackState(this, player));
                animator.SetInteger("State", 3);
                break;
            case State.die:
                fsm.ChangeState(new DieState(this, player));
                animator.SetInteger("State", 4);
                break;
        }
    }

    private bool CanSeePlayer()
    {
        if (Mathf.Abs(enemy.GetComponent<Transform>().position.x - player.GetComponent<Transform>().position.x) <= 9
            && player.GetComponent<Transform>().position.y - enemy.GetComponent<Transform>().position.y <= 6
            && player.GetComponent<Transform>().position.y - enemy.GetComponent<Transform>().position.y >= -6)
        {

            return true;

        }
        else
            return false;
        //  플레이어 탐지 구현
    }

    private bool CanAttackPlayer()
    {
        if (Mathf.Abs(enemy.GetComponent<Transform>().position.x - player.GetComponent<Transform>().position.x) < 5.3
            && player.GetComponent<Transform>().position.y - enemy.GetComponent<Transform>().position.y <= 6
            && player.GetComponent<Transform>().position.y - enemy.GetComponent<Transform>().position.y >= -6)
        {

            return true;
        }
        else
            return false;
        //  사정거리 체크 구현
    }

    public void AttackShoot1()
    {
        if ((this.transform.position.x - player.transform.position.x) < 0)
        {
            angle = 1;
        }
        else
        {
            angle = -1;
        }

        if (angle == 1)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }

        attackMotionDone = false;
    }
    public void AttackShoot2()
    {
        enemyProjectile = Instantiate(monsterStat.projectile);
        enemyProjectile.transform.position = this.transform.position;
        enemyProjectile.gameObject.GetComponent<targetEnemyProjectile>().target = player;
        enemyProjectile.gameObject.GetComponent<targetEnemyProjectile>().dmg = monsterStat.enemyDamage;
        enemyProjectile.gameObject.GetComponent<targetEnemyProjectile>().speed = monsterStat.projectileSpeed;

    }
    public void AttackShoot3()
    {

        attackMotionDone = true;
        attackCoolTime = true;
        StartCoroutine(attackCoolTimeCor());
    }
    IEnumerator attackCoolTimeCor()
    {

        yield return new WaitForSeconds(4);
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

    public class MoveState : BaseState
    {

        public MoveState(enemy enemy, GameObject player) : base(enemy, player) { }


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


            if (angle == 1)
            {
                if (curEnemy.gameObject.GetComponent<bat>().isRun == false)
                {
                    curEnemy.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    curEnemy.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
            }
            else
            {
                if (curEnemy.gameObject.GetComponent<bat>().isRun == false)
                {
                    curEnemy.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    curEnemy.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
            }

            //curEnemy.transform.position = Vector2.MoveTowards(curEnemy.transform.position, new Vector2 (curPlayer.transform.position.x, curEnemy.transform.position.y), curEnemy.monsterStat.moveSpeed * Time.deltaTime);
            if (Mathf.Abs(curEnemy.GetComponent<Transform>().position.x - curPlayer.GetComponent<Transform>().position.x) >= 5)
            {
                curEnemy.gameObject.GetComponent<bat>().isRun = false;
                curEnemy.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * angle * curEnemy.monsterStat.moveSpeed, ForceMode2D.Impulse);
            }
            else if (Mathf.Abs(curEnemy.GetComponent<Transform>().position.x - curPlayer.GetComponent<Transform>().position.x) > 4.7
                && Mathf.Abs(curEnemy.GetComponent<Transform>().position.x - curPlayer.GetComponent<Transform>().position.x) < 5.3)
            {
                curEnemy.gameObject.GetComponent<bat>().isRun = false;
                curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                curEnemy.gameObject.GetComponent<bat>().isRun = true;
                curEnemy.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -angle * curEnemy.monsterStat.moveSpeed, ForceMode2D.Impulse);
            }
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
            curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }

        public override void OnStateUpdate()
        {

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