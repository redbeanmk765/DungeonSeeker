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
    public int layermask;
    public int enemyDamage;
    public float angle;
    public float distance;
    public float projectileSpeed;
    public GameObject player;
    public GameObject magic;
    public GameObject enemyProjectile;
    public int maxCount;

    // Start is called before the first frame update
    void Start()
    {
        layermask = (1 << LayerMask.NameToLayer("Ground"));
        player = GameObject.FindGameObjectsWithTag("Player")[0].gameObject;
        playerPositon = player.transform.position;
        IsStuck = true;
        maxCount = 0;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        while (IsStuck == true || distance <= 4.5 || distance >= 5.2 )
        {
            spawnPoint();
            maxCount++;
            if(maxCount >= 999)
            {
                Destroy(this.gameObject);
                break;
            }   
        }
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }


    public void spawnPoint()
    {
        posX = Random.Range(playerPositon.x - 5f, playerPositon.x + 8f);
        posY = Random.Range(playerPositon.y, playerPositon.y + 5f);

        this.transform.position = new Vector2(posX, posY);
    
        distance = Vector2.Distance(this.transform.position, playerPositon);


        RaycastHit2D ray = Physics2D.BoxCast(this.transform.position, new Vector2(1.2f, 1.2f), 0, playerPositon - this.transform.position, distance - 0.5f, layermask);

        if (ray != false)
        {
            IsStuck = true;
        }
        else
        {
            IsStuck = false;
        }

        return;
    }

    public void shootFire()
    {
        enemyProjectile = Instantiate(magic);
        enemyProjectile.transform.position = this.transform.position;
        enemyProjectile.gameObject.GetComponent<targetEnemyProjectile>().target = player;
        enemyProjectile.gameObject.GetComponent<targetEnemyProjectile>().dmg = this.enemyDamage;
        enemyProjectile.gameObject.GetComponent<targetEnemyProjectile>().speed = this.projectileSpeed;

    }

    public void shootFire2()
    {
        Destroy(this.gameObject);
    }
}


 
