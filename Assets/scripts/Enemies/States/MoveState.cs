using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * �ýű��ǹ�����ƶ�״̬
 */
public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;       //���ǽ��
    protected bool isDetectingLedge;       //�������

    //base�������ø���Ĺ��캯��,�����ǵ���State�Ĺ��캯��,��Ϊ���಻��ֱ�Ӽ̳и���Ĺ��캯��
    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();       //baseͬʱ���ø����Enter�����еĹ��캯��
        entity.SetVelocity(stateData.movementSpeed);       //����ʵ���ٶ�

        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
    }
}