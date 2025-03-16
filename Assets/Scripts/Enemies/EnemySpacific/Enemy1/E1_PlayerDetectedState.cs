using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;


/* 玩家状态转换类
 * 未检测到时enemy1转到Idle状态
 * 检测到时enemy1从Idle转到PlayerDetected状态
 * 
 */
public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    

    public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateDate, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateDate)
    {
        this.enemy = enemy;
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

        /*//如果玩家不在最大攻击范围内则转换到Idle状态
        if (!isPlayerInMaxAgroRange)
        {
            enemy.idleState.SetFlipAfterImage(false);       //不希望翻转
            stateMachine.ChangeState(enemy.idleState);
        }*/

        //在近战范围内        
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        
        //如果敌人在远距离,则执行冲锋状态
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }

        //遇到悬崖和墙壁
        else if (!isDetectedLedge)
        {
            entity.Flip();      //翻转
            stateMachine.ChangeState(enemy.moveState);
        }

        //如果敌人不在最大攻击范围内,则转换到寻敌状态
        else if (!isPlayerInMaxAgroRange)   
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
