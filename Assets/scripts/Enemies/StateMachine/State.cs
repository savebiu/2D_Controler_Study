using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 此脚本负责管理所有实体的状态
 * 
 */
public class State
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    protected float startTime;      //开始时间

    protected string animBoolName;      //动画布尔值

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
}
