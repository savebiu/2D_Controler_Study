using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/RangeAttack Dataa")]
public class D_RangeAttackState : ScriptableObject
{
    public GameObject projectile;       // 投射物
    public float projectileDamage = 10f;        // 投射物伤害
    public float projectileSpeed = 12f;     // 投射物速度
    // 飞行距离决定了多久开始下落
    public float projectileTravelDistance = 5f;       // 投射物飞行距离

}
