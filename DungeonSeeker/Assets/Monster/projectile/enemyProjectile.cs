using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    public int dmg;
    public float rot;
    public float speed;
    public Vector3 pos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameObject.Find("StageController").GetComponent<StageController>().curRoom.transform;
        this.transform.localEulerAngles = new Vector3(0, 0, rot);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + (pos), speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Monster") && !col.gameObject.CompareTag("EnemyAttack") && !col.gameObject.CompareTag("Attack") && !col.gameObject.CompareTag("Trigger"))
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<PlayerStat>().damaged = this.dmg;

            }

            Destroy(this.gameObject);

        }
    }


}
