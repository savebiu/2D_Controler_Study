using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_IdleState : IdleState
{
    Enemy2 enemy;
    public E2_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 一次Idle时间结束后转换为Move状态
        if(isIdleTimeOver)
        {
            enemy.stateMachine.ChangeState(enemy.moveState);
        }

        //TODO: 敌人在 minAgroDistance 距离外转换为 PlayerDetectedState

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
