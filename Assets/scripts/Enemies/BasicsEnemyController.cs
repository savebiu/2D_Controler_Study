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
    //��ǰ״̬
    State currenState;

    //������
    //������
    [SerializeField]
    float groundCheckDistance;
    float wallCheckDistance;
    [SerializeField]
    Transform groundCheck;  //�������
    Transform wallCheck;   //ǽ�ڼ���
    [SerializeField]
    LayerMask whatIsGround;

    //�ƶ�
    Vector2 movement;
    [SerializeField]
    float movementSpeed;

    //Ѫ��
    [SerializeField]
    float maxHealth;
    float currentHealth;
    float knockbackStartTime;
    float knockbackDuration;
    Vector2 knockbackSpeed;


    //���
    bool groundDetected;    //������
    bool wallDetected;      //ǽ�ڼ��
    
    int facingDerection;    
    int damageDirection;    //���˷���
    Rigidbody2D aliveRb;
    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = GetComponent<Rigidbody2D>();
        facingDerection = 1;
    }
    private void Update()
    {
        //״̬���¿�����
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
    //״̬������------------------------------------------------------
    void SwitchState(State state)
    {
        //״̬���������
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
        //״̬�˳�������
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
    //����״̬��:����, ����, �˳�
    //�ƶ�------------------------------------------------------
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
    //����------------------------------------------------------
    void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;     //����
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;

    }
    void UpdataKnockbackState()
    {

    }
    void ExitKnockbackState()
    {

    }
    //����------------------------------------------------------
    void EnterDeadState()
    {

    }
    void UpdataDeadState()
    {

    }
    void ExitDeadState()
    {

    }
    
    //����
    void Flip()
    {
        facingDerection *= -1;
        alive.transform.Rotate(0, 180f, 0);
    }
    //����
    void Damage(float[] attackDeteils)
    {
        currentHealth -= attackDeteils[0];
        //���ݹ�����λ���жϹ��Ʒ���
        if (attackDeteils[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }
        //��������Ч��
        if (currentHealth > 0f)
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0f)
        {
            SwitchState(State.Dead);
        }
    }
    //����ǽ��ת������
}
