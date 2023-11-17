using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    public float MaxSpeedX;
    public float MaxSpeedY;
    public Rigidbody2D rigid;
    public Animator Animator;
    public float Hor;
    public float Ver;
    public float LastHor;
    public float AirJumpCount;
    public bool IsGround;
    public float GroundDistance;
    public float GroundScale;
    public LayerMask LayerMask;
    public ConstantForce2D Gravity;
    public bool IsWall;
    public bool IsWallJump;
    public bool IsDash;
    public bool IsFall;
    public bool IsDashReady;
    public float DashCooltime;
    public bool IsAttackCooltime;
    public float AttackCooltime;
    public bool IsAttack;
    public bool IsAttack2;
    public bool callAttack2;
    public bool readyAttack;
    public bool IsWallAttack;
    public bool IsJumpAttack;
    public bool IsUpAttack;
    public float dashDirection;
    public GameObject HitBox;
    public GameObject WallHitBox;
    public GameObject UpHitBox;
    public GameObject DownHitBox;
    public float TM;
    public bool IsFade;
    public bool IsSkillCoolTime;
    public bool IsSkillMotion;
    public PlayerStat playerStat;
    public Image Timer;
    public SoundController sound;
    public GameObject TimerSound;
    // Start is called before the first frame update
    void Start()
    {
        TM = 1;
        MaxSpeedX = 5 * TM;
        MaxSpeedY = 10 * TM;
        rigid = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        GroundDistance = GetComponent<BoxCollider2D>().bounds.extents.y + 0.1f;
        GroundScale = GetComponent<BoxCollider2D>().bounds.extents.x + 0.1f;

        Gravity = this.GetComponent<ConstantForce2D>();
        Gravity.force = new Vector2(0, -9.8f * TM );

        IsDash = false;
        IsDashReady = true;
        IsFall = false;
        IsAttack = false;
        IsAttack2 = false;
        readyAttack = false;
        callAttack2 = false;
        IsWallAttack = false;
        IsJumpAttack = false;
        LastHor = 1;
        DashCooltime = 2f;
        AttackCooltime = 0.2f;
        HitBox = this.transform.Find("HitBox").gameObject;
        HitBox.SetActive(false);
        WallHitBox = this.transform.Find("WallHitBox").gameObject;
        WallHitBox.SetActive(false);
        UpHitBox = this.transform.Find("UpHitBox").gameObject;
        UpHitBox.SetActive(false);
        DownHitBox = this.transform.Find("DownHitBox").gameObject;
        DownHitBox.SetActive(false);
        IsFade = false;
        IsSkillMotion = false;
        playerStat = this.GetComponent<PlayerStat>();
        Timer = GameObject.Find("Timer").GetComponent<Image>();

        //Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GetComponentsInChildren<BoxCollider2D>()[1]);


    }
       

