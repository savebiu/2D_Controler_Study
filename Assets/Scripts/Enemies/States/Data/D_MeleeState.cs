using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeStateData", menuName = "Data/State Data/MeleeData")]
public class D_MeleeState : ScriptableObject
{
    public float attackRadius = 0.5f;       //¼ì²â·¶Î§Îª0.5f
    public float attackDamage = 10f;        //ÉËº¦Îª10

    public LayerMask whatIsPlayer;
}
