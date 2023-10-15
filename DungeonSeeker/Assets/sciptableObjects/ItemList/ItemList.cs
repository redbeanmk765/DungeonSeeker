using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemList")]



public class ItemList : ScriptableObject
{
    public List<GameObject> Weapons;
    public List<GameObject> PassiveItems;
    public List<GameObject> ConsumableItems;
    public GameObject Key;
    public GameObject Coin;

}
