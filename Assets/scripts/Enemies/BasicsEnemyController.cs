using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BasicsEnemyController : MonoBehaviour
{
    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;

    [Header("����")]
    public float Head;
    //״̬
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }
    //��ǰ״̬
    State currenState;

    //������
    //������
    [SerializeField]
    float groundCheckDistance;
    [SerializeField]
    float wallCheckDistance;

    [SerializeField]
    Transform groundCheck;  //�������
    [SerializeField]
    Transform wallCheck;   //ǽ�ڼ���
    [SerializeField]
    LayerMask whatIsGround; //������

    //�ƶ�
    Vector2 movement;
    [SerializeField]
    float movementSpeed;

    //Ѫ��
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float currentHealth;
    float knockbackStartTime;
    [SerializeField]
    float knockbackDuration;
    [SerializeField]
    Vector2 knockbackSpeed;


    //���
    bool groundDetected;    //������
    bool wallDetected;      //ǽ�ڼ��
    int facingDerection;    
    int damageDirection;    //���˷���

    //����ϵͳ
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
        //״̬���¿�����
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
    //״̬������------------------------------------------------------
    void SwitchState(State state)
    {
        //״̬���������
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
        //״̬�˳�������
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
        currenState = state;    //ת��Ϊ���ǵĵ�ǰ״̬
    }
    //����״̬��:����, ����, �˳�
    //�ƶ�------------------------------------------------------
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
    //����------------------------------------------------------
    void EnterKnockbackState()
    {
        //ʱ�����
        knockbackStartTime = Time.time;     //ȷ��������ʱ��
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);   //�����л�

    }
    void UpdataKnockbackState()
    {
        //��ɻ���Ч��
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }
    void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }
    //����------------------------------------------------------
    void EnterDeadState()
    {
        //��������Ч��
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
    
    //��ת
    void Flip()
    {
        facingDerection *= -1;
        alive.transform.Rotate(0, 180f, 0);        
    }
    //����
    void Damage(float[] attackDeteils)
    {
        currentHealth -= attackDeteils[0];
        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360f)));   //ʵ����������Ч,���漴��ת�Ƕ�

        //���ݹ�����λ���жϹ��Ʒ���
        if (attackDeteils[1] > alive.transform.position.x)   //�����ߵ�λ�ô��ڹ���λ��,�����˷���Ϊ��
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }
        //��������Ч��
        if (currentHealth > 0f)   //������Ѫ��,�������״̬
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0f)   //������Ѫ��,����
        {
            SwitchState(State.Dead);
        }
    }
    //����ǽ��ת������

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x,groundCheck.position.y-groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x, groundCheck.position.y));
    }
}
