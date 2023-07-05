using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float MaxSpeed;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }   

    void FixedUpdate()
    {
        float Hor = Input.GetAxisRaw("Horizontal");
        Debug.Log(Hor);
        rigid.AddForce(Vector2.right * Hor, ForceMode2D.Impulse);

        if (rigid.velocity.x > MaxSpeed)
        {
            rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);
        }

        else if (rigid.velocity.x < -1 * MaxSpeed)
        {
            rigid.velocity = new Vector2(-1 *  MaxSpeed, rigid.velocity.y);
        }
    }
}
