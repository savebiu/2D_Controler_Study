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
    private bool isWalking = true;
    private bool canJump = false;
    private bool isGround;
    private bool isJump;
    private int jumpCount = 2;
    private bool isTouchingWall;
    private bool isWallSliding;

    private float moveX;
    private float moveY;

    [Header("基本参数")]
    public float movementSpeed = 7.0f;
    public float jumpForce = 12f;
    public float WallSlidingSpeed;
    public float groundCheckRadius;
    public Transform GroundCheck;
    public float WallCheckDistance;
    public Transform WallCheck;
    public LayerMask whatIsGround;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //移动输入
        Movement();
        CheckInput();
        UpdateAnimation();
        CheckIfJump();
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
        //墙面下滑
    }
    private void ApplyMovement()
    {
        rb.velocity = new Vector2(moveX * movementSpeed, rb.velocity.y);
        if(isWallSliding)
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
        if (Input.GetKeyDown(KeyCode.Space))
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
    }
    //跳跃
    private void Jump()
    {
        if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
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
        Debug.Log(isWallSliding);
    }
    //动画
    private void UpdateAnimation()
    {
        anim.SetBool("IsWalking", isWalking);
        anim.SetBool("IsGround", isGround);
        anim.SetFloat("IsJump", rb.velocity.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + WallCheckDistance, WallCheck.position.y, WallCheck.position.y));
    }

    
}
