using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicCircle : MonoBehaviour
{
    public Vector3 playerPositon;
    public Vector3 pos;
    public float posX;
    public float posY;
    public bool IsStuck;
    public int maxCount;
    public int layermask;
    public int dmg;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        layermask = 1 << LayerMask.NameToLayer("Ground");

        playerPositon = GameObject.FindGameObjectsWithTag("Player")[0].gameObject.transform.position;
        IsStuck = false;
        //maxCount = 0;
        //while (IsStuck == false && maxCount <= 999)
        //{
           spawnPoint();
        //    maxCount++;
        //}

        //angle = Mathf.Atan2(playerPositon.y - this.gameObject.transform.position.y, playerPositon.x - this.gameObject.transform.position.x) * Mathf.Rad2Deg;
        //this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //pos.x = Mathf.Cos(this.transform.eulerAngles.z * Mathf.Deg2Rad);
        //pos.y = Mathf.Sin(this.transform.eulerAngles.z * Mathf.Deg2Rad);


    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, pos, Color.red);
        Debug.DrawRay(this.transform.position,  playerPositon - this.transform.position , Color.blue);

        Ray ray = new Ray(this.transform.position, playerPositon - this.transform.position);

        if (Physics.Raycast(ray, 10))
        {
            Debug.Log("Stuck");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    { 
            if (col.CompareTag("Ground"))
            {
            //Debug.Log("stuck");
                IsStuck = true;
            }
    }

    public void spawnPoint()
    {
        posX = Random.Range(playerPositon.x - 5f, playerPositon.x + 5f);
        posY = Random.Range(playerPositon.y , playerPositon.y + 3f);

        this.transform.position = new Vector2(posX, posY);

       

        return;
    }
}
