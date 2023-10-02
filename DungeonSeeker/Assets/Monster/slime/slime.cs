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
    public int damaged;
    public Vector3 MoveTowardsVector;

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
        players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0];
        curState = State.sleep;
        fsm = new FSM(new sleepState(this,player));

        nowHp = monsterStat.maxHp;
        animator = GetComponent<Animator>();

        onFlash = false;
        IsDie = false;
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
                    Destroy(this.gameObject);
                }
                break;

        }

        fsm.UpdateState();
    }

    IEnumerator FlashWhite()
    {
        while (onFlash)
        {
            this.GetComponent<SpriteRenderer>().material = this.monsterStat.flashMaterial;
            yield return new WaitForSeconds(0.1f);
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
        if (Vector2.Distance(enemy.GetComponent<Transform>().position, player.GetComponent<Transform>().position) <= 10)
        {

            return true;
            
        }
        else
            return false;
        //  플레이어 탐지 구현
    }

    private bool CanAttackPlayer()
    {
        if (Vector2.Distance(enemy.GetComponent<Transform>().position, player.GetComponent<Transform>().position) <= 2.3)
        {

            return true;
        }
        else
            return false;
        //  사정거리 체크 구현
    }

    public void AttackDash1()
    {
        targetPos = player.transform.position;
        MoveTowardsVector = Vector3.Normalize(targetPos - this.transform.position);
        //MoveTowardsVector = MoveTowardsVector * 1f;

        float angle = Mathf.Atan2(player.transform.position.y - this.transform.position.y, player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg;

        if (angle >= -90 && angle < 90)
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

        float angle = Mathf.Atan2(targetPos.y - this.transform.position.y, targetPos.x - this.transform.position.x) * Mathf.Rad2Deg;

        if (angle >= -90 && angle < 90)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        isDash = true;
        StartCoroutine(dash());
    } 
    public void AttackDash4()
    {
        isDash = false;
        attackMotionDone = true;
    }
    IEnumerator dash()
    {
        while (isDash)
        {            
             yield return new WaitForEndOfFrame();
            // this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, 0.05f);
            if (this.curState != State.die)
            {
                transform.position += MoveTowardsVector * 10f * Time.deltaTime;
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
            
            float angle = Mathf.Atan2(curPlayer.transform.position.y - curEnemy.transform.position.y, curPlayer.transform.position.x - curEnemy.transform.position.x) * Mathf.Rad2Deg;
            
            if (angle >= -90 && angle < 90)
            {
                curEnemy.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                curEnemy.transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            curEnemy.transform.position = Vector3.MoveTowards(curEnemy.transform.position, curPlayer.transform.position, curEnemy.monsterStat.moveSpeed * Time.deltaTime);
            
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

        }

        public override void OnStateUpdate()
        {
            
            curEnemy.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ((curEnemy.gameObject.GetComponent<SpriteRenderer>().color.a) - 1 * Time.deltaTime));
          
        }

        public override void OnStateExit()
        {
        }
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        if (this.curState != State.die)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                onTrigger = true;
                col.gameObject.GetComponent<playerStat>().damaged = monsterStat.damage;
                colPlayer = col.gameObject;
                StartCoroutine(WaitForDamage());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
           // this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
           // this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            onTrigger = false;
        }
    }

    IEnumerator WaitForDamage()
    {
        while (onTrigger)
        {
            yield return new WaitForSeconds(1.0f);

            if (onTrigger == false)
            {
                yield break;
            }
            colPlayer.gameObject.GetComponent<playerStat>().damaged = monsterStat.damage;
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
            
            if (col.CompareTag("attack"))
            {
               
                this.damaged += col.gameObject.GetComponent<weaponStat>().dmg;

            }
        }
    }

    
}