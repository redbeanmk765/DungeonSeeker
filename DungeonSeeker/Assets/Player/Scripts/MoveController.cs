using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float MaxSpeedX;
    public float MaxSpeedY;
    public Rigidbody2D rigid;
    public Animator Animator;
    public float Hor;
    public float Ver;
    public float LastHor;
    public int AirJumpCount;
    public int AirJumpCountMax;
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

    // Start is called before the first frame update
    void Start()
    {
        MaxSpeedX = 5;
        MaxSpeedY = 10;
        rigid = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        AirJumpCountMax = 1;
        AirJumpCount = AirJumpCountMax;

        GroundDistance = GetComponent<BoxCollider2D>().bounds.extents.y + 0.1f;
        GroundScale = GetComponent<BoxCollider2D>().bounds.extents.x + 0.1f;

        Gravity = this.GetComponent<ConstantForce2D>();
        Gravity.force = new Vector2(0, -9.8f);

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
        DashCooltime = 1.7f;
        AttackCooltime = 0.2f;
        HitBox = this.transform.Find("HitBox").gameObject;
        HitBox.SetActive(false);
        WallHitBox = this.transform.Find("WallHitBox").gameObject;
        WallHitBox.SetActive(false);
        UpHitBox = this.transform.Find("UpHitBox").gameObject;
        UpHitBox.SetActive(false);
        DownHitBox = this.transform.Find("DownHitBox").gameObject;
        DownHitBox.SetActive(false);

        //Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GetComponentsInChildren<BoxCollider2D>()[1]);

    }
       

// Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, new Vector3(1, 0, 0) * 0.4f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * 1, new Color(0, 1, 0));
        Debug.DrawRay(transform.position, new Vector3(0, 1, 0) * 0.9f, new Color(0, 1, 0));

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
        if (rigid.velocity.x > 0.1 && IsJumpAttack == false )

        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (rigid.velocity.x < -0.1 && IsJumpAttack == false )
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        }

        if ((Input.GetButtonUp("Horizontal") || Hor == 0) && IsWallJump == false && IsDash == false)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }



        RaycastHit2D GroundHit = Physics2D.BoxCast(transform.position, new Vector2(GroundScale, 0.01f), 0, Vector2.down, GroundDistance, LayerMask);

        if (GroundHit != false)
        {

            if (GroundHit.transform.CompareTag("Ground"))
            {
                AirJumpCount = AirJumpCountMax;
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
            AirJumpCount = AirJumpCountMax;

        }
        else
        {
            IsWall = false;
        }


        if (Input.GetButtonDown("Jump"))
        {

            if (AirJumpCount > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 6f);

                if (IsGround == false && IsWall == false)
                {
                    AirJumpCount--;
                }
            }

            if (IsWall == true)
            {
                rigid.AddForce(new Vector2(-1 * Hor * 5f, 6f), ForceMode2D.Impulse);
                StartCoroutine(WallJump());
            }
        }

        if (Input.GetButton("Horizontal") && (WallHit != false) && IsDash == false && IsAttack == false && IsAttack2 == false && IsJumpAttack == false)
        {
            //rigid.velocity = new Vector2(0, -2f);
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
            MaxSpeedY = 2f;
            LastHor = LastHor * -1;
            Gravity.force = new Vector2(0, -20f);

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
                Gravity.force = new Vector2(0, -9.8f);
            }
            MaxSpeedY = 10;

        }

        if (Input.GetButtonDown("Dash") && IsDashReady == true)
        {
            if (IsDash == false && IsWallJump == false)
            {
                IsDash = true;
                Hor = 0;
                dashDirection = LastHor;
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
        if (Input.GetButtonDown("Attack") && IsDash == false && IsAttack == false  && IsAttack2 == false && IsAttackCooltime == false)
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

    }

    void FixedUpdate()
    {


        if (IsWallJump == false && IsAttack == false && IsAttack2 == false)
        {
            rigid.AddForce(Vector2.right * Hor * MaxSpeedX, ForceMode2D.Impulse);
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


    IEnumerator WallJump()
    {
        IsWallJump = true;
        Hor = LastHor * -1;
        yield return new WaitForSeconds(0.05f);
        rigid.velocity = new Vector2(rigid.velocity.x, 6f);
        yield return new WaitForSeconds(0.1f);
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
        MaxSpeedX = 12;
  
        rigid.velocity = new Vector2(dashDirection * 20f, 0);        
        Gravity.force = new Vector2(0, 0);
        yield return new WaitForSeconds(0.35f);
        Gravity.force = new Vector2(0, -9.8F);
        MaxSpeedX = 5;
        IsDash = false;
    }

    IEnumerator DashCoolTimeBar()
    {

        float coolTime = 0;
        float coolTImeRatio = 1;
        while (coolTime <= DashCooltime)
        {
            coolTime += Time.deltaTime;
            coolTImeRatio = 1 - (coolTime / DashCooltime);
            this.gameObject.transform.Find("DashCooltime").GetComponent<RectTransform>().localScale = new Vector3(coolTImeRatio * 0.7f, 0.05f, 0);
            yield return new WaitForFixedUpdate();
        }
        this.gameObject.transform.Find("DashCooltime").gameObject.SetActive(false);
    }

    IEnumerator DashCoolTime() 
    { 
        IsDashReady = false;
        yield return new WaitForSeconds(DashCooltime);
        IsDashReady = true;
    }

    IEnumerator AttackCoolTime()
    {
        IsAttackCooltime = true;
        yield return new WaitForSeconds(AttackCooltime);
        IsAttackCooltime = false;
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

        yield return new WaitForSeconds(2f);

        readyAttack = false;

    }

  

    //IEnumerator JumpAttack()
    //{

    //    IsJumpAttack = true;

    //    yield return new WaitForSeconds(0.2f);

    //    IsJumpAttack = false;

    //}

    
    
}




