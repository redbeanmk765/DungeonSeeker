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
    public bool IsDashReady;
    public float DashCooltime;
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
        LastHor = 1;
        DashCooltime = 2f;
    }


    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, new Vector3(1, 0, 0) * 0.4f, new Color(0, 1, 0));
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * 1, new Color(0, 1, 0));
        Debug.DrawRay(transform.position, new Vector3(0, 1, 0) * 0.9f, new Color(0, 1, 0));
        
        if (Hor != 0 && IsWallJump == false)
        {
            LastHor = Hor;
        }

        if (IsDash == false && IsWallJump == false)
        {
            Hor = Input.GetAxisRaw("Horizontal");
          
        }

        if (Hor == 1 && Hor != LastHor)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (Hor == -1 && Hor != LastHor)
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
            }

        }
        else
        {
            IsGround = false;
        }

        RaycastHit2D WallHit = Physics2D.BoxCast(transform.position, new Vector2(0.01f, 1.70f), 0, Vector2.right * Hor, 0.4f, LayerMask);
        if (WallHit != false)
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

        if (Input.GetButton("Horizontal") && (WallHit != false) && IsDash == false)
        {
            //rigid.velocity = new Vector2(0, -2f);
            MaxSpeedY = 2f;

            Gravity.force = new Vector2(0, -20f);

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
            Debug.Log("test");
            if (IsDash == false)
            {
                StartCoroutine(Dash());
                StartCoroutine(DashCoolTime());
            }

        }

        if (Hor == 0 && IsGround == true && IsDash == false)
        {
            Animator.SetInteger("State", 1);
        }

        if (Hor != 0 && IsGround == true && IsDash == false)
        {
            Animator.SetInteger("State", 2);
        }

        if(IsDash == true)
        {
            Animator.SetInteger("State", 3);
        }

        if (IsGround == false && rigid.velocity.y > 0)
        {
            Animator.SetInteger("State", 4);
        }

        if(IsGround == false && rigid.velocity.y < 0)
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
    }

    void FixedUpdate()
    {


        if (IsWallJump == false)
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

    IEnumerator WallJump()
    {
        IsWallJump = true;
        Hor = LastHor * - 1; 
        yield return new WaitForSeconds(0.05f);
        rigid.velocity = new Vector2(rigid.velocity.x, 6f);
        yield return new WaitForSeconds(0.25f);
        IsWallJump = false;
    }

    IEnumerator Dash()
    {
        IsDash = true;
        MaxSpeedX = 10;
        rigid.velocity = new Vector2(LastHor * 10f, 0);
        Gravity.force = new Vector2(0, 0);
        yield return new WaitForSeconds(0.25f);
        Gravity.force = new Vector2(0, -9.8F);
        MaxSpeedX = 5;
        IsDash = false;
    }

    IEnumerator DashCoolTime()
    {
        IsDashReady = false;
        yield return new WaitForSeconds(DashCooltime);
        IsDashReady = true;
    }
}
