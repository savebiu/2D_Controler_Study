using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    //状态列表
    public E2_IdleState idleState { get; private set; }     //引入Idle状态
    public E2_MoveState moveState { get; private set; }     //引入Move状态

    //数据列表
    [Header("数据")]
    [SerializeField]
    private D_IdleState idleStateData;     //引入Idle状态数据
    [SerializeField]
    private D_MoveState moveStateData;     //引入Move状态数据

    public override void Start()
    {
        base.Start();

        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);     //将Idle状态传递给状态机
        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);     //将Move状态传递给状态机

        stateMachine.Initialize(moveState);     //move作为初始状态
    }

    public override void Damage(AttackDetails attackDealis)
    {
        base.Damage(attackDealis);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
