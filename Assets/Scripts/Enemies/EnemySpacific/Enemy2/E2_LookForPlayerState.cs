using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_LookForPlayerState : LookForPlayerState
{
    Enemy2 enemy;
    public E2_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        // 超出最小攻击范围，转换为玩家检测状态
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        //// 完成所有翻转查找, 换为玩家检测状态
        //else if (isAllTurnsDone)
        //{
        //    stateMachine.ChangeState(enemy.idleState);
        //}
        // 查找时间结束，换为移动状态
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
