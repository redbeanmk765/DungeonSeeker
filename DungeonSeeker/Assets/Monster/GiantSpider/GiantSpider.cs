using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiantSpider : enemy
{
    public GameObject enemy;
    public GameObject player;
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
    public List<int> coolTime;
    public int layermask;
    public int attackAngle;
    public bool IsDelay;
    public GameObject oneWay;
    public GameObject targetP;
    public int dmgCount;
    public bool IsCeiling;
    public bool IsFloor;
    public bool IsJump;
    public int IsLeft;
    public int IsDown;
    public bool IsLow;
    public GameObject BGController;
    public BossHpBar bossHpbar;
    public Canvas canvas;

    public AudioSource audioSource;
    public AudioClip[] clip;



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
        layermask = (1 << LayerMask.NameToLayer("Ground"));
        rigid = GetComponent<Rigidbody2D>();
        MaxSpeedX = 1f;
        player = GameObject.Find("Player");
        curState = State.idle;
        fsm = new FSM(new IdleState(this, player));

        nowHp = monsterStat.maxHp;
        animator = GetComponent<Animator>();

        IsDie = false;
        IsDelay = false;
        IsLow = false;
        IsLeft = 0;
        IsDown = 0;


        coolTime = new List<int> { 0, 0, 0, 0, 0 };
    }


    //private void Update()
    //{

    //    dmgCount = 0;
    //    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.000001f, 0);
    //    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.000001f, 0);
    //    UpdatePolCol(this.GetComponent<PolygonCollider2D>(), this.GetComponent<SpriteRenderer>().sprite);

    //    if (this.damaged != 0)
    //    {
    //        if (!onFlash)
    //        {
    //            nowHp -= this.damaged;
    //            onFlash = true;
    //            StartCoroutine(FlashWhite());
    //        }
    //        this.damaged = 0;
    //        dmgCount = 0;
    //        if (nowHp <= monsterStat.maxHp * 0.4f)
    //        {
    //            IsLow = true;
    //        }
    //    }
    //    if (nowHp <= 0)
    //    {
    //        if (IsDie == false)
    //        {
    //            this.curState = State.die;
    //            ChangeState(State.die);
    //            IsDie = true;
    //        }

    //    }

    //    float hpRatio = nowHp / monsterStat.maxHp;
    //    if (bossHpbar.On == true)
    //    {
    //        bossHpbar.image.fillAmount = Mathf.Lerp(bossHpbar.image.fillAmount, hpRatio, Time.deltaTime * 10);
    //    }

    //    switch (curState)
    //    {
    //        case State.idle:

    //            if (IsDelay == false)
    //            {
    //                if (CanAttackPlayer())
    //                {
    //                    ChangeState(State.attack);
    //                }
    //                else if (player.GetComponent<PlayerStat>().IsSafeZone == false)
    //                    ChangeState(State.move);
    //            }
    //            break;
    //        case State.move:


    //            if (CanAttackPlayer())
    //            {
    //                attackMotionDone = true;
    //                ChangeState(State.attack);
    //            }

    //            else
    //            {
    //                ChangeState(State.move);
    //            }
    //            break;
    //        case State.attack:
    //            if (attackMotionDone)
    //            {

    //                if (!CanAttackPlayer())
    //                {
    //                    ChangeState(State.move);
    //                }


    //                else
    //                {
    //                    ChangeState(State.idle);
    //                }
    //            }
    //            break;
    //        case State.die:
    //            if (this.gameObject.GetComponent<SpriteRenderer>().color.a <= 0)
    //            {
    //                player.GetComponent<PlayerStat>().PlayerGold += monsterStat.enemyGold;
    //                player.GetComponent<PlayerStat>().totalGold += monsterStat.enemyGold;
    //                player.GetComponent<PlayerStat>().killScore++;

    //                Destroy(this.gameObject);
    //            }
    //            break;

    //    }

    //    fsm.UpdateState();
    //}
    //private void FixedUpdate()
    //{
    //    if (rigid.velocity.x > MaxSpeedX)
    //    {
    //        rigid.velocity = new Vector2(MaxSpeedX, rigid.velocity.y);
    //    }

    //    else if (rigid.velocity.x < -1 * MaxSpeedX)
    //    {
    //        rigid.velocity = new Vector2(-1 * MaxSpeedX, rigid.velocity.y);
    //    }
    //}


    //IEnumerator FlashWhite()
    //{
    //    while (onFlash)
    //    {
    //        this.GetComponent<SpriteRenderer>().material = this.monsterStat.flashMaterial;
    //        yield return new WaitForSecondsRealtime(0.1f);
    //        this.GetComponent<SpriteRenderer>().material = this.monsterStat.originalMaterial;
    //        dmgCount = 0;

    //        if (onFlash == false)
    //        {
    //            this.GetComponent<SpriteRenderer>().material = this.monsterStat.originalMaterial;
    //            dmgCount = 0;
    //            yield break;

    //        }
    //        onFlash = false;


    //    }
    //    if (onFlash == false)
    //    {
    //        this.GetComponent<SpriteRenderer>().material = this.monsterStat.originalMaterial;
    //        dmgCount = 0;
    //        yield break;
    //    }
    //}



    //private void ChangeState(State nextState)
    //{
    //    curState = nextState;
    //    switch (curState)
    //    {
    //        case State.idle:
    //            fsm.ChangeState(new IdleState(this, player));
    //            animator.SetInteger("State", 1);
    //            break;
    //        case State.move:
    //            fsm.ChangeState(new MoveState(this, player));
    //            animator.SetInteger("State", 2);
    //            break;
    //        case State.attack:
    //            fsm.ChangeState(new AttackState(this, player));
    //            Pattern();
    //            break;
    //        case State.die:
    //            fsm.ChangeState(new DieState(this, player));
    //            animator.SetInteger("State", 12);
    //            break;
    //    }
    //}



    //private bool CanAttackPlayer()
    //{
    //    if (Mathf.Abs(enemy.GetComponent<Transform>().position.x - player.GetComponent<Transform>().position.x) <= 8)
    //    {

    //        return true;
    //    }
    //    else
    //        return false;
    //    //  »çÁ¤°Å¸® Ã¼Å© ±¸Çö
    //}

    //public void AttackDash1()
    //{
    //    attackAngle = turn();

    //    attackMotionDone = false;

    //    coolTime[0] = 2;

    //}
    //public void AttackDash2()
    //{
    //    wallHit.SetActive(true);
    //    isDash = true;
    //    MaxSpeedX = 7;

    //    animator.SetInteger("State", 4);
    //    audioSource.PlayOneShot(clip[0]);
    //    StartCoroutine(dash());
    //}
    //IEnumerator dash()
    //{
    //    while (isDash)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        if (this.curState != State.die)
    //        {
    //            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * attackAngle * this.monsterStat.moveSpeed, ForceMode2D.Impulse);
    //        }
    //        if (isDash == false)
    //        {
    //            yield break;
    //        }
    //        if (WallHit == true)
    //        {
    //            wallHit.SetActive(false);
    //            isDash = false;
    //            StartCoroutine(delay());
    //            ChangeState(State.idle);
    //            attackMotionDone = true;
    //            MaxSpeedX = 1;
    //            audioSource.Stop();
    //        }
    //    }
    //    if (isDash == false)
    //    {
    //        yield break;
    //    }
    //}

    //public void AttackShoot1()
    //{
    //    attackAngle = turn();
    //    attackMotionDone = false;
    //    coolTime[1] = 2;

    //}

    //public void AttackShoot2()
    //{
    //    audioSource.PlayOneShot(clip[3]);
    //    oneWay = Instantiate(monsterStat.projectile);
    //    oneWay.transform.position = this.transform.position;
    //    oneWay.gameObject.GetComponent<enemyProjectile>().pos = new Vector3(attackAngle, 0, 0);
    //    oneWay.gameObject.GetComponent<enemyProjectile>().rot = 180 * IsLeft;
    //    oneWay.gameObject.GetComponent<enemyProjectile>().dmg = monsterStat.enemyDamage;
    //    oneWay.gameObject.GetComponent<enemyProjectile>().speed = monsterStat.projectileSpeed;
    //}

    //public void AttackShoot3()
    //{
    //    StartCoroutine(delay());
    //    ChangeState(State.idle);
    //    attackMotionDone = true;
    //}

    //public void AttackJump1()
    //{
    //    attackAngle = turn();
    //    attackMotionDone = false;
    //    coolTime[3] = 2;

    //}

    //public void AttackJump2()
    //{
    //    gravity.force = new Vector2(0, 19.6f);
    //    audioSource.PlayOneShot(clip[1]);
    //    StartCoroutine(CeilingCheck());
    //}
    //public void AttackJump3()
    //{
    //    this.transform.localEulerAngles = new Vector3(180, 180 * IsLeft, 0);
    //    IsDown = 1;
    //}


    //public void AttackTgShoot1()
    //{
    //    attackAngle = turn();
    //}

    //public void AttackTgShoot2()
    //{
    //    audioSource.PlayOneShot(clip[3]);
    //    attackAngle = turn();
    //    targetP = Instantiate(monsterStat.projectile2);
    //    targetP.transform.position = this.transform.position;
    //    targetP.gameObject.GetComponent<targetEnemyProjectile>().target = player;
    //    targetP.gameObject.GetComponent<targetEnemyProjectile>().dmg = monsterStat.enemyDamage;
    //    targetP.gameObject.GetComponent<targetEnemyProjectile>().speed = monsterStat.projectileSpeed * 1.5f;
    //}

    //public void AttackTgShoot3()
    //{
    //    this.transform.localEulerAngles = new Vector3(0, 180 * IsLeft, 0);
    //    IsCeiling = false;
    //    IsDown = 0;
    //    gravity.force = new Vector2(0, -9.8f);
    //    attackMotionDone = true;
    //    StartCoroutine(delay());
    //    ChangeState(State.idle);
    //}

    //public void AttackCeiling1()
    //{
    //    attackAngle = turn();
    //    attackMotionDone = false;
    //    coolTime[2] = 2;
    //}

    //public void AttackCeiling2()
    //{
    //    audioSource.PlayOneShot(clip[1]);
    //    rigid.velocity = new Vector2(0, 6f);
    //    IsFloor = false;
    //    StartCoroutine(FloorCheck());
    //    BGController.GetComponent<BGcontroller>().state = 0;
    //}

    //public void AttackCeiling3()
    //{
    //    BGController.GetComponent<BGcontroller>().state = 1;
    //}

    //public void AttackCeiling4()
    //{
    //    BGController.GetComponent<BGcontroller>().state = 2;

    //}

    //public void AttackCeiling5()
    //{
    //    BGController.GetComponent<BGcontroller>().state = 0;

    //    StartCoroutine(delay());
    //}

    //public void AttackCeiling6()
    //{
    //    BGController.GetComponent<BGcontroller>().state = 1;
    //    attackMotionDone = true;
    //    ChangeState(State.idle);
    //}

    //IEnumerator FloorCheck()
    //{

    //    while (IsFloor == false)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        if (transform.position.y >= 11f && WallHit == false)
    //        {
    //            ceilingHit.SetActive(true);
    //            yield return new WaitForEndOfFrame();

    //        }


    //        if (WallHit == true && IsDie == false)
    //        {

    //            IsFloor = true;
    //            ceilingHit.SetActive(false);
    //            animator.SetInteger("State", 9);
    //            audioSource.PlayOneShot(clip[2]);
    //            yield break;
    //        }
    //    }

    //    if (IsFloor == true && IsDie == false)
    //    {
    //        ceilingHit.SetActive(false);
    //        animator.SetInteger("State", 9);
    //        audioSource.PlayOneShot(clip[2]);
    //        yield break;
    //    }

    //}
    //IEnumerator CeilingCheck()
    //{

    //    while (IsCeiling == false)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        IsJump = true;
    //        if (transform.position.y >= 11.4 && WallHit == false)
    //        {
    //            ceilingHit.SetActive(true);
    //            yield return new WaitForEndOfFrame();

    //        }


    //        if (WallHit == true && IsDie == false)
    //        {

    //            IsCeiling = true;
    //            ceilingHit.SetActive(false);
    //            animator.SetInteger("State", 8);
    //            audioSource.PlayOneShot(clip[2]);
    //            yield break;
    //        }
    //    }

    //    if (transform.position.y >= 12.4 && IsCeiling == true && IsDie == false)
    //    {
    //        ceilingHit.SetActive(false);
    //        animator.SetInteger("State", 8);
    //        audioSource.PlayOneShot(clip[2]);
    //        yield break;
    //    }

    //}
    //IEnumerator delay()
    //{
    //    for (int i = 0; i <= 4; i++)
    //    {
    //        coolTime[i]--;
    //    }
    //    IsDelay = true;
    //    yield return new WaitForSeconds(2);
    //    IsDelay = false;
    //}
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

    //public class MoveState : BaseState
    //{

    //    public MoveState(enemy enemy, GameObject player) : base(enemy, player) { }


    //    public override void OnStateEnter()
    //    {

    //    }

    //    public override void OnStateUpdate()
    //    {

    //        int angle = 1;
    //        if ((curEnemy.transform.position.x - curPlayer.transform.position.x) < 0)
    //        {
    //            angle = 1;
    //        }
    //        else
    //        {
    //            angle = -1;
    //        }

    //        curEnemy.GetComponent<slimeKing>().turn();
    //        //curEnemy.transform.position = Vector2.MoveTowards(curEnemy.transform.position, new Vector2 (curPlayer.transform.position.x, curEnemy.transform.position.y), curEnemy.monsterStat.moveSpeed * Time.deltaTime);
    //        curEnemy.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * angle * curEnemy.monsterStat.moveSpeed, ForceMode2D.Impulse);
    //    }

    //    public override void OnStateExit()
    //    {
    //    }
    //}

    //public class AttackState : BaseState
    //{
    //    public AttackState(enemy enemy, GameObject player) : base(enemy, player) { }


    //    public override void OnStateEnter()
    //    {
    //        curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity.y);
    //    }

    //    public override void OnStateUpdate()
    //    {

    //    }

    //    public override void OnStateExit()
    //    {
    //    }
    //}

    //public class DieState : BaseState
    //{
    //    public DieState(enemy enemy, GameObject player) : base(enemy, player) { }


    //    public override void OnStateEnter()
    //    {
    //        curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, curEnemy.gameObject.GetComponent<Rigidbody2D>().velocity.y);
    //    }

    //    public override void OnStateUpdate()
    //    {

    //        curEnemy.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ((curEnemy.gameObject.GetComponent<SpriteRenderer>().color.a) - 1 * Time.unscaledDeltaTime));

    //    }

    //    public override void OnStateExit()
    //    {
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
    //        player.gameObject.GetComponent<PlayerStat>().damaged = monsterStat.enemyDamage;
    //    }
    //    if (onTrigger == false)
    //    {
    //        yield break;
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D col)
    //{

    //    if (this.curState != State.die)
    //    {

    //        if (col.CompareTag("Attack"))
    //        {
    //            if (dmgCount == 0)
    //            {
    //                this.damaged += col.gameObject.GetComponent<HitBox>().Dmg;
    //                dmgCount++;
    //            }

    //        }
    //    }

    //    if (this.curState != State.die)
    //    {

    //        if (col.CompareTag("Ground"))
    //        {
    //            WallHit = true;
    //        }
    //    }

    //    if (this.curState != State.die && col.CompareTag("PlayerHitBox"))
    //    {
    //        player.GetComponent<PlayerStat>().damaged = monsterStat.enemyDamage;

    //    }
    //}

    //private void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.CompareTag("Ground"))
    //    {
    //        WallHit = false;
    //    }
    //}

    //public void UpdatePolCol(PolygonCollider2D collider, Sprite sprite)
    //{
    //    if (collider != null && sprite != null)
    //    {
    //        collider.pathCount = sprite.GetPhysicsShapeCount();
    //        List<Vector2> path = new List<Vector2>();
    //        for (int i = 0; i < collider.pathCount; i++)
    //        {
    //            path.Clear();
    //            sprite.GetPhysicsShape(i, path);
    //            collider.SetPath(i, path.ToArray());
    //        }
    //    }
    //}
    //public void Pattern()
    //{
    //    // 1 - µ¹Áø Äð 2, 2 - Á¤¸é ÅºÈ¯  Äð 2 , 3 - ÃµÀå ÅºÈ¯ Äð 2 4 - Ãß°Ý ÅºÈ¯  Äð 2 , 5 - ¹ß¾Ç ÆÐÅÏ 1È¸
    //    int i = 0;
    //    bool Okay = false;
    //    while (Okay == false)
    //    {
    //        i = Random.Range(0, 4);

    //        if (coolTime[i] <= 0)
    //        {
    //            if (i == 4)
    //            {
    //                if (IsLow == true)
    //                {
    //                    Okay = true;
    //                }
    //            }
    //            else
    //            {
    //                Okay = true;
    //            }
    //        }
    //    }

    //    switch (i)
    //    {
    //        case 0:
    //            animator.SetInteger("State", 3);
    //            break;

    //        case 1:
    //            animator.SetInteger("State", 5);
    //            break;

    //        case 2:
    //            animator.SetInteger("State", 6);
    //            break;

    //        case 3:
    //            animator.SetInteger("State", 7);
    //            break;

    //        case 4:
    //            animator.SetInteger("State", 10);
    //            break;

    //    }




    //}

    //public int turn()
    //{
    //    int angle = 1;
    //    if ((this.transform.position.x - player.transform.position.x) < 0)
    //    {
    //        angle = 1;
    //        IsLeft = 0;
    //    }
    //    else
    //    {
    //        angle = -1;
    //        IsLeft = 1;
    //    }


    //    if (angle == 1)
    //    {
    //        this.transform.localEulerAngles = new Vector3(180 * IsDown, 0, 0);
    //    }
    //    else
    //    {
    //        this.transform.localEulerAngles = new Vector3(180 * IsDown, 180, 0);
    //    }

    //    return angle;
    //}



    //private void OnTriggerStay2D(Collider2D col)
    //{
    //    if (this.curState != State.die && col.CompareTag("PlayerHitBox"))
    //    {
    //        player.GetComponent<PlayerStat>().damaged = monsterStat.enemyDamage;

    //    }
    //}

}