// Update is called once per frame
    void Update()
    {
        if (IsSkillMotion == true && IsDash == false)
        {
            Animator.SetInteger("State", 18);
        }
        HitBoxUpdate();
        if (IsFade == false)
        {
            if (Hor != 0 && IsWallJump == false && IsDash == false)
            {
                LastHor = Hor;
            }

            if (IsDash == false && IsWallJump == false)
            {
                Hor = Input.GetAxisRaw("Horizontal");
                Ver = Input.GetAxisRaw("Vertical");
            }
            Input.GetAxisRaw("Horizontal");

            if (Hor == 1 && Hor != LastHor && IsAttack == false && IsAttack2 == false && IsJumpAttack == false)
            {
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else if (Hor == -1 && Hor != LastHor && IsAttack == false && IsAttack2 == false && IsJumpAttack == false)
            {
                this.transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            if (rigid.velocity.x > 0.1 && IsJumpAttack == false)

            {
                this.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else if (rigid.velocity.x < -0.1 && IsJumpAttack == false)
            {
                this.transform.localEulerAngles = new Vector3(0, 180, 0);
            }

            if ((Input.GetButtonUp("Horizontal") || Hor == 0) && IsWallJump == false && IsDash == false)
            {
                rigid.velocity = new Vector2(0, rigid.velocity.y);
            }



            RaycastHit2D GroundHit = Physics2D.BoxCast(transform.position, new Vector2(GroundScale + 0.1f, 0.01f), 0, Vector2.down, GroundDistance, LayerMask);

            if (GroundHit != false)
            {

                if (GroundHit.transform.CompareTag("Ground"))
                {
                    AirJumpCount = playerStat.AirJumpCountMax;
                    IsGround = true;
                    IsFall = false;
                }

            }
            else
            {
                IsGround = false;
            }

            if (IsGround == false)
            {
                if (rigid.velocity.y < 0)
                {
                    IsFall = true;
                }
                else
                {
                    IsFall = false;
                }
            }

            RaycastHit2D WallHit = Physics2D.BoxCast(transform.position, new Vector2(0.01f, 1.70f), 0, Vector2.right * Hor, 0.4f, LayerMask);
            if (WallHit != false && IsDash == false)
            {
                IsWall = true;
                AirJumpCount = playerStat.AirJumpCountMax;

            }
            else
            {
                IsWall = false;
            }


            if (Input.GetButtonDown("Jump") && IsDash == false)
            {

                if (AirJumpCount > 0)
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, 6f * TM);
                    sound.Jump();
                    if (IsGround == false && IsWall == false)
                    {
                        AirJumpCount--;
                    }

                    
                }

                if (IsWall == true)
                {
                    rigid.AddForce(new Vector2(-1 * Hor * 5f * TM, 6f * TM), ForceMode2D.Impulse);
                    //sound.Jump();
                    StartCoroutine(WallJump());
                }
            }

            if (Input.GetButton("Horizontal") && (WallHit != false) && IsDash == false && IsAttack == false && IsAttack2 == false && IsJumpAttack == false)
            {
                //rigid.velocity = new Vector2(0, 0);
                //if (IsWallAttack == false) 
                //{ 
                //    HitBox.SetActive(false);
                //}
                //UpHitBox.SetActive(false);
                //DownHitBox.SetActive(false);
                //IsAttack = false;
                //IsAttack2 = false;
                //IsJumpAttack = false;
                //IsUpAttack = false;
                //readyAttack = false;
                MaxSpeedY = 2f * TM;
                LastHor = LastHor * -1;
                Gravity.force = new Vector2(0, -20f * TM * TM);

                if (Hor == 1)
                {
                    this.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    this.transform.localEulerAngles = new Vector3(0, 0, 0);
                }

            }
            else
            {
                if (IsDash == false)
                {
                    if (TM == 1)
                    {
                        Gravity.force = new Vector2(0, -9.8f);
                    }
                    else
                    {
                        Gravity.force = new Vector2(0, -9.8f * TM * TM);
                    }
                }
                MaxSpeedY = 10 * TM;

            }

            if (Input.GetButtonDown("Dash") && IsDashReady == true)
            {
                if (IsDash == false && IsWallJump == false)
                {
                    IsDash = true;
                    Hor = 0;
                    dashDirection = LastHor;
                    sound.Dash();
                    StartCoroutine(Dash());
                    StartCoroutine(DashCoolTime());
                    StartCoroutine(DashCoolTimeBar());
                    this.gameObject.transform.Find("DashCooltime").gameObject.SetActive(true);
                }

            }
            //if (Input.GetButtonDown("Attack") && IsDash == false && readyAttack == true && IsAttack2 == false)
            //{
            //    IsAttack2 = true;
            //   Debug.Log("test");
            //    //StartCoroutine(Attack2());
            //}
            if (Input.GetButtonDown("Attack") && IsDash == false && IsAttack == false && IsAttack2 == false && IsAttackCooltime == false)
            {
                if (IsGround == true)
                {
                    if (readyAttack == false)
                    {
                        IsAttack = true;
                    }
                    else
                    {
                        IsAttack2 = true;
                    }


                    //StartCoroutine(Attack());

                }

                else if (IsWall == true)
                {
                    IsWallAttack = true;
                }

                else if (IsWall == false && IsGround == false)
                {
                    IsJumpAttack = true;
                }
            }



            if (Hor == 0 && IsGround == true && IsDash == false)
            {
                if (IsJumpAttack)
                {
                    IsJumpAttack = false;
                    IsWallAttack = false;
                    HitBox.SetActive(false);
                    WallHitBox.SetActive(false);
                    UpHitBox.SetActive(false);
                    DownHitBox.SetActive(false);
                }
                Animator.SetInteger("State", 1);
            }

            if (Hor != 0 && IsGround == true && IsDash == false)
            {
                if (IsJumpAttack)
                {
                    IsJumpAttack = false;
                    IsWallAttack = false;
                    HitBox.SetActive(false);
                    WallHitBox.SetActive(false);
                    UpHitBox.SetActive(false);
                    DownHitBox.SetActive(false);
                }
                Animator.SetInteger("State", 2);
            }

            if (IsDash == true)
            {
                Animator.SetInteger("State", 3);
            }

            if (IsGround == false && IsFall == false && IsDash == false && IsJumpAttack == false)
            {
                Animator.SetInteger("State", 4);
            }

            if (IsGround == false && IsFall == true && IsDash == false && IsJumpAttack == false)
            {
                Animator.SetInteger("State", 5);
            }

            if (IsWall == true)
            {
                Animator.SetInteger("State", 6);
            }

            if (IsWallJump == true)
            {
                Animator.SetInteger("State", 4);
            }

            if (IsAttack == true && IsAttack2 == false)
            {
                if (Ver == 1 || IsUpAttack == true)
                {
                    Animator.SetInteger("State", 13);
                }
                else
                {
                    Animator.SetInteger("State", 7);
                }
            }

            if (IsAttack2 == true)
            {
                Animator.SetInteger("State", 11);
            }

           

            if (IsWallAttack == true)
            {
                Animator.SetInteger("State", 12);
            }

            if (IsJumpAttack == true && IsFall == false)
            {
                if (Ver == 1 || IsUpAttack == true)
                {
                    Animator.SetInteger("State", 14);
                }
                else if (Ver == -1 || IsUpAttack == true)
                {
                    Animator.SetInteger("State", 15);
                }
                else
                {
                    Animator.SetInteger("State", 8);
                }
            }

            if (IsJumpAttack == true && IsFall == true)
            {
                if (Ver == 1 || IsUpAttack == true)
                {
                    Animator.SetInteger("State", 16);
                }
                else if (Ver == -1 || IsUpAttack == true)
                {
                    Animator.SetInteger("State", 17);
                }
                else
                {
                    Animator.SetInteger("State", 9);
                }
            }

            if (Input.GetButtonDown("Skill1") && IsDash == false && IsSkillCoolTime == false && TM == 1 && IsAttack == false && IsAttack2 == false)
            {
                rigid.velocity = new Vector3(0, 0, 0);
                TM = 1000;
                Time.timeScale = 1f / TM;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                HitBox.SetActive(false);
                WallHitBox.SetActive(false);
                UpHitBox.SetActive(false);
                DownHitBox.SetActive(false);
                IsAttack = false;
                IsAttack2 = false;
                IsJumpAttack = false;
                IsWallAttack = false;
                IsUpAttack = false;
                readyAttack = false;
                IsFade = true;
                IsSkillMotion = true;
                Animator.SetInteger("State", 18);
                GameObject.Find("Main Camera").GetComponent<CameraController>().IsStop = true;

            }
        }

    }

    void FixedUpdate()
    {
        if(IsFade == true)
        {
            rigid.velocity = new Vector2(0, 0);
            Hor = 0;
            Animator.SetInteger("State", 1);
        }

        if (IsWallJump == false && IsAttack == false && IsAttack2 == false)
        {
            rigid.AddForce(Vector2.right * Hor * MaxSpeedX * TM, ForceMode2D.Impulse);
        }

        if (rigid.velocity.x > MaxSpeedX)
        {
            rigid.velocity = new Vector2(MaxSpeedX, rigid.velocity.y);
        }

        else if (rigid.velocity.x < -1 * MaxSpeedX)
        {
            rigid.velocity = new Vector2(-1 * MaxSpeedX, rigid.velocity.y);
        }

        if (rigid.velocity.y > MaxSpeedY)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, MaxSpeedY);
        }

        else if (rigid.velocity.y < -1 * MaxSpeedY)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -1 * MaxSpeedY);
        }

    }
    public void Attack1()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        
    }

    public void Attack2()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        HitBox.SetActive(true);

    }

    public void Attack3()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        HitBox.SetActive(false);
        IsAttack = false;
        StartCoroutine(ReadyAttack2());
        StartCoroutine(AttackCoolTime());
    }

    public void Attack4()
    {
        HitBox.SetActive(false);
        WallHitBox.SetActive(false);
        IsAttack = false;
        IsAttack2 = false;
        IsJumpAttack = false;
        IsWallAttack = false;
        readyAttack = false;
        StartCoroutine(AttackCoolTime());

    }

    public void WallAttack()
    {
        WallHitBox.SetActive(true);
    }

    public void JumpAttack()
    {
        HitBox.SetActive(true);
    }
    public void UpAttack1()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        IsUpAttack = true;

    }
    public void UpAttack2()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        UpHitBox.SetActive(true);
    }

    public void UpAttack3()
    {
        UpHitBox.SetActive(false);
        DownHitBox.SetActive(false);
        IsAttack = false;
        IsUpAttack = false;
        IsJumpAttack = false;
        StartCoroutine(AttackCoolTime());
    }

    public void JumpUpAttack1()
    {
        UpHitBox.SetActive(true);
    }

    public void JumpDownAttack1()
    {
        DownHitBox.SetActive(true);
    }
    
    public void TheWorld1()
    {
        GameObject.Find("Main Camera").GetComponent<CameraController>().IsStop = false;
        StartCoroutine(TimerBar());
        StartCoroutine(TheWorld());
        IsFade = false;
        IsSkillMotion = false;
    }

    public void HitBoxUpdate()
    {
        HitBox.transform.localPosition = new Vector3(1 + (0.07f * playerStat.hitBox), 0, 0);
        HitBox.transform.localScale = new Vector3(0.7f+ (0.1f * playerStat.hitBox), 1, 1);
        WallHitBox.transform.localPosition = new Vector3(1 + (0.07f * playerStat.hitBox), 0, 0);
        WallHitBox.transform.localScale = new Vector3(0.7f + (0.1f * playerStat.hitBox), 1, 1);
        UpHitBox.transform.localPosition = new Vector3(0, 1 + (0.07f * playerStat.hitBox), 0);
        UpHitBox.transform.localScale = new Vector3(0.7f + (0.1f * playerStat.hitBox), 1, 1);
        DownHitBox.transform.localPosition = new Vector3(0, -(1 + (0.07f * playerStat.hitBox)), 0);
        DownHitBox.transform.localScale = new Vector3(0.7f + (0.1f * playerStat.hitBox), 1, 1);
    }

    IEnumerator WallJump()
    {
        IsWallJump = true;
        Hor = LastHor * -1;
        yield return new WaitForSecondsRealtime(0.05f);
        rigid.velocity = new Vector2(rigid.velocity.x, 6f * TM);
        yield return new WaitForSecondsRealtime(0.1f);
        IsWallJump = false;
    }

    IEnumerator Dash()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        HitBox.SetActive(false);
        WallHitBox.SetActive(false);
        UpHitBox.SetActive(false);
        DownHitBox.SetActive(false);
        IsAttack = false;
        IsAttack2 = false;
        IsJumpAttack = false;
        IsWallAttack = false;
        IsUpAttack = false;
        readyAttack = false;
        IsDash = true;
        MaxSpeedX = 12 * TM;

        rigid.velocity = new Vector2(dashDirection * 20f * TM, 0);        
        Gravity.force = new Vector2(0, 0);
        yield return new WaitForSecondsRealtime(0.35f);
        Gravity.force = new Vector2(0, -9.8F);
        MaxSpeedX = 5 * TM;
        IsDash = false;
    }

    IEnumerator DashCoolTimeBar()
    {
        float timer = TM;
        float coolTime = 0;
        float coolTImeRatio = 1;
        while (coolTime <= playerStat.dashCoolTime)
        {
            coolTime += Time.unscaledDeltaTime ;
            coolTImeRatio = 1 - (coolTime / playerStat.dashCoolTime);
            this.gameObject.transform.Find("DashCooltime").GetComponent<RectTransform>().localScale = new Vector3(coolTImeRatio * 0.7f, 0.05f, 0);
            yield return new WaitForFixedUpdate();
        }
        this.gameObject.transform.Find("DashCooltime").gameObject.SetActive(false);
    }

    IEnumerator TimerBar()
    {
        float timer = TM;
        float coolTime = 0;
        float coolTImeRatio = 1;
        while (coolTime <= playerStat.skillduration)
        {
            coolTime += Time.unscaledDeltaTime;
            coolTImeRatio = (coolTime / playerStat.skillduration);
            Timer.fillAmount = Mathf.Lerp(Timer.fillAmount, coolTImeRatio, Time.deltaTime * TM * 100f);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator TimerCoolTimeBar()
    {
        float timer = TM;
        float coolTime = 0;
        float coolTImeRatio = 1;
        while (coolTime <= playerStat.skillCoolTime)
        {
            coolTime += Time.unscaledDeltaTime;
            coolTImeRatio = 1 - (coolTime / playerStat.skillCoolTime);
            Timer.fillAmount = Mathf.Lerp(Timer.fillAmount, coolTImeRatio, Time.deltaTime * TM * 100f);
            yield return new WaitForFixedUpdate();
        }
    }


    IEnumerator DashCoolTime() 
    { 
        IsDashReady = false;
        yield return new WaitForSecondsRealtime(playerStat.dashCoolTime);
        IsDashReady = true;
    }

    IEnumerator AttackCoolTime()
    {
        IsAttackCooltime = true;
        yield return new WaitForSecondsRealtime(playerStat.AttackCoolTime);
        IsAttackCooltime = false;
    }

    IEnumerator TheWorld()
    {
        HitBox.SetActive(false);
        WallHitBox.SetActive(false);
        UpHitBox.SetActive(false);
        DownHitBox.SetActive(false);
        IsAttack = false;
        IsAttack2 = false;
        IsJumpAttack = false;
        IsWallAttack = false;
        IsUpAttack = false;
        readyAttack = false;

        GameObject.Find("Main Camera").GetComponent<FadeController>().TheWorldOn();
        TM = 10;

        TimerSound.SetActive(true);

        Time.timeScale = 1f / TM;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Gravity.force = new Vector2(0, -9.8f * TM);


        MaxSpeedX = 5 * TM;
        MaxSpeedY = 10 * TM;

        yield return new WaitForSecondsRealtime(playerStat.skillduration);

        TM = 1;

        Time.timeScale = 1f / TM;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Gravity.force = new Vector2(0, -9.8f * TM);

        TimerSound.SetActive(false);

        MaxSpeedX = 5 * TM;
        MaxSpeedY = 10 * TM;

        StartCoroutine(TheWorldCoolTime());
        GameObject.Find("Main Camera").GetComponent<FadeController>().TheWorldOff();
        Vector3 tmp = rigid.velocity;
        rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y * 0.1f);
        //yield return new WaitForSecondsRealtime(0.2f);
        //rigid.velocity = tmp;
        yield return 0;
    }

    IEnumerator TheWorldCoolTime()
    {
        StartCoroutine(TimerCoolTimeBar());
        IsSkillCoolTime = true;
        yield return new WaitForSecondsRealtime(playerStat.skillCoolTime);
        IsSkillCoolTime = false;

    }


        //IEnumerator DashCooltime()
        //{
        //    float coolTime = 0;
        //    float coolTImeRatio = 1;
        //    while (coolTime <= this.gameObject.GetComponent<playerStat>().dashCooltime)
        //    {
        //        coolTime += Time.deltaTime;
        //        coolTImeRatio = 1 - (coolTime / this.gameObject.GetComponent<playerStat>().dashCooltime);
        //        this.gameObject.transform.Find("DashCooltime").GetComponent<RectTransform>().localScale = new Vector3(coolTImeRatio, 0.1f, 0);
        //        yield return new WaitForFixedUpdate();
        //    }
        //}

        //IEnumerator Attack()
        //{
        //    rigid.velocity = new Vector2(0, rigid.velocity.y);
        //    IsAttack = true;
        //    HitBox.SetActive(true);
        //    yield return new WaitForSeconds(0.2f);
        //    HitBox.SetActive(false);
        //    readyAttack = true;
        //    yield return new WaitForSeconds(0.2f);
        //    IsAttack = false;
        //    readyAttack = false;


        //}

        //IEnumerator Attack2()
        //{
        //    IsAttack = true;
        //    IsAttack2 = true;
        //    HitBox.SetActive(true);
        //    yield return new WaitForSeconds(0.15f);
        //    HitBox.SetActive(false);
        //    IsAttack = false;
        //    IsAttack2 = false;

        //}

        IEnumerator ReadyAttack2()
    {

        readyAttack = true;

        yield return new WaitForSecondsRealtime(2f);

        readyAttack = false;

    }

    public void MotionReset()
    {
        HitBox.SetActive(false);
        WallHitBox.SetActive(false);
        UpHitBox.SetActive(false);
        DownHitBox.SetActive(false);
        IsAttack = false;
        IsAttack2 = false;
        IsJumpAttack = false;
        IsWallAttack = false;
        IsUpAttack = false;
        readyAttack = false;
    }

  

    //IEnumerator JumpAttack()
    //{

    //    IsJumpAttack = true;

    //    yield return new WaitForSeconds(0.2f);

    //    IsJumpAttack = false;

    //}

    
}




