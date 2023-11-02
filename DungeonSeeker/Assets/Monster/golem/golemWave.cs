using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golemWave : MonoBehaviour
{
    // Start is called before the first frame update
    public float enemyDamage;

    private void Start()
    {
    
    }

        private void Destroy()
    {
        // Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("PlayerHitBox"))
        {
            GameObject.Find("Player").GetComponent<PlayerStat>().damaged = enemyDamage;

        }
    }
}
