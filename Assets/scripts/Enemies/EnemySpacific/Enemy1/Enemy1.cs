using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    //状态列表
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }     //玩家检测状态获取器
    public E1_ChargeState chargeState { get; private set; }       //冲锋状态获取器
    public E1_LookForPlayerState lookForPlayerState { get; private set; }       //寻找玩家状态获取器
    public E1_MeleeAttackState meleeAttackState { get; private set; }       //近战攻击状态获取器
    public E1_StunState stunState { get; private set; }       //眩晕状态获取器

    //数据列表
    [Header("数据")]
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;       //玩家检测状态数据
    [SerializeField]
    private D_ChargeState chargeStateData;     //冲锋状态数据
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;        //寻找玩家状态数据
    [SerializeField]
    private D_MeleeState meleeAttackStateData;       //近战攻击状态数据
    [SerializeField]
    private Transform meleeAttackPosition;     //近战攻击位置
    [SerializeField]
    private D_StunState stunStateData;       //眩晕状态数据

    public override void Start()
    {
        base.Start();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);      //创建移动状态
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);      //创建空闲状态
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);      //创建玩家检测状态
        chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);      //创建冲锋状态
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);      //创建寻找玩家状态
        meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);      //创建近战攻击状态
        stunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);      //创建眩晕状态

        stateMachine.Initialize(idleState);     //初始化状态机
    }    

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if(isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
