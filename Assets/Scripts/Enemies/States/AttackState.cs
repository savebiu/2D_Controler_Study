using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ����������
 */
public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;     //�����Ƿ����
    protected bool isPlayerInMinAgroRange;      //����Ƿ�����С������Χ��
    protected bool isPlayerInMaxAgroRange;      //����Ƿ�����󹥻���Χ��
    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();     //�������Ƿ�����С������Χ��
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();

    }

    public override void Enter()
    {
        base.Enter();
        entity.atsm.attackState = this;     //����ǰ״̬���붯��״̬��
        isAnimationFinished = false;
        entity.SetVelocity(0);
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
    }

    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
