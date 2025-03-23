using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_LookForPlayerState : LookForPlayerState
{
    Enemy1 enemy;
    public E1_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateDate, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateDate)
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
        //在最小检测距离时转换为玩家检测状态
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
            //Debug.Log("进入玩家检测检测");
        }
        //如果所有转向都完成,则转为移动状态
        //else if (isAllTurnsDone)
        else if (isAllTurnsTimeDone)
        {
            //Debug.Log("所有翻转完成");
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
