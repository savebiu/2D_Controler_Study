using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    public D_PlayerDetected stateData;      //引入检测数据

    protected bool isPlayerInMinAgroRange;      //玩家是否在最小攻击范围内
    protected bool isPlayerInMaxAgroRange;      //玩家是否在最大攻击范围内

    protected bool performCloseRangeAction;        //执行近距离攻击
    protected bool performLongRangeAction;      //执行远程攻击

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateDate) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateDate;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();        //检测玩家是否在最小攻击范围内
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();        //检测玩家是否在最大攻击范围内

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();      //检测玩家是否在近距离攻击范围内
    }

    //检测到玩家实体后进入状态
    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0);      //设置速度为0
        performLongRangeAction = false;        //执行远距离动作
        //performCloseRangeAction = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //玩家执行远距离攻击
        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }

        //TODO：转换为攻击状态
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
