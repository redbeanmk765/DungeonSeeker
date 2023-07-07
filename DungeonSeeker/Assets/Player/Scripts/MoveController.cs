using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float MaxSpeed;
    public Rigidbody2D rigid;
    public float Hor;
    public int JumpCount;
    public bool IsGround;
    public float GroundDistance;
    public LayerMask LayerMask;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        JumpCount = 2;
        GroundDistance = GetComponent<BoxCollider2D>().bounds.extents.y + 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        Hor = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (rigid.velocity.y < 0)
        {
            IsGround = false;
            RaycastHit2D GroundHit = Physics2D.Raycast(transform.position, Vector2.down, GroundDistance, LayerMask);

            if (GroundHit)
            {

                if (GroundHit.transform.CompareTag("Ground"))
                {

                    IsGround = true;
                }
               
            }

            
        }

        if (rigid.velocity.y > 0)
        {
            //IsGround = false;
        }



        if (IsGround == true)
        {
            JumpCount = 2;
        }

        if (Input.GetButtonDown("Jump") ) {
           
            if (JumpCount > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 5);
                JumpCount--;
            }
        }

        
    }   

    void FixedUpdate()
    {
        
       
        rigid.AddForce(Vector2.right * Hor, ForceMode2D.Impulse);

        if (rigid.velocity.x > MaxSpeed)
        {
            rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);
        }

        else if (rigid.velocity.x < -1 * MaxSpeed)
        {
            rigid.velocity = new Vector2(-1 *  MaxSpeed, rigid.velocity.y);
        }

        //if (rigid.velocity.y < 0) // 플레이어가 낙하중일 때 == velocity.y가 음수
        //{
        //    Debug.DrawRay(rigid.position, Vector2.down, new Color(0, 1, 0)); //ray를 그리기
        //    RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Ground")); //ray 쏘기
        //    if (rayHit.collider != null)
        //    { // RayCastHit 변수의 콜라이더로 검색 확인 가능
        //        if (rayHit.distance < 0.5f)
        //        { // ray가 0.5 이상 들어갔을 때
        //            JumpCount = 2;
        //        }
        //    }
        //}
    }
}
