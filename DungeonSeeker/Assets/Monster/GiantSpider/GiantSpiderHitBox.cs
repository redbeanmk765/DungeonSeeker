using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSpiderHitBox : MonoBehaviour
{
    public GameObject player;
    public GameObject giantSpider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PlayerHitBox"))
        {
            player.GetComponent<PlayerStat>().damaged = giantSpider.GetComponent<GiantSpider>().monsterStat.enemyDamage;

        }
    }
}
