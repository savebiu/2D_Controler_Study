using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

/*
 * 寻找玩家状态
 */
public class LookForPlayerState : State
{
    protected D_LookForPlayerState stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;      //所有翻转时间完成
    protected bool turnImmediately;     //立即翻转

    protected float lastTurnTime;       //上次翻转时间

    public int amountOfTurnsDone;

    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        /*
         * 初始化进入状态
         */
        isAllTurnsDone = false;         //所有翻转未完成
        isAllTurnsTimeDone = false;     //所有翻转时间未启用

        lastTurnTime = startTime;       //将开始时间设置为上次翻转时间
        amountOfTurnsDone = 0;          //完成的翻转数量为0
        entity.SetVelocity(0);          //速度为0
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (turnImmediately)
        {
            entity.Flip();
            lastTurnTime = Time.time;   
            amountOfTurnsDone++;        //反转数量加一
            turnImmediately = false;     //禁止立即翻转
        }
        else if(!isAllTurnsDone && Time.time >= lastTurnTime + stateData.TimeBetweenTurns)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        //判断是否完成所有翻转检查
        if(amountOfTurnsDone >= stateData.amountOfTurns)        //完成翻转的数量 >= 总翻转数量
        {
            isAllTurnsDone = true;
        }
        if (isAllTurnsDone && Time.time >= lastTurnTime + stateData.TimeBetweenTurns)      //所有翻转完成 && 反转冷却结束
        {
            isAllTurnsDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void SetTurnImediately(bool flip)
    {
        turnImmediately = flip;
    }
}
