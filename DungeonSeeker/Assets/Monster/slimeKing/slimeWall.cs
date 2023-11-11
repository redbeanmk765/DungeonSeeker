using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeWall : MonoBehaviour
{
    public float dmg;
    public GameObject onObj;
    public bool onTrigger;
    // Start is called before the first frame update
    void Start()
    {
        onTrigger = false;
        dmg = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (onTrigger)
        {
            onObj.gameObject.GetComponent<PlayerStat>().damaged = this.dmg;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("trap");
            onTrigger = true;
            //col.gameObject.GetComponent<playerStat>().damaged = this.dmg;
            onObj = col.gameObject;
            // StartCoroutine(WaitForDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            onTrigger = false;
            
        }
    }

}
