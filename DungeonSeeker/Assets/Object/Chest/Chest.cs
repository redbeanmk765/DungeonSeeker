using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public GameObject keyUI;
    public bool IsNear;
    public bool IsOpen;
    public PlayerStat playerStat;
    public Sprite open;
    // Start is called before the first frame update
    void Start()
    {
        IsNear = false;
        IsOpen = false;
       playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") && IsNear == true && IsOpen == false)
        {
            IsOpen = true;
            GetComponent<SpriteRenderer>().sprite = open;
            playerStat.PlayerGold += 50;
            playerStat.totalGold += 50;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            keyUI.SetActive(true);
            IsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            keyUI.SetActive(false);
            IsNear = false;
        }
    }

}
