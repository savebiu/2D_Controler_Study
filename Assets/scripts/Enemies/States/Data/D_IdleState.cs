using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 该脚本同于保存怪物空闲状态的数据
 */

[CreateAssetMenu(fileName = "newIdleState Data", menuName = "Data/State Data/Idle Data")]
public class D_IdleState : ScriptableObject        
{
    public float minIdleTime = 1f;
    public float maxIdleTime = 2f;
}
