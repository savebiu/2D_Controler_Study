using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_MeleeState : ScriptableObject
{
    public float attackRadius = 0.5f;       //��ⷶΧΪ0.5f
    public float attackDamage = 10f;        //�˺�Ϊ10

    public LayerMask whatIsPlayer;
}
