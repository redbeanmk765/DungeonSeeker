using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    public int dmg;
    public Vector3 pos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {     
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + (pos), 4f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Monster") && !col.gameObject.CompareTag("EnemyAttack") && !col.gameObject.CompareTag("Attack"))
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<PlayerStat>().damaged = this.dmg;

            }
            Destroy(this.gameObject);
 
        }
    }


}
