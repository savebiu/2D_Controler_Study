using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_StunState : StunState
{
    Enemy1 enemy;

    public E1_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

        if (isStunTimeOver)
        {
            //在近战范围内
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);       //转换为 近战攻击状态
            }
            //在最小攻击范围
            else if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.chargeState);        //转换为 冲刺状态
            }
            //寻找
            else
            {
                enemy.lookForPlayerState.SetTurnImediately(true);       //立即转向
                stateMachine.ChangeState(enemy.lookForPlayerState);      //转换为 寻找玩家状态
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
