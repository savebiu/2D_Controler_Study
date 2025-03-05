using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    public D_PlayerDetected stateData;      //����������

    protected bool isPlayerInMinAgroRange;      //����Ƿ�����С������Χ��
    protected bool isPlayerInMaxAgroRange;      //����Ƿ�����󹥻���Χ��

    protected bool performCloseRangeAction;        //ִ�н����빥��
    protected bool performLongRangeAction;      //ִ��Զ�̹���

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateDate) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateDate;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();        //�������Ƿ�����С������Χ��
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();        //�������Ƿ�����󹥻���Χ��

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();      //�������Ƿ��ڽ����빥����Χ��
    }

    //��⵽���ʵ������״̬
    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0);      //�����ٶ�Ϊ0
        performLongRangeAction = false;        //ִ��Զ���붯��
        //performCloseRangeAction = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //���ִ��Զ���빥��
        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }

        //TODO��ת��Ϊ����״̬
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
