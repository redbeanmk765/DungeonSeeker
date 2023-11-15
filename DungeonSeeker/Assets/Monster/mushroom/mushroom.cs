using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroom : enemy
{
    public GameObject enemy;
    public GameObject player;
    public GameObject colPlayer;
    public GameObject[] players;
    public GameObject enemyProjectileUp;
    public GameObject enemyProjectileDown;
    public GameObject enemyProjectileLeft;
    public GameObject enemyProjectileRight;
    public Animator animator;
    public bool onWake = false;
    public bool onTrigger = false;
    public bool attackMotionDone = true;
    public bool onFlash = false;
    public bool IsDie = false;
    public float nowHp;
    public float damaged;


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
        players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0];
        curState = State.attack;
        fsm = new FSM(new AttackState(this, player));

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
                   

                    ChangeState(State.attack);                  
                }
                break;
            case State.attack:
                if (!CanSeePlayer())
                {
                    
                    ChangeState(State.idle);
                }
                break;
            case State.die:
                if (this.gameObject.GetComponent<SpriteRenderer>().color.a <= 0)
                {
                    player.GetComponent<PlayerStat>().PlayerGold += monsterStat.enemyGold;
                    player.GetComponent<PlayerStat>().totalGold += monsterStat.enemyGold;
                    player.GetComponent<PlayerStat>().killScore++;
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
                animator.SetInteger("State", 0);
                break;
            case State.attack:
                fsm.ChangeState(new AttackState(this, player));
                animator.SetInteger("State", 1);
                break;
            case State.die:
                fsm.ChangeState(new DieState(this, player));
                animator.SetInteger("State", 2);
                break;
        }
    }

   

  

    
    public void AttackShoot()
    {
        enemyProjectileUp = Instantiate(monsterStat.projectile);
        enemyProjectileUp.transform.position = this.transform.position;
        enemyProjectileUp.gameObject.GetComponent<enemyProjectile>().pos = new Vector3 (0,1,0);
        enemyProjectileUp.gameObject.GetComponent<enemyProjectile>().dmg = monsterStat.enemyDamage;
        enemyProjectileUp.gameObject.GetComponent<enemyProjectile>().speed = monsterStat.projectileSpeed;

        enemyProjectileDown = Instantiate(monsterStat.projectile);
        enemyProjectileDown.transform.position = this.transform.position;
        enemyProjectileDown.gameObject.GetComponent<enemyProjectile>().pos = new Vector3(0, -1, 0);
        enemyProjectileDown.gameObject.GetComponent<enemyProjectile>().dmg = monsterStat.enemyDamage;
        enemyProjectileDown.gameObject.GetComponent<enemyProjectile>().speed = monsterStat.projectileSpeed;

        enemyProjectileLeft = Instantiate(monsterStat.projectile);
        enemyProjectileLeft.transform.position = this.transform.position;
        enemyProjectileLeft.gameObject.GetComponent<enemyProjectile>().pos = new Vector3(-1, 0, 0);
        enemyProjectileLeft.gameObject.GetComponent<enemyProjectile>().dmg = monsterStat.enemyDamage;
        enemyProjectileLeft.gameObject.GetComponent<enemyProjectile>().speed = monsterStat.projectileSpeed;

        enemyProjectileRight = Instantiate(monsterStat.projectile);
        enemyProjectileRight.transform.position = this.transform.position;
        enemyProjectileRight.gameObject.GetComponent<enemyProjectile>().pos = new Vector3(1, 0, 0);
        enemyProjectileRight.gameObject.GetComponent<enemyProjectile>().dmg = monsterStat.enemyDamage;
        enemyProjectileRight.gameObject.GetComponent<enemyProjectile>().speed = monsterStat.projectileSpeed;

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
    //        onTrigger = false;
    //    }
    //}

    //IEnumerator WaitForDamage()
    //{
    //    while (onTrigger)
    //    {
    //        yield return new WaitForSeconds(1.0f);

    //        if (onTrigger == false)
    //        {
    //            yield break;
    //        }
    //        colPlayer.gameObject.GetComponent<PlayerStat>().damaged = monsterStat.enemyDamage;
    //    }
    //    if (onTrigger == false)
    //    {
    //        yield break;
    //    }
    //}

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
    private bool CanSeePlayer()
    {
        if (Mathf.Abs(enemy.GetComponent<Transform>().position.x - player.GetComponent<Transform>().position.x) <= 10
            && Mathf.Abs(player.GetComponent<Transform>().position.y - enemy.GetComponent<Transform>().position.y) <= 6)
        {

            return true;

        }
        else

        return false;
        //  플레이어 탐지 구현
    }

}