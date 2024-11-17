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
    protected bool FlipAfterImage;      //是否翻转
    protected bool isIdleTimeOver;      //是否空闲时间结束

    protected float IdleTime;       //跟踪进入Idle的时间
    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0f);     //速度为0进入
        isIdleTimeOver = false;     
        SetRandomIdleTiem();        //设置随机空闲时间
    }

    public override void Exit()
    {
        base.Exit();

        if (FlipAfterImage)
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
    }

    //是否翻转
    public void SetFlipAfterImage(bool flip)
    {
        FlipAfterImage = flip;
    }

    //
    public void SetRandomIdleTiem()
    {
        IdleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
