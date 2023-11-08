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
    }
}
