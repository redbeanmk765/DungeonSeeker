using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    public float dmg;
    public GameObject ballGenerator;
    public GameObject BGC;
    // Start is called before the first frame update
    void Start()
    {
        dmg = 30f;
        transform.localPosition = new Vector3(0, -1, 0);
        GetComponent<Rigidbody2D>().gravityScale = 2f;
        BGC = GameObject.Find("BallGenController");
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerStat>().damaged = this.dmg;
            transform.localPosition = new Vector3(0, -1, 0);
            gameObject.SetActive(false);
            BGC.GetComponent<BGcontroller>().Sound();
        }
        else if (col.gameObject.CompareTag("Destroyer"))
        {
            transform.localPosition = new Vector3(0, -1, 0);
            gameObject.SetActive(false);
            BGC.GetComponent<BGcontroller>().Sound();
        }


            

        
    }
}
