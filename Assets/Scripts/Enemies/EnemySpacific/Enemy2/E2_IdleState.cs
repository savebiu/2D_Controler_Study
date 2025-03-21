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

        // һ��Idleʱ�������ת��ΪMove״̬
        if(isIdleTimeOver)
        {
            enemy.stateMachine.ChangeState(enemy.moveState);
        }

        //TODO: ������ minAgroDistance ������ת��Ϊ PlayerDetectedState

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
