using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime : enemy
{
    public GameObject enemy;
    public GameObject player;
    public GameObject colPlayer;
    public GameObject[] players;
    public Animator animator;
    public bool onWake = false;
    public bool onTrigger = false;
    public bool attackMotionDone = true;
    public bool onFlash = false;
    public bool IsDie = false;
    public Vector3 targetPos;
    public bool isDash;
    public float nowHp;
    public float damaged;
    public Vector3 MoveTowardsVector;
    public float MaxSpeedX;
    public Rigidbody2D rigid;
    public int angle;


    private enum State
    {
        sleep,
        wake,
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
        curState = State.sleep;
        fsm = new FSM(new sleepState(this,player));

        nowHp = monsterStat.maxHp;
        animator = GetComponent<Animator>();

        onFlash = false;
        IsDie = false;

        //Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GetComponentsInChildren<BoxCollider2D>()[1],true);
        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), player.GetComponent<EdgeCollider2D>(), true);
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Ground"), true);
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"), true);

    }


    private void Update()
    {
        player = GameObject.Find("Player");
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
            case State.sleep:
                if (CanSeePlayer())
                {
                    onWake = true;
                    ChangeState(State.wake);
                }
                break;
            case State.wake:
                {
                    StartCoroutine(EnterIdle());
                    break;
                }
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
                        // attackMotionDone = true;
                        ChangeState(State.attack);
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
                            if (!CanAttackPlayer())
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
                this.GetComponent<SpriteRenderer>().material = this.monsterStat.originalMaterial;
                yield break;
            }
            onFlash = false;
            
            
        }
        if (onFlash == false)
        {
            this.GetComponent<SpriteRenderer>().material = this.monsterStat.originalMaterial;
            yield break;
        }
    }

    IEnumerator EnterIdle()
    {         
        while (onWake)
        {           
            yield return new WaitForSeconds(1.0f);

            if (onWake == false)
            {
                yield break;
            }
            if (this.curState != State.die)
            {
                ChangeState(State.idle);
            }
            onWake = false;
        }
        if (onWake == false)
        {
            yield break;
        }
    }

    private void ChangeState(State nextState)
    {
        curState = nextState;
        switch (curState)
        {
            case State.wake:
                fsm.ChangeState(new wakeState(this,player));
                animator.SetInteger("State", 1);
                break;
            case State.idle:
                fsm.ChangeState(new IdleState(this, player));
                animator.SetInteger("State", 2);
                break;
            case State.move:
                fsm.ChangeState(new MoveState(this, player));
                animator.SetInteger("State", 3);
                break;
            case State.attack:
                fsm.ChangeState(new AttackState(this, player));
                animator.SetInteger("State", 4);
                break;
            case State.die:
                fsm.ChangeState(new DieState(this, player));
                animator.SetInteger("State", 5);
                break;
        }
    }

    private bool CanSeePlayer()
    {
        if (Mathf.Abs(enemy.GetComponent<Transform>().position.x - player.GetComponent<Transform>().position.x) <= 6    
            && player.GetComponent<Transform>().position.y - enemy.GetComponent<Transform>().position.y <=  5
            && player.GetComponent<Transform>().position.y - enemy.GetComponent<Transform>().position.y >= -5)
        {

            return true;
            
        }
        else
            return false;
        //  플레이어 탐지 구현
    }

    private bool CanAttackPlayer()
    {
        if (Mathf.Abs(enemy.GetComponent<Transform>().position.x - player.GetComponent<Transform>().position.x) <= 2)
        {

            return true;
        }
        else
            return false;
        //  사정거리 체크 구현
    }

    public void AttackDash1()
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
    public void AttackDash2()
    {
        
        
    }
    public void AttackDash3()
    {

        isDash = true;
        MaxSpeedX = 8;
        
        StartCoroutine(dash());
    } 
    public void AttackDash4()
    {
        isDash = false;
        attackMotionDone = true;
        
        MaxSpeedX = 2;
    }
    IEnumerator dash()
    {
        while (isDash)
        {            
             yield return new WaitForEndOfFrame();
            // this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, 0.05f);
            if (this.curState != State.die)
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * angle * this.monsterStat.moveSpeed, ForceMode2D.Impulse);
            }
            if (isDash == false)
            {
                yield break;
            }                        
        }
        if (isDash == false)
        {
            yield break;
        }
    }
    public class sleepState : BaseState
    {
        public sleepState(enemy enemy, GameObject player) : base(enemy,player) { }

        public override void OnStateEnter()
        {           
        }
        public override void OnStateUpdate()
        {
        }
        public override void OnStateExit()
        {
        }
    }
    public class wakeState : BaseState
    {
        public wakeState(enemy enemy, GameObject player) : base(enemy, player) { }
        public override void OnStateEnter()
        {
        }
        public override void OnStateUpdate()
        {
        }
        public override void OnStateExit()
        {
        }
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
            if((curEnemy.transform.position.x - curPlayer.transform.position.x) < 0)
            {
                angle = 1;
            }
            else
            {
                angle = -1;
            }
            
            
            if (angle == 1)
            {
                curEnemy.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                curEnemy.transform.localEulerAngles = new Vector3(0, 180, 0);
            }

            //curEnemy.transform.position = Vector2.MoveTowards(curEnemy.transform.position, new Vector2 (curPlayer.transform.position.x, curEnemy.transform.position.y), curEnemy.monsterStat.moveSpeed * Time.deltaTime);
            curEnemy.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * angle * curEnemy.monsterStat.moveSpeed, ForceMode2D.Impulse);
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
            
            curEnemy.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ((curEnemy.gameObject.GetComponent<SpriteRenderer>().color.a) - 1 * Time.unscaledDeltaTime));
          
        }

        public override void OnStateExit()
        {
        }
    }



    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (this.curState != State.die)
    //    {
    //        if (col.gameObject.CompareTag("Player"))
    //        {
    //            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    //            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
    //            onTrigger = true;
    //            col.gameObject.GetComponent<PlayerStat>().damaged = monsterStat.enemyDamage;
    //            colPlayer = col.gameObject;
    //            StartCoroutine(WaitForDamage());
    //        }
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //       // this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    //       // this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
    //        onTrigger = false;
    //    }
    //}

    IEnumerator WaitForDamage()
    {
        while (onTrigger)
        {
            yield return new WaitForSeconds(1.0f);

            if (onTrigger == false)
            {
                yield break;
            }
            colPlayer.gameObject.GetComponent<PlayerStat>().damaged = monsterStat.enemyDamage;
        }
        if (onTrigger == false)
        {
            yield break;
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


    private void OnTriggerStay2D(Collider2D col)
    {



        if (this.curState != State.die && col.CompareTag("PlayerHitBox"))
        {
            player.GetComponent<PlayerStat>().damaged = monsterStat.enemyDamage;

        }
    }

}