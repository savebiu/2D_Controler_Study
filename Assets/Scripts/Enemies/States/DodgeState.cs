using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected D_DodgeState stateData;
    protected bool performCloseRangeAction;     // �Ƿ�ִ�н����붯��
    protected bool isPlayerInMaxAgroRange;      // ����Ƿ�������ⷶΧ��

    protected bool isGround;        // �ڵ�����
    protected bool isDodgeOver;     // ���ܽ���
    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        //״̬���
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();       
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isGround = entity.CheckGround();
    }

    public override void Enter()
    {
        base.Enter();

        isGround = false;

        // ��ʼ���ٶ�
        entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        //����ʱ�������ɫ�ڵ�����
        if(Time.time >= startTime + stateData.dodgeTime)
        {
            //Debug.Log("isGround");
            isDodgeOver = true;        
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
