using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicBookCircle : MonoBehaviour
{
    public Vector3[] pos;
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
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void spawnPoint()
    {
        
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


 
