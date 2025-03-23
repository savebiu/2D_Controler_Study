using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    protected D_DodgeState stateData;
    protected bool performCloseRangeAction;     // 是否执行近距离动作
    protected bool isPlayerInMaxAgroRange;      // 玩家是否在最大检测范围内

    protected bool isGround;        // 在地面上
    protected bool isDodgeOver;     // 闪避结束
    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        //状态检测
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();       
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isGround = entity.CheckGround();
    }

    public override void Enter()
    {
        base.Enter();

        isGround = false;

        // 初始化速度
        entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        //闪避时间结束角色在地面上
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
