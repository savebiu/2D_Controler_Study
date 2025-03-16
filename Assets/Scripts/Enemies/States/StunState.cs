using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 眩晕状态,打断敌人攻击,一定时间后恢复
 */
public class StunState : State
{
    D_StunState stateData;
 
    protected bool isStunTimeOver;        //眩晕时间是否结束
    protected bool isGrounded;        //是否在地面上
    protected bool isMovementStopped;        //是否停止移动

    protected bool performCloseRangeAction;        //执行近距离动作
    protected bool isPlayerInMinAgroRange;        //玩家是否在最小攻击范围内

    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();        //检测玩家是否在近距离动作范围内
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();        //检测玩家是否在最小攻击范围内
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;     //眩晕时间未结束
        isMovementStopped = false;      //移动未停止 
        entity.SetVelocity(stateData.stunknockbackSpeed, stateData.knockbackAngle, entity.lastDamageDirection);     //设置速度
        isGrounded = entity.groundCheck;        //是地面检测
    }

    public override void Exit()
    {
        base.Exit();

        entity.ResetStunResistance();       //重置眩晕抵抗

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //眩晕时间结束
        if (Time.time >= startTime + stateData.stuntime)
        {
            isStunTimeOver = true;
            
        }

        //击退时间结束 并且 时间没停止
        if (Time.time >= startTime + stateData.stunknockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            entity.SetVelocity(0);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
