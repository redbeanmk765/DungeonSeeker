using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public float maxHpPer = 100;
    public float defPer = 0;
    public float hitBoxPer = 0.7f;
    public float dashCoolTimePer = 2;
    public float AttackCoolTimePer = 0.2f;
    public float DmgPer = 6;
    public int PlayerPlat = 0;
    public bool skillOn = false;
    public float skilldurationPer = 6;
    public float skillCoolTimePer = 60;
    public int AirJumpCountMaxPer = 1;
}
