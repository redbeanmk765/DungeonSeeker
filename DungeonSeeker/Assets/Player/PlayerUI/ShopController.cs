using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject goldShop;
    public GameObject platShop;



   public void OpenGoldShop()
    {
        goldShop.SetActive(true);
        GameObject.Find("Player").GetComponent<MoveController>().IsFade = true ;
    }

   public void OpenPlatShop()
    {
        platShop.SetActive(true);
        GameObject.Find("Player").GetComponent<MoveController>().IsFade = true;
    }
}
