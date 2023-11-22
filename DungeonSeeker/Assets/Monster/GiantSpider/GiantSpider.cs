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
    public bool attackMotionDone = true;
    public bool IsDie = false;
    public float nowHp;
    public float damaged;
    public List<int> coolTime;
    public bool IsDelay;
    public GameObject oneWay;
    public GameObject targetP;
    public int dmgCount;
    public bool IsLow;  
    public BossHpBar bossHpbar;
    public GameObject SpawnPoint;

    public AudioSource audioSource;
    public AudioClip[] clip;

    public List<SpriteRenderer> sprites;
    public int spritesCount;

    public GameObject ICL;
    public GameObject ICR;
    public GameObject ICW;

    private enum State
    {
        wait,
        idle,
        attack,
        die
    }

    private State curState;
    private FSM fsm;

    private void Start()
    {
        player = GameObject.Find("Player");
        curState = State.wait;
        fsm = new FSM(new WaitState(this, player));

        nowHp = monsterStat.maxHp;
        animator = GetComponent<Animator>();

        IsDie = false;
        IsDelay = false;
        IsLow = false;

        coolTime = new List<int> { 0, 0, 0, 0, 0 };

        spritesCount = sprites.Count;
    }


    private void Update()
    {

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.000001f, 0);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.000001f, 0);

        if (this.damaged != 0)
        {

            nowHp -= this.damaged;
        }
        this.damaged = 0;
        if (nowHp <= monsterStat.maxHp * 0.4f)
        {
            IsLow = true;
        }
        if (nowHp <= 0)
        {
            if (IsDie == false)
            {
                this.curState = State.die;
                //ChangeState(State.die);
                IsDie = true;
            }

        }

        float hpRatio = nowHp / monsterStat.maxHp;
        if (bossHpbar.On == true)
        {
            bossHpbar.image.fillAmount = Mathf.Lerp(bossHpbar.image.fillAmount, hpRatio, Time.deltaTime * 10);
        }

        switch (curState)
        {
            case State.wait:

                if (player.GetComponent<PlayerStat>().IsSafeZone == false)
                {
                    animator.SetInteger("State", 1);
                }
                break;

            case State.idle:

                if (IsDelay == false)
                {             
                        ChangeState(State.attack);
                }
                break;

            case State.attack:
                if (attackMotionDone)
                {
                    ChangeState(State.idle);
                }
                break;
            case State.die:
                for (int i = 0; i < spritesCount; i++)
                {
                    sprites[i].color = new Color(1, 1, 1, ((sprites[i].color.a) - 1 * Time.unscaledDeltaTime));
                    if (sprites[i].color.a <= 0)
                    {
                        Destroy(this.gameObject);
                    }
                }
                break;

        }

        fsm.UpdateState();
    }
    private void FixedUpdate()
    {

    }




    private void ChangeState(State nextState)
    {
        curState = nextState;
        switch (curState)
        {
            case State.idle:
                fsm.ChangeState(new IdleState(this, player));
                animator.SetInteger("State", 2);
                break;
            case State.attack:
                fsm.ChangeState(new AttackState(this, player));
                Pattern();
                break;
            case State.die:
                fsm.ChangeState(new DieState(this, player));
                audioSource.PlayOneShot(clip[0]);
                animator.SetInteger("State", 12);
                break;
        }
    }

    public void GoIdle()
    {
        ChangeState(State.idle); ;
    }

    public void IdleFin()
    {
        IsDelay = false;
    }
    public void Spawn()
    {
        SpawnPoint.SetActive(true);
        audioSource.PlayOneShot(clip[2]);

    }

    public void DeSpawn()
    {
        SpawnPoint.SetActive(false);

    }

    public void MagicCircle()
    {
        GameObject tmp;
        tmp = Instantiate(monsterStat.projectile);
        tmp.transform.position = new Vector3(4, 48, 0);
    }

    public void IceWall1()
    {
        ICL.SetActive(true);
        ICR.SetActive(true);
        audioSource.PlayOneShot(clip[3]);
    }
    public void IceWall2()
    {
        ICW.SetActive(true);
    }

    public void IceWall3()
    {
        ICW.transform.position += new Vector3(0, 0.25f, 0);
    }

    public void IceWall4()
    {
        ICL.SetActive(false);
        ICR.SetActive(false);
        ICW.transform.position += new Vector3(0, -1f, 0);
        ICW.SetActive(false);
    }


    public void Delay()
    {
        IsDelay = true;
       
    }

    public void SlamSound()
    {
        audioSource.PlayOneShot(clip[1]);

    }


    public void SlashSound()
    {
        audioSource.PlayOneShot(clip[2]);

    }

    public void MotionDone()
    {
        attackMotionDone = true;
    }

    public class WaitState : BaseState
    {
        public WaitState(enemy enemy, GameObject player) : base(enemy, player) { }
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

        }

        public override void OnStateExit()
        {
        }
    }


    public void Pattern()
    {
      
        int i = 0;
        bool Okay = false;
        while (Okay == false)
        {
            i = Random.Range(0, 5);

            if (coolTime[i] <= 0)
            {   
                    Okay = true;
            }
        }
        for (int tmp = 0; tmp <= 4; tmp++)
        {
            coolTime[tmp]--;
        }
        switch (i)
        {
            
            case 0:
                int j = Random.Range(0, 2);
                if (j == 0)
                {
                    animator.SetInteger("State", 3);
                    IsDelay = true;
                    attackMotionDone = false;
                    coolTime[i] = 3;
                }
                else
                {
                    animator.SetInteger("State", 4);
                    IsDelay = true;
                    attackMotionDone = false;
                    coolTime[i] = 3;
                }
                break;

            case 1:
                animator.SetInteger("State", 5);
                IsDelay = true;
                attackMotionDone = false;
                coolTime[i] = 3;
                break;

            case 2:
                int k = Random.Range(0, 2);
                if (k == 0)
                {
                    animator.SetInteger("State", 6);
                    IsDelay = true;
                    attackMotionDone = false;
                    coolTime[i] = 3;
                }
                else
                {
                    animator.SetInteger("State", 7);
                    IsDelay = true;
                    attackMotionDone = false;
                    coolTime[i] = 3;
                }
                break;

            case 3:
                animator.SetInteger("State", 8);
                IsDelay = true;
                attackMotionDone = false;
                coolTime[i] = 5;
                break;

            case 4:
                animator.SetInteger("State", 9);
                IsDelay = true;
                attackMotionDone = false;
                coolTime[i] = 3;
                break;

        }




    }
}
