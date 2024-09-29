using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BasicsEnemyController : MonoBehaviour
{
    GameObject alive;

    private enum State
    {
        Wallking,
        Knockback,
        Dead
    }
    //当前状态
    State currenState;

    //检测参数
    //检测距离
    [SerializeField]
    float groundCheckDistance;
    float wallCheckDistance;
    [SerializeField]
    Transform groundCheck;  //地面检测点
    Transform wallCheck;   //墙壁检测点
    [SerializeField]
    LayerMask whatIsGround;

    //移动
    Vector2 movement;
    [SerializeField]
    float movementSpeed;

    //血量
    [SerializeField]
    float maxHealth;
    float currentHealth;
    float knockbackStartTime;
    float knockbackDuration;
    Vector2 knockbackSpeed;


    //检测
    bool groundDetected;    //地面检测
    bool wallDetected;      //墙壁检测
    
    int facingDerection;    
    int damageDirection;    //损伤方向
    Rigidbody2D aliveRb;
    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = GetComponent<Rigidbody2D>();
        facingDerection = 1;
    }
    private void Update()
    {
        //状态更新控制器
        switch (currenState)
        {
            case State.Wallking:
                UpdataWalkingState();
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
            case State.Wallking:
                EnterWalkingState();
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
            case State.Wallking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }
    }
    //三个状态机:进入, 更新, 退出
    //移动------------------------------------------------------
    void EnterWalkingState()
    {

    }
    void UpdataWalkingState()
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
    void ExitWalkingState()
    {

    }
    //攻击------------------------------------------------------
    void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;     //击退
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;

    }
    void UpdataKnockbackState()
    {

    }
    void ExitKnockbackState()
    {

    }
    //死亡------------------------------------------------------
    void EnterDeadState()
    {

    }
    void UpdataDeadState()
    {

    }
    void ExitDeadState()
    {

    }
    
    //滑动
    void Flip()
    {
        facingDerection *= -1;
        alive.transform.Rotate(0, 180f, 0);
    }
    //损伤
    void Damage(float[] attackDeteils)
    {
        currentHealth -= attackDeteils[0];
        //根据攻击者位置判断估计方向
        if (attackDeteils[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }
        //攻击粒子效果
        if (currentHealth > 0f)
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0f)
        {
            SwitchState(State.Dead);
        }
    }
    //遇到墙壁转换方向
}
