using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ����С������Χ�ڼ�⵽���ʱ�����ｫ������״̬
 * ���������Ե��ǽ�ڣ���ֹͣ��棬����Idle״̬
 * 
 */
public class ChargeState : State
{
    protected D_ChargeState stateData;
    protected bool isPlayerInMinAgroRange;      // ����Ƿ�����С������Χ��
    protected bool isDetectingLedge;        // ��⵽����
    protected bool isDetectingWall;     //��⵽ǽ��
    protected bool isChargeTimeOver;        //���ʱ���Ƿ����

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();        //�������С������Χ��
        isDetectingLedge = entity.CheckLedge();        //�������
        isDetectingWall = entity.CheckWall();       //���ǽ��
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;       //���ʱ��δ����
        entity.SetVelocity(stateData.chargeSpeed);        //���ó���ٶ�
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
       
        //�������
        if(Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
