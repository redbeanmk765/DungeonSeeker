using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoddessStatueP : MonoBehaviour
{
    public GameObject keyUI;
    public bool IsNear;
    public bool IsOpen;
    public GameObject playerUI;
    // Start is called before the first frame update
    void Start()
    {
        IsNear = false;
        IsOpen = false;
        playerUI = GameObject.Find("PlayerUI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") &&  IsNear == true && IsOpen == false)
        {
            IsOpen = true;
            playerUI.GetComponent<ShopController>().OpenPlatShop();
            this.GetComponent<AudioSource>().Play();
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
        if (col.CompareTag("Player") )
        {
            keyUI.SetActive(false);
            IsNear = false;
            IsOpen = false;
        }
    }
}
