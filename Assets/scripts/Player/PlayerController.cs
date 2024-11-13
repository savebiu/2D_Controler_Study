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
    private bool isTouchingLedge;
    private bool isWallSliding;
    //��Ծ����
    private bool canJump = false;
    private int jumpCount = 2;
    private float JumpHeightMultiplier = 0.5f;
    //��ǽ��    
    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    //run
    public bool isRun;
    //���
    public bool isDashing;   
    private float dashTimeLeft;

    private float lastImageXpos;
    private float lastDash = -100;      //��Ӱ��ȴʱ��
    /*//����
    private bool isClimb;
    private bool canClimb = false;
    private bool ledgeDetected;
    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;*/

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
    //��̲���
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages = 0.5f;
    public float dashCoolDown;
    float knockbackStartTime;   //������Ŀ�ʼʱ��
    bool knockback;

    [SerializeField]
    float knockbackDuration;   //���������ʱ��

    [SerializeField]
    [Header("�����ٶ�")]
    Vector2 knockbackSpeed;   //�����˵��ٶ�

    /*//�������
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
        CheckIfRun();
        CheckDash();
        CheckKnockBack();       
    }

    void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
       
    }

    //������
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
            if (Time.time >= (lastDash + dashCoolDown))      //��ȴ���
                AttemptToDash();
        }
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
        
        //isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, facingRight ? Vector2.right : Vector2.left, WallCheckDistance, whatIsGround);
        /*//�������
        isClimb = Physics2D.Raycast(ledgeCheck.position, facingRight ? Vector2.right : Vector2.left, WallCheckDistance, whatIsGround);
        if (isTouchingWall && isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = ledgeCheck.position;
        }*/
    }
    //�ƶ�
    private void Movement()
    {
        //�ƶ�
        if (Mathf.Abs(rb.velocity.x) > 0.01f)
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
        //����
        if (knockback)      //�ڻ���״̬ʱ,��������״̬���ٶȸ���
            return;
        //��Ծ
        if(!isGround && !isWallSliding && moveX == 0 && !knockback)
        {
            rb.velocity = new Vector2 (rb.velocity.x * JumpHeightMultiplier, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveX * movementSpeed, rb.velocity.y);
        }
        //��ǽ
        if (isWallSliding && !knockback)
        {            
            if (rb.velocity.y < -WallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -WallSlidingSpeed);                
            }
        }
        //���
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
    //��ת
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
    //��Ծ
    private void CheckIfJump()
    {
        //��Ծ
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
            Flip();
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
        if(isTouchingWall && !isGround && rb.velocity.y < 0 /*&& !canClimb*/)
            isWallSliding = true;
        else
            isWallSliding = false;        
    }
    //���
    private void CheckDash()
    {        
        if (isDashing)
        {
            if(dashTimeLeft > 0)
            {
                isWalking = false;
                

                dashTimeLeft -= Time.deltaTime;
                //������Ӱλ��
                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    Debug.Log("���ɲ�Ӱ");
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
    //�����ȴʱ��
    private void AttemptToDash()
    {   
        //�����ȴʱ�����
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
    //��ת��ɫ
    public int GetFacingDirection()
    {
        return facingRight ? 1 :-1;
    }

    //��������
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

    //���״̬���
    public bool GetDashStatus()
    {
        return isDashing;
    }

    /*
    //����
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
    //����
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
