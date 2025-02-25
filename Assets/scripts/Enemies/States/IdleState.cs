using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 该脚本是怪物的空闲状态
 */

public class IdleState : State
{
    protected D_IdleState stateData;

    [Header("判断条件")]
    protected bool flipAfterImage;      //是否翻转
    protected bool isIdleTimeOver;      //空闲时间
    protected bool isPlayerInMinAgroRange;        //玩家是否在最小攻击范围内

    protected float IdleTime;       //跟踪进入Idle的时间
    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)     //( 当前状态, 状态机, )
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0f);     //速度为0进入
        isIdleTimeOver = false;
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();        //检测玩家是否在最小攻击范围内
        SetRandomIdleTiem();        //设置随机空闲时间
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterImage)
        {
            entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + IdleTime)
        {
            isIdleTimeOver = true;
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    //是否翻转
    public void SetFlipAfterImage(bool flip)
    {
        flipAfterImage = flip;
    }

    //空闲时间管理
    public void SetRandomIdleTiem()
    {
        IdleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);      //在最小空闲时间和最大空闲时间之间随机
    }
}
