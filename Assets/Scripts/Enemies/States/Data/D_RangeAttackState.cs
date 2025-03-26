using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/RangeAttack Dataa")]
public class D_RangeAttackState : ScriptableObject
{
    public GameObject projectile;       // Ͷ����
    public float projectileDamage = 10f;        // Ͷ�����˺�
    public float projectileSpeed = 12f;     // Ͷ�����ٶ�
    // ���о�������˶�ÿ�ʼ����
    public float projectileTravelDistance = 5f;       // Ͷ������о���

}
