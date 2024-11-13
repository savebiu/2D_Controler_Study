using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    //动画状态
    private bool facingRight = true;
    //墙壁检测
    private bool isWalking = true;
    //地面检测
    private bool isGround;
    //墙壁检测
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool isWallSliding;
    //跳跃参数
    private bool canJump = false;
    private int jumpCount = 2;
    private float JumpHeightMultiplier = 0.5f;
    //蹬墙跳    
    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    //run
    public bool isRun;
    //冲刺
    public bool isDashing;   
    private float dashTimeLeft;

    private float lastImageXpos;
    private float lastDash = -100;      //残影冷却时间
    /*//攀爬
    private bool isClimb;
    private bool canClimb = false;
    private bool ledgeDetected;
    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;*/

    private float moveX;
    private float moveY;

    [Header("基本参数")]
    //移速
    public float movementSpeed = 7.0f;
    //地面检测
    public float groundCheckRadius;
    public Transform GroundCheck;
    public LayerMask whatIsGround;
    //跳跃高度
    public float jumpForce = 12f;
    //墙壁检测
    public float WallCheckDistance;
    public float WallSlidingSpeed;
    public Transform WallCheck;
    //蹬墙跳力度
    public float wallHopForce;
    public float wallJumpForce;
    //冲刺参数
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages = 0.5f;
    public float dashCoolDown;
    float knockbackStartTime;   //被击打的开始时间
    bool knockback;

    [SerializeField]
    float knockbackDuration;   //被击打持续时间

    [SerializeField]
    [Header("击退速度")]
    Vector2 knockbackSpeed;   //被击退的速度

    /*//攀爬检测
    public Transform ledgeCheck;
    public float ledgeClimbXOffset1 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;*/ 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();        
        //蹬墙跳归一化
        //wallHopDirection.Normalize();
        //wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        //移动输入
        Movement();
        CheckInput();
        UpdateAnimation();        
        CheckIfWallSliding();
        CheckIfRun();
        CheckDash();
        CheckKnockBack();       
    }

    void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
       
    }

    //输入检测
    private void CheckInput()
    {
        if (!isDashing)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
        }        
        CheckIfJump();

        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))      //冷却完成
                AttemptToDash();
        }
    }

    //环境反馈
    private void CheckSurroundings()
    {
        //地面检测
        isGround = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        if (isGround)
        {
            jumpCount = 2;
        }
        if (isWallSliding)
        {
            jumpCount = 1;
        }
        //墙面检测
        isTouchingWall = Physics2D.Raycast(WallCheck.position, facingRight ? Vector2.right : Vector2.left, WallCheckDistance, whatIsGround);
        
        //isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, facingRight ? Vector2.right : Vector2.left, WallCheckDistance, whatIsGround);
        /*//攀爬检测
        isClimb = Physics2D.Raycast(ledgeCheck.position, facingRight ? Vector2.right : Vector2.left, WallCheckDistance, whatIsGround);
        if (isTouchingWall && isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = ledgeCheck.position;
        }*/
    }
    //移动
    private void Movement()
    {
        //移动
        if (Mathf.Abs(rb.velocity.x) > 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        //翻转
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        if (moveX < 0 && facingRight)
        {
            Flip();
        }       
    }

    //Run
    private void CheckIfRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && moveX !=0)
        {
            if (!isRun)
            {
                isRun = true;
                movementSpeed = movementSpeed * 1.5f;
            }
        }
        else
        {
            if(isRun)
            {
                isRun = false;
                movementSpeed = movementSpeed / 1.5f;
            }           
        }
    }

    private void ApplyMovement()
    {
        //击退
        if (knockback)      //在击退状态时,跳过其他状态的速度更新
            return;
        //跳跃
        if(!isGround && !isWallSliding && moveX == 0 && !knockback)
        {
            rb.velocity = new Vector2 (rb.velocity.x * JumpHeightMultiplier, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveX * movementSpeed, rb.velocity.y);
        }
        //滑墙
        if (isWallSliding && !knockback)
        {            
            if (rb.velocity.y < -WallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -WallSlidingSpeed);                
            }
        }
        //冲刺
        if (isDashing)
        {
            rb.velocity = new Vector2(dashSpeed * (facingRight ? 1 : -1), -rb.velocity.y);
            return;
        }

    }
    public void DisableFlip()
    {

    }
    public void EnableFlip()
    {

    }
    //翻转
    private void Flip()
    {        
        if(!isWallSliding && !knockback)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        
    }
    //跳跃
    private void CheckIfJump()
    {
        //跳跃
        if (Input.GetButtonDown("Jump"))
        {
            /*if (canClimb)
            {               
                canClimb = false;
                anim.SetBool("IsClimb", false);
            }*/
            if (jumpCount <= 0)
            {
                canJump = false;
            }
            else
            {
                canJump = true;
                Jump();
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * JumpHeightMultiplier);
        }
    }
    //跳跃
    private void Jump()
    {
        if (canJump && !isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }
        else if (isWallSliding && moveX == 0 && canJump)
        {
            isWallSliding = false;
            jumpCount--;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * (facingRight ? -1: 1), wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);   // 应用蹬墙跳力
            //蹬墙时翻转角色
            Flip();
        }
        else if ((isWallSliding || isTouchingWall) && moveX != 0 && canJump)
        {            
            isWallSliding = false;
            jumpCount--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * moveX, wallJumpForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);  // 应用蹬墙跳力
        }
    }

    //墙面反馈
    private void CheckIfWallSliding()
    {
        if(isTouchingWall && !isGround && rb.velocity.y < 0 /*&& !canClimb*/)
            isWallSliding = true;
        else
            isWallSliding = false;        
    }
    //冲刺
    private void CheckDash()
    {        
        if (isDashing)
        {
            if(dashTimeLeft > 0)
            {
                isWalking = false;
                

                dashTimeLeft -= Time.deltaTime;
                //生成拖影位置
                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    Debug.Log("生成残影");
                    PlayerAfterPol.Instance.GetFromPool();                    
                    lastImageXpos = transform.position.x;
                }
            }

            if(dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;
                isWalking = true;
            }
        }
    }
    //冲刺冷却时间
    private void AttemptToDash()
    {   
        //如果冷却时间完成
        if(Time.time >=(lastDash + dashCoolDown))
        {
            isDashing = true;
            dashTimeLeft = dashTime;
            lastDash = Time.time;            
            PlayerAfterPol.Instance.GetFromPool();
            lastDash = Time.time;
            //lastImageXpos = transform.position.x;
        }        
    }
    //翻转角色
    public int GetFacingDirection()
    {
        return facingRight ? 1 :-1;
    }

    //被击打反馈
    public void KnockBack(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;        
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void CheckKnockBack()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    //冲刺状态检测
    public bool GetDashStatus()
    {
        return isDashing;
    }

    /*
    //攀爬
    private void HandleLedgeClimb()
    {
        if (canClimb)
        {
            // 使用 MoveTowards 来平滑移动角色到悬崖顶部
            transform.position = Vector2.MoveTowards(transform.position, ledgePos1, movementSpeed * Time.deltaTime);

            // 当角色到达 ledgePos1 位置，进入悬崖的最终位置 ledgePos2
            if (Vector2.Distance(transform.position, ledgePos1) < 0.1f)
            {
                FinishClimb();
            }
        }
    }

    private void CheckLedgeClimb()
    {
        if (ledgeDetected && !canClimb)
        {
            canClimb = true;
            rb.velocity = Vector2.zero;
            anim.SetBool("IsClimb", true);
            if (facingRight)
            {

                ledgePos1 = new Vector2(ledgePosBot.x + WallCheckDistance - ledgeClimbXOffset1, ledgePosBot.y + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(ledgePosBot.x + WallCheckDistance + ledgeClimbXOffset2, ledgePosBot.y + ledgeClimbYOffset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x - WallCheckDistance) + ledgeClimbXOffset1, ledgePosBot.y + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x - WallCheckDistance) - ledgeClimbXOffset2, ledgePosBot.y + ledgeClimbYOffset2);
            }

        }
    }
    private void FinishClimb()
    {
        transform.position = Vector2.MoveTowards(transform.position, ledgePos2, movementSpeed * Time.deltaTime);
        if (Vector2.Distance(rb.position, ledgePos2) < 0.1f)
            ledgeDetected = false;
        canClimb = false;
        anim.SetBool("IsClimb", false);
    }*/
    //动画
    private void UpdateAnimation()
    {
        anim.SetBool("IsWalking", isWalking);
        anim.SetBool("IsGround", isGround);
        anim.SetFloat("IsJump", rb.velocity.y);
        anim.SetBool ("IsWallSliding", isWallSliding);
        anim.SetBool("IsRun", isRun);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + WallCheckDistance, WallCheck.position.y, WallCheck.position.y));
    } 


}
