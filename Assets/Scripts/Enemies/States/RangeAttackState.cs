
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Զ���빥��״̬
 */
public class RangeAttackState : AttackState
{
    protected D_RangeAttackState stateData;      //Զ���빥��״̬����
    protected GameObject projectile;        //Ͷ����
    protected Projectile projectileScript;       //Ͷ�����߼��ű�

    private bool hasTriggeredAttack = false; // ���һ����־λ��ȷ�� TriggerAttack ֻ������һ��


    public RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
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

        hasTriggeredAttack = true;
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

        Debug.Log("����Զ�̹���");
        // ����Ͷ����
        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);

        hasTriggeredAttack = true; // ���ñ�־λ��ȷ�� TriggerAttack ֻ������һ��
    }
}
