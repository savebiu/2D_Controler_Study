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
    private float Head;
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
    [SerializeField]
    float 
        groundCheckDistance,
        wallCheckDistance;

    [SerializeField]
    Transform
        groundCheck,   //�������
        wallCheck,   //ǽ�ڼ���
        touchDamageCheck;   //�������

    //�ƶ�
    Vector2 movement;
    [SerializeField]
    float movementSpeed;

    //Ѫ��
    [SerializeField]
    float
        maxHealth,   //���Ѫ��
        currentHealth,   //����Ѫ��
        knockbackStartTime,   //����ʱ��ʱ��
        knockbackDuration,
        lastTouchDamageTime,   //�ϴα�������ʱ��
        touchDamageCooldown,   //�˺���ȴʱ��
        touchDamage,   //���˺�������
        touchDamageWidth,
        touchDamageHeight;

    //���
    bool groundDetected;    //������
    bool wallDetected;      //ǽ�ڼ��
    int facingDerection;
    int damageDirection;    //���˷���


    //ͼ����
    [SerializeField]
    LayerMask
        whatIsGround,   //������
        whatIsPlayer;   //��Ҽ��

    [SerializeField]
    Vector2
        knockbackSpeed,
        touchDamageBotLeft,   //�ص�����(���½�)
        touchDamageTopRight;    //�ص�����(���Ͻ�)

    [SerializeField]
    float[] attackDetails = new float[2];   //������Ϣ����

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
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState(); 
                break;
            case State.Dead:
                UpdateDeadState();
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
    void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatIsGround);

        CheckTouchDamage();

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
    void UpdateKnockbackState()
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
    void UpdateDeadState()
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
        wallCheckDistance *= -1;
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

    private void CheckTouchDamage()
    {
        //�����ϴι����ѹ��ܳ�ʱ��
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            //����Ƿ���ͼ���ص�����
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            //�����˶Թ�������ĸ�������й���
            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }


    //��ǽת��
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x,groundCheck.position.y-groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        //�����ĸ���ײ��
        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }
}
