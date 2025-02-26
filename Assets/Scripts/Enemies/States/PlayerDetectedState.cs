using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    public D_PlayerDetected stateData;      //����������

    protected bool isPlayerInMinAgroRange;      //����Ƿ�����С������Χ��
    protected bool isPlayerInMaxAgroRange;      //����Ƿ�����󹥻���Χ��
    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateDate) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateDate;
    }

    //��⵽���ʵ������״̬
    public override void Enter()
    {        
        base.Enter();

        entity.SetVelocity(0);      //�����ٶ�Ϊ0
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();        //�������Ƿ�����󹥻���Χ��
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();        //�������Ƿ�����С������Χ��
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

        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }
}
