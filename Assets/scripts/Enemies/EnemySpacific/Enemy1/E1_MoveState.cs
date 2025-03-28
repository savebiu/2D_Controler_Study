using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 enemy;
    public E1_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

        //在最小检测范围内则将状态转换为 PlayerDetectedState
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }

        //如果检测到墙壁或者没有检测到悬崖则转换到 Idle
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterImage(true);      //设置翻转
            stateMachine.ChangeState(enemy.idleState);      //通过状态机进行状态转换
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
