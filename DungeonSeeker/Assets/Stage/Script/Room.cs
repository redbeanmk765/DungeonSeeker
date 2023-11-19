using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room")]



public class Room : ScriptableObject
{
    public Vector2 MapCenter;
    public Vector2 MapSize;

    public Vector2 BossMapCenter;
    public Vector2 BossMapSize;
    public float BossCameraSize;

    public string Name;

}

