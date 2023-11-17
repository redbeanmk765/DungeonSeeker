using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicBookCircle : MonoBehaviour
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
        pos = new List<Vector2> {
            new Vector2(1f,0f),  new Vector2(0.65f, 0.35f), new Vector2(0.35f, 0.65f), 
            new Vector2(0f, 1f),  new Vector2(-0.35f, 0.65f), new Vector2(-0.65f, 0.35f), 
            new Vector2(-1f, 0f),  new Vector2(-0.65f, -0.35f), new Vector2(-0.35f, -0.65f), 
            new Vector2(0f, -1f),  new Vector2(0.35f, -0.65f), new Vector2(0.65f, -0.35f)
        };
}

    // Update is called once per frame
    void Update()
    {
        
    }


    public void spawnPoint()
    {
        
    }

    public void shootMagic()
    {
        audioSource.PlayOneShot(clip[1]);
        for (int i = 0; i <= 11; i++)
        {
            enemyProjectile = Instantiate(magic);
            enemyProjectile.transform.position = this.transform.position;
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().pos = pos[i];
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().rot = 30 * i;
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().dmg = enemyDamage;
            enemyProjectile.gameObject.GetComponent<enemyProjectile>().speed = projectileSpeed;
        }

    }

    public void shootMagic2()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}


 
