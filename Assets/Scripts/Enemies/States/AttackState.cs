using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 基础攻击类
 */
public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;     //动画是否结束
    protected bool isPlayerInMinAgroRange;      //玩家是否在最小攻击范围内
    protected bool isPlayerInMaxAgroRange;      //玩家是否在最大攻击范围内
    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();     //检测玩家是否在最小攻击范围内
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();

    }

    public override void Enter()
    {
        base.Enter();
        entity.atsm.attackState = this;     //将当前状态传入动画状态机
        isAnimationFinished = false;
        entity.SetVelocity(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
