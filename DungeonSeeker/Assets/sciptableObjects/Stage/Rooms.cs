using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rooms")]



public class Rooms : ScriptableObject
{
    public GameObject BossRoom;
    public GameObject EliteRoom;
    public GameObject ShopRoom;
    public GameObject ChestRoom;
    public List<GameObject> EasyRooms;
    public List<GameObject> NormRooms;
    public List<GameObject> HardRooms;

}

