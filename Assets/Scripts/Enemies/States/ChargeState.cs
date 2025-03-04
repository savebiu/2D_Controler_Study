using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 在最小攻击范围内检测到玩家时，怪物将进入冲刺状态
 * 如果遇到边缘和墙壁，则停止冲锋，返回Idle状态
 * 
 */
public class ChargeState : State
{
    protected D_ChargeState stateData;
    protected bool isPlayerInMinAgroRange;      // 玩家是否在最小攻击范围内
    protected bool isDetectingLedge;        // 检测到悬崖
    protected bool isDetectingWall;     //检测到墙壁
    protected bool isChargeTimeOver;        //冲刺时间是否结束

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();        //如果在最小攻击范围内
        isDetectingLedge = entity.CheckLedge();        //检测悬崖
        isDetectingWall = entity.CheckWall();       //检测墙壁
    }

    public override void Enter()
    {
        base.Enter();
        isChargeTimeOver = false;       //冲锋时间未结束
        entity.SetVelocity(stateData.chargeSpeed);        //设置冲刺速度
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
       
        //冲锋条件
        if(Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
