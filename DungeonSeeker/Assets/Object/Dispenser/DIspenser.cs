using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIspenser : MonoBehaviour
{
    public GameObject keyUI;
    public Sprite empty;
    public bool IsNear;
    public bool IsFull;
    public PlayerStat playerStat;
    // Start is called before the first frame update
    void Start()
    {
        IsFull = true;
        IsNear = false;
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") && IsNear == true && IsFull == true)
        {
            playerStat.HpPotionCount = playerStat.HpPotionMax;
            this.GetComponent<SpriteRenderer>().sprite = empty;
            IsFull = false;
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
