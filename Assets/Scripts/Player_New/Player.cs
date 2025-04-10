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
    public PlayerWallSlideState WallSlideState { get; private set; }   //墙壁滑行状态
    public PlayerWallClimbState WallClimbState { get; private set; }   //墙壁攀爬状态
    public PlayerWallGrabState WallGrabState { get; private set; }     //墙壁抓取状态
    public PlayerWallJumpState WallJumpState { get; private set; }         //墙壁跳跃状态
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }     //悬崖攀爬状态

    #endregion

    public int FacingDirection { get; private set; }                   //方向 

    [SerializeField]
    private PlayerData playerData;                                      //玩家数据

    private Vector2 workspace;                                          //工作空间
    public Vector2 currentVelocity;                                    //当前速度

    [Header("检测")]
    [SerializeField]
    public Transform groundCheck;       //检测地面 
    [SerializeField]
    public Transform wallCheck;         //检测墙壁
    [SerializeField]
    public Transform ledgeCheck;        //悬崖检测

    private bool isGround;                                              //是否在地面
    private bool isTouchingWall;                                        //触碰到墙
    private bool isTouchingWallBack;       //触碰到背后墙壁
    private bool isTouchingLedge;       //是否在悬崖


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
        WallSlideState = new PlayerWallSlideState(this, stateMachine, playerData, "wallSlide");     //初始化WallSlide状态机
        WallClimbState = new PlayerWallClimbState(this, stateMachine, playerData, "wallClimb");     //初始化WallClimb状态机
        WallGrabState = new PlayerWallGrabState(this, stateMachine, playerData, "wallGrab");     //初始化WallGrab状态机
        WallJumpState = new PlayerWallJumpState(this, stateMachine, playerData, "inAir");     //初始化WallJump状态机
        LedgeClimbState = new PlayerLedgeClimbState(this, stateMachine, playerData, "ledgeClimb");     //初始化LedgeClimb状态机
    }

    // 初始化状态
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();    //获取刚体
        Anim = GetComponent<Animator>();   //获取动画
        InputHandle = GetComponent<PlayerInputHandle>();   //获取输入控制
        FacingDirection = 1;    //设置方向为正

        stateMachine.Initialize(IdleState); //初始化状态机为Idle状态
       
    }

    private void Update()
    {
        currentVelocity = RB.velocity;      //获取当前速度
        stateMachine.LogicUpdate();
        // Debug.Log(whatLayer);     // 地面检测调试
        isTouchingWall = CheckIfTouchingWall();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    #endregion

    #region 速度函数
    // 设置为0的速度函数
    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;        //设置刚体速度为0
        currentVelocity = Vector2.zero;    //设置当前速度0
    }

    // 有方向角度的速度
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();        //归一化角度
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);        //设置速度
        RB.velocity = workspace;        //设置刚体速度
        currentVelocity = workspace;    //设置当前速度为工作空间中的值
    }


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
        FacingDirection *= -1;      //改变方向
        transform.Rotate(0.0f, 180.0f, 0.0f);      //旋转
    }
    #endregion

    #region 检查函数
    // 翻转检测
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput !=0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    // 检查是否在地面
    public bool CheckIfGrounded()
    {
        return isGround = Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);        // 地面检测,若在地面则返回true       
    }

    //墙壁检测
    public bool CheckIfTouchingWall()
    {
        return isTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);        // 墙壁检测,若在墙壁则返回true
    }
    //背后墙壁检测
    public bool CheckIfTouchingWallBack()
    {
        return isTouchingWallBack = Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);        // 背后墙壁检测,若在墙壁则返回true        
    }

    // 悬崖检测
    public bool CheckIfTouchingLedge()
    {
        return isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);        // 悬崖检测,若在悬崖则返回true
    }

    // 悬崖角落检测
    public Vector2 DeterminCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);        // 检测墙壁
        float xDist = xHit.distance;        // 获取墙壁距离
        RaycastHit2D vHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workspace), Vector2.down, ledgeCheck.position.y - ledgeCheck.position.y, playerData.whatIsGround);        
        float yDist = vHit.distance;        // 获取悬崖距离

        workspace.Set(wallCheck.position.x + (xDist * FacingDirection), ledgeCheck.position.y - yDist);        // 设置工作空间
        return workspace;        // 返回工作空间
    }
    #endregion

    private void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();        //动画触发状态
    private void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();      //动画结束触发状态

}
