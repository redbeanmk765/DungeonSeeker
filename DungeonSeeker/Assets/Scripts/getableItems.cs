using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class getableItems : MonoBehaviour
{
    public weapon weapon;
    public GameObject obj;
    public TMP_Text nameText;
    public int itemNumber;   //1은 무기, 3은 소비템
    bool isNear;

    public string objectName;

    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = objectName;
        nameText.gameObject.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        nameText.transform.position = this.transform.position + new Vector3(0, 0.5f, 0); ;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
                nameText.gameObject.SetActive(true);
                isNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            nameText.gameObject.SetActive(false);
            isNear = false;
        }
    }

}
