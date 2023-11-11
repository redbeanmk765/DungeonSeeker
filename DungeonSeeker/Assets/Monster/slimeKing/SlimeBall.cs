using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    public float dmg;
    public GameObject generator;
    // Start is called before the first frame update
    void Start()
    {
        dmg = 30f;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerStat>().damaged = this.dmg;
            transform.localPosition = new Vector3(0, 0, 0);
            gameObject.SetActive(false);
        }
        else if (col.gameObject.CompareTag("Destroyer"))
        {
            transform.localPosition = new Vector3(0, 0, 0);
            gameObject.SetActive(false);
        }


            

        
    }
}
