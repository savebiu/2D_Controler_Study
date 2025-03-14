using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 眩晕状态,打断敌人攻击,一定时间后恢复
 */
public class StunState : State
{
    D_StunState stateData;

    bool isStunTimeOver;        //眩晕时间是否结束
    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;     //眩晕时间未结束
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //眩晕时间结束
        if (Time.time >= startTime + stateData.stuntime)
        {
            isStunTimeOver = true;
            entity.SetVelocity(stateData.stunknockbackSpeed, stateData.knockbackAngle, entity.lastDamageDirection);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
