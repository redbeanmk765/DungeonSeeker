using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class door : MonoBehaviour
{
    public bool IsOpen;
    public bool IsNear;
    public Text doorText;
    // Start is called before the first frame update
    void Start()
    {
        Close();
        IsOpen = true;
        IsNear = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") && IsOpen == true && IsNear == true)
        {
            GameObject.Find("StageController").GetComponent<StageController>().GoNextRoom();
        }
    }

    public void Open()
    {
        this.GetComponent<Animator>().SetInteger("State", 1);
        IsOpen = true;
    }

    public void Close()
    {
        this.GetComponent<Animator>().SetInteger("State", 2);
        IsOpen = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && IsOpen == true)
        {
            doorText.gameObject.SetActive(true);
            IsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && IsOpen == true)
        {
            doorText.gameObject.SetActive(false);
            IsNear = false;
        }
    }
}
