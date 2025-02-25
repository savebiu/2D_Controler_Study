using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;

    public override void Start()
    {
        base.Start();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);      //创建移动状态
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);      //创建空闲状态
        
        stateMachine.Initialize(moveState);     //初始化状态机
    }
}
