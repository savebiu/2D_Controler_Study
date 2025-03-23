using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    //状态列表
    public E2_IdleState idleState { get; private set; }     //引入Idle状态
    public E2_MoveState moveState { get; private set; }     //引入Move状态
    public E2_PlayerDetectedState playerDetectedState { get; private set; }     //引入PlayerDetected状态
    public E2_MeleeAttackState meleeAttackState { get; private set; }     //引入MeleeAttack状态
    public E2_LookForPlayerState lookForPlayerState { get; private set; }     //引入LookForPlayer状态
    public E2_StunState stunState { get; private set; }     //引入Stun状态

    //数据列表
    [Header("数据")]
    [SerializeField]
    private D_IdleState idleStateData;     //引入Idle状态数据
    [SerializeField]
    private D_MoveState moveStateData;     //引入Move状态数据
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;     //引入PlayerDetected状态数据
    [SerializeField]
    private D_MeleeState meleeAttackStateData;     //引入MeleeAttack状态数据
    [SerializeField]
    private Transform meleeAttackPosition;     //近战攻击位置
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;     //引入LookForPlayer状态数据
    [SerializeField]
    private D_StunState stunStateData;     //引入Stun状态数据

    public override void Start()
    {
        base.Start();

        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);     //将Idle状态传递给状态机
        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);     //将Move状态传递给状态机
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);     //将PlayerDetected状态传递给状态机
        meleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);     //将MeleeAttack状态传递给状态机
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);     //将LookForPlayer状态传递给状态机
        stunState = new E2_StunState(this, stateMachine, "stun", stunStateData, this);     //将Stun状态传递给状态机

        stateMachine.Initialize(moveState);     //move作为初始状态
    }

    public override void Damage(AttackDetails attackDealis)
    {
        base.Damage(attackDealis);
        Debug.Log("进入损伤");
        // 被眩晕并且不在眩晕状态
        if(isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }

        // 背后攻击时翻转Enemy
        else if (!CheckPlayerInMinAgroRange())
        {
            Debug.Log("在检测状态");
            lookForPlayerState.SetTurnImediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
