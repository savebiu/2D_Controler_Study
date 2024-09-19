using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    
    //����״̬
    private bool facingRight = true;
    //ǽ�ڼ��
    private bool isWalking = true;
    //������
    private bool isGround;
    //ǽ�ڼ��
    private bool isTouchingWall;
    private bool isWallSliding;
    //��Ծ����
    private bool canJump = false;
    private int jumpCount = 2;
    private float JumpHeightMultiplier = 0.5f;
    //��ǽ��    
    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    //����
    private bool isClimb;
    private bool canClimb = false;
    private bool ledgeDetected;
    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;



    private float moveX;
    private float moveY;

    [Header("��������")]
    //����
    public float movementSpeed = 7.0f;
    //������
    public float groundCheckRadius;
    public Transform GroundCheck;
    public LayerMask whatIsGround;
    //��Ծ�߶�
    public float jumpForce = 12f;
    //ǽ�ڼ��
    public float WallCheckDistance;
    public float WallSlidingSpeed;
    public Transform WallCheck;
    //��ǽ������
    public float wallHopForce;
    public float wallJumpForce;
    //�������
    public Transform ledgeCheck;
    public float ledgeClimbXOffset1 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //��ǽ����һ��
        //wallHopDirection.Normalize();
        //wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        //�ƶ�����
        Movement();
        CheckInput();
        UpdateAnimation();        
        CheckIfWallSliding();
        CheckLedgeClimb();
        HandleLedgeClimb();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }
    //������
    private void CheckInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        CheckIfJump();
    }

    //��������
    private void CheckSurroundings()
    {
        //������
        isGround = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        if (isGround)
        {
            jumpCount = 2;
        }
        if (isWallSliding)
        {
            jumpCount = 1;
        }
        //ǽ����
        isTouchingWall = Physics2D.Raycast(WallCheck.position, facingRight ? Vector2.right : Vector2.left, WallCheckDistance, whatIsGround);
        //�������
        isClimb = Physics2D.Raycast(ledgeCheck.position, facingRight ? Vector2.right : Vector2.left, WallCheckDistance, whatIsGround);
        if (isTouchingWall && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = ledgeCheck.position;
        }
    }
    //�ƶ�
    private void Movement()
    {
        //�ƶ�
        if(rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        //��ת
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
        if(!isGround && !isWallSliding && moveX == 0)
        {
            rb.velocity = new Vector2 (rb.velocity.x * JumpHeightMultiplier, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveX * movementSpeed, rb.velocity.y);
        }
        if (isWallSliding)
        {            
            if (rb.velocity.y < -WallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -WallSlidingSpeed);                
            }
        }        
    }
    //��ת
    private void Derection()
    {        
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    //��Ծ
    private void CheckIfJump()
    {
        //��Ծ
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
    //��Ծ
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
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);   // Ӧ�õ�ǽ����
            //��ǽʱ��ת��ɫ
            Derection();
        }
        else if ((isWallSliding || isTouchingWall) && moveX != 0 && canJump)
        {            
            isWallSliding = false;
            jumpCount--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * moveX, wallJumpForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);  // Ӧ�õ�ǽ����
        }
    }

    //ǽ�淴��
    private void CheckIfWallSliding()
    {
        if(isTouchingWall && !isGround && rb.velocity.y < 0 && !canClimb)
            isWallSliding = true;
        else
            isWallSliding = false;        
    }
    private void HandleLedgeClimb()
    {
        if (canClimb)
        {
            // ʹ�� MoveTowards ��ƽ���ƶ���ɫ�����¶���
            transform.position = Vector2.MoveTowards(transform.position, ledgePos1, movementSpeed * Time.deltaTime);

            // ����ɫ���� ledgePos1 λ�ã��������µ�����λ�� ledgePos2
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
            if(Input.GetButtonDown("Jump")) {
                canClimb = true;
                rb.velocity = Vector2.zero;
                anim.SetBool("IsClimb", canClimb);
                if (facingRight)
                {

                    ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + WallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                    ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + WallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
                }
                else
                {
                    ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x - WallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                    ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x - WallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
                }
            }
                                      
        }        
    }
    private void FinishClimb()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, ledgePos2, movementSpeed * Time.deltaTime);
        if(Vector2.Distance(rb.position, ledgePos2) < 0.1f)
            ledgeDetected = false;
            canClimb = false;
            anim.SetBool("IsClimb", canClimb);
    }
    //����
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
