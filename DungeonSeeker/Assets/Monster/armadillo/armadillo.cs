using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armadillo : enemy
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
    public Vector3 rotation;
    public float nowHp;
    public float damaged;
    public Vector3 MoveTowardsVector;
    public float maxSpeedX;
    public Rigidbody2D rigid;
    public int angle;
    public bool WallHit;
    public bool attackCoolTime;
    public int layermask;
    public int IsLeft;
    public bool IsRoll;
    public bool RollStart;

    public AudioSource audioSource;
    public AudioClip[] clip;

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
        layermask = (1 << LayerMask.NameToLayer("Ground"));
        rigid = GetComponent<Rigidbody2D>();
        rotation = this.transform.localEulerAngles;
        player = GameObject.Find("Player");
        curState = State.idle;
        fsm = new FSM(new IdleState(this, player));

        nowHp = monsterStat.maxHp;
        animator = GetComponent<Animator>();

        onFlash = false;
        IsDie = false;
        maxSpeedX = 6;

        Physics2D.IgnoreCollision(this.GetComponent<PolygonCollider2D>(), player.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(this.GetComponent<PolygonCollider2D>(), player.GetComponent<EdgeCollider2D>(), true);

    }


    private void Update()
    {
        UpdatePolCol(this.GetComponent<PolygonCollider2D>(), this.GetComponent<SpriteRenderer>().sprite);
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

        if(RollStart == true)
        {
            RollStart = false;
            ChangeState(State.attack);
        }


        switch (curState)
        {
            case State.idle:

                    if (CanAttackPlayer())
                    {
                        ChangeState(State.attack);
                    }
                
                break;

            case State.attack:

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
    private void FixedUpdate()
    {
        if (rigid.velocity.x > maxSpeedX)
        {
            rigid.velocity = new Vector2(maxSpeedX, rigid.velocity.y);
        }

        else if (rigid.velocity.x < -1 * maxSpeedX)
        {
            rigid.velocity = new Vector2(-1 * maxSpeedX, rigid.velocity.y);
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
                break;
            case State.attack:
                fsm.ChangeState(new AttackState(this, player));
                animator.SetInteger("State", 1);
                break;
            case State.die:
                fsm.ChangeState(new DieState(this, player));
                animator.SetInteger("State", 3);
                //audioSource.PlayOneShot(clip[1]);
                break;
        }
    }


    private bool CanAttackPlayer()
    {
        if (Mathf.Abs(enemy.GetComponent<Transform>().position.x - player.GetComponent<Transform>().position.x) < 20
            && player.GetComponent<Transform>().position.y - enemy.GetComponent<Transform>().position.y <= 2
            && player.GetComponent<Transform>().position.y - enemy.GetComponent<Transform>().position.y >= -7)
        {
            return true;
        }
  
            return false;
        //  사정거리 체크 구현
    }
    public void Jump()
    {
        rigid.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
    }
    public void Roll()
    {
        IsRoll = true;
        animator.SetInteger("State", 2);
    }

    public void Turn()
    {
        IsLeft = 1;
        this.transform.localEulerAngles = new Vector3(0, 180 * IsLeft, 0);
        IsLeft = 1;
    }
    public void UpdatePolCol(PolygonCollider2D collider, Sprite sprite)
    {
        if (collider != null && sprite != null)
        {
            collider.pathCount = sprite.GetPhysicsShapeCount();
            List<Vector2> path = new List<Vector2>();
            for (int i = 0; i < collider.pathCount; i++)
            {
                path.Clear();
                sprite.GetPhysicsShape(i, path);
                collider.SetPath(i, path.ToArray());
            }
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

 
    public class AttackState : BaseState
    {
        public AttackState(enemy enemy, GameObject player) : base(enemy, player) { }


        public override void OnStateEnter()
        {

        }

        public override void OnStateUpdate()
        {
            if (curEnemy.gameObject.GetComponent<armadillo>().IsRoll == true)
            {
                int angle = 1;
                if ((curEnemy.gameObject.transform.localEulerAngles.y == 0))
                {
                    angle = 1;
                }
                else
                {
                    angle = -1;
                }

                curEnemy.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * angle * curEnemy.monsterStat.moveSpeed, ForceMode2D.Impulse); 
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
            curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        public override void OnStateUpdate()
        {

            curEnemy.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ((curEnemy.gameObject.GetComponent<SpriteRenderer>().color.a) - 1 * Time.unscaledDeltaTime));

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

        if (this.curState != State.die)
        {

            if (col.CompareTag("Ground"))
            {
                WallHit = true;

                
                rigid.velocity = new Vector2(0, 0);

                if(IsLeft == 0)
                {
                    IsLeft = 1;
                }
                else
                {
                    IsLeft = 0;
                }

                this.transform.localEulerAngles = new Vector3(0, 180 * IsLeft, 0);
            }
        }

        if (this.curState != State.die && col.CompareTag("PlayerHitBox"))
        {
            player.GetComponent<PlayerStat>().damaged = monsterStat.enemyDamage;

        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Ground")
        {
            rigid.velocity = new Vector2(0, 0);

            if (IsLeft == 0)
            {
                IsLeft = 1;
            }
            else
            {
                IsLeft = 0;
            }

            this.transform.localEulerAngles = new Vector3(0, 180 * IsLeft, 0);
        }

    }
        private void OnTriggerStay2D(Collider2D col)
    {



        if (this.curState != State.die && col.CompareTag("PlayerHitBox"))
        {
            player.GetComponent<PlayerStat>().damaged = monsterStat.enemyDamage;

        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            WallHit = false;
        }
    }
}
