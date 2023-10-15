using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WallType")]



public class WallType : ScriptableObject
{
    [SerializeField] public GameObject Wall_U;
    [SerializeField] public GameObject Wall_R;
    [SerializeField] public GameObject Wall_D;
    [SerializeField] public GameObject Wall_L;

    [SerializeField] public GameObject Wall_UR;
    [SerializeField] public GameObject Wall_UD;
    [SerializeField] public GameObject Wall_UL;
    [SerializeField] public GameObject Wall_RD;
    [SerializeField] public GameObject Wall_RL;
    [SerializeField] public GameObject Wall_DL;

    [SerializeField] public GameObject Wall_URD;
    [SerializeField] public GameObject Wall_URL;
    [SerializeField] public GameObject Wall_UDL;
    [SerializeField] public GameObject Wall_RDL;

    [SerializeField] public GameObject Wall_URDL;

}
