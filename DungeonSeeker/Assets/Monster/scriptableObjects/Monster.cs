using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monster")]



public class Monster : ScriptableObject
{
    public string enemyName;
    public float maxHp;
    public int level;
    public int enemyDamage;
    public Race race;
    public float moveSpeed;
    public float projectileSpeed;
    public GameObject projectile;
    public GameObject projectile2;
    public int enemyGold;
    [SerializeField] public Material originalMaterial;
    [SerializeField] public Material flashMaterial;

}
public enum Race
{
    slime,
    skeletonArcher,
    mushroom,
    bat,
    ghost,
    magicBook,
    spider,
    golem,
    slimeKing

}