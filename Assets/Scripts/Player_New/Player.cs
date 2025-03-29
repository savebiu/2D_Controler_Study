using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine { get; private set; }       //״̬��

    // ��ʼ��״̬��
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
    }

    // ��ʼ��״̬
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
