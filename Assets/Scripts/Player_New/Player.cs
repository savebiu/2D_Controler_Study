using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Path;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 组件
    public Rigidbody2D RB { get; private set; }                     //刚体
    public Animator Anim { get; private set; }                   //动画
    public PlayerInputHandle InputHandle { get; private set; }      //输入控制
    #endregion

    #region 状态机
    public PlayerStateMachine stateMachine { get; private set; }       //状态机

    public PlayerIdleState IdleState { get; private set; }             //空闲状态
    public PlayerMoveSate MoveState { get; private set; }              //移动状态
    public PlayerJumpState JumpState { get; private set; }             //跳跃状态
    public PlayerInAirState InAirState { get; private set; }           //空中状态
    public PlayerLanState LanState { get; private set; }               //落地状态

    #endregion

    public int FacingDerection { get; private set; }                   //方向 

    [SerializeField]
    private PlayerData playerData;                                      //玩家数据

    private Vector2 workspace;                                          //工作空间
    public Vector2 currentVelocity;                                    //当前速度

    [Header("检测")]
    public Transform groundCheck;       //检测地面 
    // public LayerMask whatLayer;
    private bool isGround;                                              //是否在地面
    private bool isWall;                                        //触碰到墙

    #region Unity回调函数
    // 初始化状态机
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, stateMachine, playerData, "idle");        //初始化Idle状态机
        MoveState = new PlayerMoveSate(this, stateMachine, playerData, "move");     //初始化Move状态机
        JumpState = new PlayerJumpState(this, stateMachine, playerData, "jump");        //初始化Jump状态机
        InAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");     //初始化InAir状态机
        LanState = new PlayerLanState(this, stateMachine, playerData, "land");       //初始化Lan状态机
    }

    // 初始化状态
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();    //获取刚体
        Anim = GetComponent<Animator>();   //获取动画
        InputHandle = GetComponent<PlayerInputHandle>();   //获取输入控制
        FacingDerection = 1;    //设置方向为正

        stateMachine.Initialize(IdleState); //初始化状态机为Idle状态

    }

    private void Update()
    {
        currentVelocity = RB.velocity;      //获取当前速度
        stateMachine.LogicUpdate();
        // Debug.Log(whatLayer);     // 地面检测调试
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    #endregion

    #region 速度函数
    // 水平速度函数(移动)
    public void SetVelocityX(float veloicity)
    {
        workspace.Set(veloicity, currentVelocity.y);        // 设置速度
        RB.velocity = workspace;        //设置刚体速度
        currentVelocity = workspace;    //设置当前速度为工作空间中的值
        
    }
    //竖直速度函数(跳跃)
    public void SetVelocityY(float velocity)
    {
        workspace.Set(currentVelocity.x, velocity);        // 设置速度
        RB.velocity = workspace;        //设置刚体速度
        currentVelocity = workspace;    //设置当前速度为工作空间中的值
    }

    #endregion

    #region 翻转函数
    // 翻转函数
    public void Flip()
    {
        FacingDerection *= -1;      //改变方向
        transform.Rotate(0.0f, 180.0f, 0.0f);      //旋转
    }
    #endregion

    #region 检查函数
    // 翻转检测
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput !=0 && xInput != FacingDerection)
        {
            Flip();
        }
    }

    // 检查是否在地面
    public bool CheckIfGrounded()
    {
        return isGround = Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);        // 地面检测,若在地面则返回true
        
        ////在地面上
        //if (isGround)
        //{
        //    playerData.amountOfJump = 2;
        //}
    }


    // 墙壁检测
    //public bool CheckWall()
    //{
    //    return isWall = Physics2D.Raycast(groundCheck.position, Vector2.right * FacingDerection, playerData.wallCheckDistance, playerData.whatIsGround);        // 墙壁检测,若在墙壁则返回true
    //}

    // 悬崖检测
    public void CheckLedge()
    {

    }
    #endregion

    private void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();        //动画触发状态
    private void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();      //动画结束触发状态

}
