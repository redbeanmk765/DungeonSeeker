using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]

public class weapon : ScriptableObject
{
    public int dmg;
    public int weaponNumber;
    public Sprite objectSprite;
    public string objectName;
    [SerializeField] public GameObject playerHitBox;
    [SerializeField] public RuntimeAnimatorController weaponAnimator;
    public Type type;
    public float attackSpeed;

    public enum Type
    {
        stick,
        bow
    }


   
}
