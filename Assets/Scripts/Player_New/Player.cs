using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine { get; private set; }       //状态机

    // 初始化状态机
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
    }

    // 初始化状态
    private void Start()
    {
        
    }

    private void Update()
    {
        stateMachine.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
