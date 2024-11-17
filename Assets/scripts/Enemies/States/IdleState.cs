using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * �ýű��ǹ���Ŀ���״̬
 */

public class IdleState : State
{
    protected D_IdleState stateData;

    [Header("�ж�����")]
    protected bool FlipAfterImage;      //�Ƿ�ת
    protected bool isIdleTimeOver;      //�Ƿ����ʱ�����

    protected float IdleTime;       //���ٽ���Idle��ʱ��
    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0f);     //�ٶ�Ϊ0����
        isIdleTimeOver = false;     
        SetRandomIdleTiem();        //�����������ʱ��
    }

    public override void Exit()
    {
        base.Exit();

        if (FlipAfterImage)
        {
            entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + IdleTime)
        {
            isIdleTimeOver = true;
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    //�Ƿ�ת
    public void SetFlipAfterImage(bool flip)
    {
        FlipAfterImage = flip;
    }

    //
    public void SetRandomIdleTiem()
    {
        IdleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
