using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossHpBar : MonoBehaviour
{
    public GameObject bossHpbarBack;
    public GameObject bossHpBar;
    public Image image;
    public bool On;

    // Start is called before the first frame update
    void Start()
    {
        image = bossHpBar.GetComponent<Image>();
        SetFalse();
        On = false;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void SetFalse()
    {
        bossHpbarBack.SetActive(false);
        bossHpBar.SetActive(false);
        On = false ;
    }

    public void SetTrue()
    {
        bossHpbarBack.SetActive(true);
        bossHpBar.SetActive(true);
        On = true;
    }
}
