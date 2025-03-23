using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : AttackState
{
    protected D_MeleeState stateData;
    protected bool performCloseRangeAction;        //�Ƿ�ִ�н����붯��
    protected AttackDetails attackDetails;
    public MeleeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
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
        attackDetails.damageAmount = stateData.attackDamage;        //��ʼ���˺���ֵ
        attackDetails.position = entity.aliveGO.transform.position;  //��ʼ���˺���λ��
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        //���һ��2DԲ����ײ��
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);
        //��ÿ�������������巢�͸�����
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }
}
