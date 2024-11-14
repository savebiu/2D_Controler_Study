using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 该脚本是怪物的移动状态
 */
public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;       //检测墙壁
    protected bool isDetectingLedge;       //检测悬崖

    //base用来调用父类的构造函数,这里是调用State的构造函数,因为子类不能直接继承父类的构造函数
    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();       //base同时调用父类的Enter方法中的构造函数
        entity.SetVelocity(stateData.movementSpeed);       //设置实体速度

        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
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
        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
    }
}
