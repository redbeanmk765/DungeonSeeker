using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetEnemyProjectile : MonoBehaviour
{
    public int dmg;
    public float angle;
    public float speed;
    public GameObject target;
    public Vector3 targetPos;
    Vector3 pos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = GameObject.Find("StageController").GetComponent<StageController>().curRoom.transform;
        targetPos = target.transform.position;
        angle = Mathf.Atan2(targetPos.y - this.gameObject.transform.position.y, targetPos.x - this.gameObject.transform.position.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        pos.x = Mathf.Cos(this.transform.eulerAngles.z * Mathf.Deg2Rad);
        pos.y = Mathf.Sin(this.transform.eulerAngles.z * Mathf.Deg2Rad);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + (pos), speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Monster") && !col.gameObject.CompareTag("EnemyAttack") && !col.gameObject.CompareTag("Attack") && !col.gameObject.CompareTag("Trigger") )
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.GetComponent<PlayerStat>().damaged = this.dmg;

            }
            Destroy(this.gameObject);

        }
    }


}
