using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ѣ��״̬,��ϵ��˹���,һ��ʱ���ָ�
 */
public class StunState : State
{
    D_StunState stateData;

    bool isStunTimeOver;        //ѣ��ʱ���Ƿ����
    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;     //ѣ��ʱ��δ����
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //ѣ��ʱ�����
        if (Time.time >= startTime + stateData.stuntime)
        {
            isStunTimeOver = true;
            entity.SetVelocity(stateData.stunknockbackSpeed, stateData.knockbackAngle, entity.lastDamageDirection);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
