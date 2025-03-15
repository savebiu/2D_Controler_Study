using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 眩晕时间 float stuntime
 * 击退角度 Vecyor2 knockbackAngle
 */
[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    public float stuntime = 2f;         //眩晕时间
    public float stunknockbackTime = 0.2f;       //开始记录击退时间
    public float stunknockbackSpeed = 20f;       //击退速度

    public Vector2 knockbackAngle;      //击退角度
}
