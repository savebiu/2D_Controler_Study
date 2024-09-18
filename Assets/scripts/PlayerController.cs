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
    private bool isWallSliding;
    //跳跃参数
    private bool canJump = false;
    private int jumpCount = 2;
    private float JumpHeightMultiplier = 0.5f;
    //蹬墙跳
    public float wallHopForce;
    public float wallJumpForce;
    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

       
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

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //蹬墙跳归一化
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        //移动输入
        Movement();
        CheckInput();
        UpdateAnimation();
        
        CheckIfWallSliding();
    }
    void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }
    //输入检测
    private void CheckInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        CheckIfJump();
    }
 
    //移动
    private void Movement()
    {
        //移动
        if(rb.velocity.x != 0)
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
            Derection();
        }
        if (moveX < 0 && facingRight)
        {
            Derection();
        }
    }
    private void ApplyMovement()
    {
        rb.velocity = new Vector2(moveX * movementSpeed, rb.velocity.y);
     
        if (isWallSliding)
        {            
            if (rb.velocity.y < -WallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -WallSlidingSpeed);                
            }
        }        
    }
    //翻转
    private void Derection()
    {        
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    //跳跃
    private void CheckIfJump()
    {
        //跳跃
        if (Input.GetButtonDown("Jump"))
        {
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
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingRight, wallHopForce * wallHopDirection.y);
        }
        else if ((isWallSliding || isTouchingWall) && moveX != 0 && canJump)
        {
            isWallSliding = false;
            jumpCount--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * moveX, wallJumpForce * wallHopDirection.y);

        }
    }
    //地面反馈
    private void CheckSurroundings()
    {
        isGround = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        if (isGround)
        {
            jumpCount = 2;
        }
        isTouchingWall = Physics2D.Raycast(WallCheck.position, facingRight ? Vector2.right : Vector2.left, WallCheckDistance, whatIsGround);
    }
    //墙面反馈
    private void CheckIfWallSliding()
    {
        if(isTouchingWall && !isGround && rb.velocity.y < 0)
            isWallSliding = true;
        else
            isWallSliding = false;        
    }
    //动画
    private void UpdateAnimation()
    {
        anim.SetBool("IsWalking", isWalking);
        anim.SetBool("IsGround", isGround);
        anim.SetFloat("IsJump", rb.velocity.y);
        anim.SetBool ("IsWallSliding", isWallSliding);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + WallCheckDistance, WallCheck.position.y, WallCheck.position.y));
    }

    
}
