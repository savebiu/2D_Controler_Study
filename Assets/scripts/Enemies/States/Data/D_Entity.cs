using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.2f;      //墙面检测距离
    public float ledgeCheckDistance = 0.4f;     //悬崖检测距离
    public float groundChekDistance = 0.2f;     //地面检测距离

    public float minAgroDistance = 3f;      //最小仇恨距离
    public float maxAgroDistance = 4f;      //最大仇恨距离

    //攻击检测距离
    public float closeRangeActionDistance = 1f;
    public float longRangeActionDistance = 5f;

    public LayerMask whatIsGround;      //地面图层
    public LayerMask whatIsPlayer;      //角色图层

    public float maxHealth;       //血量
    public float damageHopSpeed = 6f;       //伤害跳跃速度
    public float knockbackSpeedX;       //击退速度X

}
