using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSCircle : MonoBehaviour
{
    public List<Vector2> pos;
    public int enemyDamage;
    public float projectileSpeed;
    public GameObject player;
    public GameObject magic;
    public GameObject enemyProjectile;
    public int maxCount;
    public AudioSource audioSource;
    public AudioClip[] clip;


    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    
}

    // Update is called once per frame
    void Update()
    {
        
    }


    public void spawnPoint()
    {
        
    }

    public void shootMagic1()
    {
        audioSource.PlayOneShot(clip[1]);
        for (int i = 0; i <= 8; i++)
        {
            enemyProjectile = Instantiate(magic);
            enemyProjectile.transform.position = this.transform.position;
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().pos = DegreeToVector2(180 + (22.5f * i));
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().rot = 180 + (22.5f * i);
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().dmg = enemyDamage;
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().speed = projectileSpeed;
        }

        
     
    }

    public void shootMagic2()
    {
        audioSource.PlayOneShot(clip[1]);
        for (int i = 1; i <= 8; i++)
        {
            enemyProjectile = Instantiate(magic);
            enemyProjectile.transform.position = this.transform.position;
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().pos = DegreeToVector2(168.75f + (22.5f * i));
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().rot = 168.75f + (22.5f * i);
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().dmg = enemyDamage;
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().speed = projectileSpeed;
        }
    }

    public void shootMagic3()
    {
        this.gameObject.SetActive(false);
    }
    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
}


 
