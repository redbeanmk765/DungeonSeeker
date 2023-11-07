using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoddessStatue : MonoBehaviour
{
    public GameObject keyUI;
    public bool IsNear;
    public GameObject GoldShop;
    // Start is called before the first frame update
    void Start()
    {
        IsNear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") &&  IsNear == true)
        {
            GoldShop.SetActive(true);
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
        }
    }
}
