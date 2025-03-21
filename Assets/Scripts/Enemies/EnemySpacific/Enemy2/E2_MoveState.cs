using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MoveState : MoveState
{
    private Enemy2 enemy;

    public E2_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        // 碰到墙壁或者未检测到悬崖 转换为Idle状态
        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterImage(true);    //翻转
            stateMachine.ChangeState(enemy.idleState);   //转换为Idle状态 
        }

        //TODO: 敌人在 minAgroDistance 距离外转换为 PlayerDetectedState
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
