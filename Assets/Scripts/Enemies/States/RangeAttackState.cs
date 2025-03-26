
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Ô¶¾àÀë¹¥»÷×´Ì¬
 */
public class RangeAttackState : AttackState
{
    protected D_RangeAttackState stateData;      //Ô¶¾àÀë¹¥»÷×´Ì¬Êý¾Ý
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

        // Éú³ÉÍ¶ÉäÎï
        GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
    }
}
