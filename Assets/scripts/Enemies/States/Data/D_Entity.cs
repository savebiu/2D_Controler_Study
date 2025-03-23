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

    public float maxHealth = 60;       //血量
    public float damageHopSpeed = 6f;       //伤害跳跃速度
    public float knockbackSpeedX = 4f;       //击退速度X

    //眩晕抗性(敌人承受多少伤害会被眩晕)
    public float stunResistance = 3f;     //眩晕抗性
    //眩晕回复时间(敌人上次收到眩晕后需要多久才能恢复)
    public float stunRecorveryTime = 2f;

    [Header("效果")]
    [SerializeField]
    public GameObject hitParticle;     //受伤粒子效果
    public Object deathChunkParticle;       //死亡粒子效果
}
