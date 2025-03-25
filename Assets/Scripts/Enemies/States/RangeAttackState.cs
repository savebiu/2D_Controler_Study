
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Ô¶¾àÀë¹¥»÷×´Ì¬
 */
public class RangeAttackState : State
{
    D_RangeAttackState data;
    public RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_RangeAttackState data) : base(entity, stateMachine, animBoolName)
    {
        this.data = data;
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
