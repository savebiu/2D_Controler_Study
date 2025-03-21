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

        // ����ǽ�ڻ���δ��⵽���� ת��ΪIdle״̬
        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterImage(true);    //��ת
            stateMachine.ChangeState(enemy.idleState);   //ת��ΪIdle״̬ 
        }

        //TODO: ������ minAgroDistance ������ת��Ϊ PlayerDetectedState
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
