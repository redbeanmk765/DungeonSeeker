using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public bool IsOpen;

    // Start is called before the first frame update
    void Start()
    {
        Close();
        IsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction") && IsOpen == true)
        {
            Debug.Log("test");  
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
}
