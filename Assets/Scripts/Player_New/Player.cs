using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D RB { get; private set; }                     //刚体
    public Animator Anim { get; private set; }                   //动画
    public PlayerInputHandle InputHandle { get; private set; }      //输入控制

    public PlayerStateMachine stateMachine { get; private set; }       //状态机

    public PlayerIdleState IdleState { get; private set; }             //空闲状态
    public PlayerMoveSate MoveState { get; private set; }              //移动状态

    [SerializeField]
    private PlayerData playerData;                                      //玩家数据

    private Vector2 workspace;                                          //工作空间
    private Vector2 currentVelocity;                                    //当前速度


    // 初始化状态机
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        MoveState = new PlayerMoveSate(this, stateMachine, playerData, "move");
    }

    // 初始化状态
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();    //获取刚体
        Anim = GetComponent<Animator>();   //获取动画
        InputHandle = GetComponent<PlayerInputHandle>();   //获取输入控制

        stateMachine.Initialize(IdleState); //初始化状态机为Idle状态

    }

    private void Update()
    {
        currentVelocity = RB.velocity;      //获取当前速度
        stateMachine.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    // 速度函数
    public void SetVelocityX(float veloicity)
    {
        workspace.Set(veloicity, currentVelocity.y);        // 设置速度
        RB.velocity = workspace;        //设置刚体速度
        currentVelocity = workspace;    //设置当前速度为工作空间中的值
    }
}
