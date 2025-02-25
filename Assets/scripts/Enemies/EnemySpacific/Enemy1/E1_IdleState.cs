using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : IdleState
{
    public Enemy1 enemy;
    public E1_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateDate, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateDate)
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
        //如果空闲时间结束，转换到移动状态
        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
