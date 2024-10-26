using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BasicsEnemyController : MonoBehaviour
{
    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;

    [Header("测试")]
    public float Head;
    //状态
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }
    //当前状态
    State currenState;

    //检测参数
    //检测距离
    [SerializeField]
    float groundCheckDistance;
    [SerializeField]
    float wallCheckDistance;

    [SerializeField]
    Transform groundCheck;  //地面检测点
    [SerializeField]
    Transform wallCheck;   //墙壁检测点
    [SerializeField]
    LayerMask whatIsGround; //地面检测

    //移动
    Vector2 movement;
    [SerializeField]
    float movementSpeed;

    //血量
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float currentHealth;
    float knockbackStartTime;
    [SerializeField]
    float knockbackDuration;
    [SerializeField]
    Vector2 knockbackSpeed;


    //检测
    bool groundDetected;    //地面检测
    bool wallDetected;      //墙壁检测
    int facingDerection;    
    int damageDirection;    //损伤方向

    //粒子系统
    [SerializeField]
    GameObject
        hitParticle,
        deathChunkParticle,
        deathBloodParticle;

    
    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();

        currentHealth = maxHealth;
        facingDerection = 1;
    }
    private void Update()
    {
        //状态更新控制器
        switch (currenState)
        {
            case State.Moving:
                UpdataMovingState();
                break;
            case State.Knockback:
                UpdataKnockbackState(); 
                break;
            case State.Dead:
                UpdataDeadState();
                break;
        }
        
    }
    //状态交换器------------------------------------------------------
    void SwitchState(State state)
    {
        //状态进入控制器
        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }
        //状态退出控制器
        switch (currenState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }
        currenState = state;    //转换为我们的当前状态
    }
    //三个状态机:进入, 更新, 退出
    //移动------------------------------------------------------
    void EnterMovingState()
    {

    }
    void UpdataMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);
        if(!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDerection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        }
    }
    void ExitMovingState()
    {

    }
    //攻击------------------------------------------------------
    void EnterKnockbackState()
    {
        //时间跟踪
        knockbackStartTime = Time.time;     //确定被攻击时间
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);   //动画切换

    }
    void UpdataKnockbackState()
    {
        //完成击退效果
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }
    void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }
    //死亡------------------------------------------------------
    void EnterDeadState()
    {
        //生成粒子效果
        Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }
    void UpdataDeadState()
    {

    }
    void ExitDeadState()
    {

    }
    
    //翻转
    void Flip()
    {
        facingDerection *= -1;
        alive.transform.Rotate(0, 180f, 0);        
    }
    //损伤
    void Damage(float[] attackDeteils)
    {
        currentHealth -= attackDeteils[0];
        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360f)));   //实例化攻击特效,且随即旋转角度

        //根据攻击者位置判断估计方向
        if (attackDeteils[1] > alive.transform.position.x)   //攻击者的位置大于怪物位置,则受伤方向为负
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }
        //攻击粒子效果
        if (currentHealth > 0f)   //怪物有血量,进入击退状态
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0f)   //怪物无血量,死亡
        {
            SwitchState(State.Dead);
        }
    }
    //遇到墙壁转换方向

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x,groundCheck.position.y-groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x, groundCheck.position.y));
    }
}
