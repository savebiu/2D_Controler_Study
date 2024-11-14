using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 此脚本负责管理所有实体的状态机
 */
public class FiniteStateMachine //该脚本不调用Unity对象,所以不需继承MonoBehaviour
{
    //跟踪当前状态
    public State currentState { get; private set; }

    //设置游戏开始时的最新状态
    public void Initialize(State startingState)
    {
        
        currentState = startingState;       //初始状态
        currentState.Enter();       //调用当前状态的Enter方法
    }

    //改变状态
    public void ChangeState(State newState)
    {
        currentState.Exit();        //退出上一状态
        currentState = newState;        //状态改变
        currentState.Enter();
        
    }
}
