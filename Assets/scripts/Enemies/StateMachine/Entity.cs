using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/*
 * ʵ����ű����˽ű������������ʵ��Ļ�������
 * �����ű�
 * �ô����д���й��ﶼӵ�е�����
 * ��ͬ����:
 *      --����rigibody
 *      --����������Animator
 *      --������aliveGo
 */
public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;

    public Rigidbody2D rb { get; private set; }     //����
    public Animator anim { get; private set; }      //����������
    public GameObject aliveGO{ get; private set; }     //������

    public int lastDamageDirection { get; private set; }     //�ϴι������� 
    public float facingDirection { get; private set; }      //����ĳ���
    public AnimationToStatemachine atsm { get; private set; }        //����״̬��

    public Vector2 velocityWorkSpace;       //�ٶȹ����ռ�

    //[SerializeField]
    [Header("���")]
    public Transform wallCheck;     //ǽ�ڼ��
    public Transform ledgeCheck;        //���¼��
    public Transform groundCheck;       //������
    public Transform playerCheck;       //��Ҽ��

    private float lastDamageTime;       //�ϴ�����ʱ��
    public float currentHealth;     //��ǰ����ֵ
    public float currentStunResistance;      //��ǰѣ�εֿ���

    protected bool isStunned;       //�Ƿ�ѣ��

    //����
    private float knockbacksatart;  //��ʼ��¼����ʱ��
    private bool knockback; //�Ƿ����ڽ��л���

    
    [Header("Ч��")]
    [SerializeField]
    GameObject hitParticle;     //��������Ч��
    [SerializeField]
    GameObject deathChunkParticle;      //��������Ч��

    public virtual void Start()
    {
        //��ʼ��
        aliveGO = transform.Find("Alive").gameObject;       //��ȡ���д�����
        rb = aliveGO.GetComponent<Rigidbody2D>();       //��AliveGo�������л�ȡ���ǵĸ���
        anim = aliveGO.GetComponent<Animator>();        //��AliveGo�������л�ȡ���ǵĶ���������
        atsm = aliveGO.GetComponent<AnimationToStatemachine>();        //��AliveGo�������л�ȡ���ǵĶ���״̬��

        facingDirection = 1;        //Ĭ�ϳ���Ϊ1
        currentHealth = entityData.maxHealth;        //��ǰ����ֵΪ�������ֵ
        currentStunResistance = entityData.stunResistance;        //��ǰѣ�εֿ���Ϊѣ�εֿ���
        stateMachine = new FiniteStateMachine();

    }

    public virtual void Update()        //virtual���������ڻ�����������д,��������ֻ��Ҫʹ��override���ɸ��Ǹ÷���
    {
        stateMachine.currentState.LogicUpdate();        //����״̬���е�ǰ״̬���߼�����

        //����ѣ��
        if(Time.time >= lastDamageTime + entityData.stunRecorveryTime)
        {
            Debug.Log("����ѣ��:" + Time.time.ToString() + "      aaa     " + lastDamageTime.ToString() + "   bbb " + entityData.stunRecorveryTime.ToString());
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();      //����״̬���е�ǰ״̬���������
    }

    //���ó���,�ٶ� 
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);       //�����ռ�����Ϊ(x = ��ǰx�᷽�� * �ٶ�, ��ǰy���ٶ�)
        rb.velocity = velocityWorkSpace;        //���ٶȸ�ֵ����������ƶ�
    }
    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();      //��һ������
        velocityWorkSpace.Set(velocity * angle.x * direction, velocity * angle.y);       //�����ռ�����Ϊ(�ٶ� * �Ƕ�x * ����, �ٶ� * �Ƕ�y
        rb.velocity = velocityWorkSpace;        //���ٶȸ�ֵ����������ƶ�
    }

    //ǽ�ڼ��
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);       //Raycast(����ʼλ��, ����, ����, ͼ��)
    }

    //���¼��
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);        //Raycast�������⣬���£� ��������룬 ����ͼ�㣩
    }


    /*
     *��Ҽ�������Ҫ�ӽ�ɫ���ֳ���
     *
    */

    //�������Ƿ�����С��޷�Χ��
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.transform.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    //�������Ƿ�������޷�Χ��
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.transform.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);     
    }

    //�������Ƿ��ڽ����빥����Χ��
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.transform.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    //����
    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;     //��¼�ϴ�����ʱ��

        //����Ѫ��
        currentHealth -= attackDetails.damageAmount;        //��ǰ����ֵ��ȥ���������е��˺���
        currentStunResistance -= attackDetails.stunDamageAmount;        //��ǰѣ�εֿ�����ȥ���������е�ѣ���˺���

        //����Ч��
        DamageHop(entityData.damageHopSpeed);       //������Ծ
        Instantiate(hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));        //������������Ч��

        //�жϹ�������
        if (attackDetails.position.x > aliveGO.transform.position.x)     //��ɫλ���Ƿ��ڹ���λ�õ��ұ�
        {
            lastDamageDirection = -1;       //��������Ϊ-1
            KnockBack();
        }
        else
        {
            lastDamageDirection = 1;        //��������Ϊ1
        }

        //ѣ������
        if(currentStunResistance <= 0)
        {
            isStunned = true;
        }
       
        //��������
        if (currentHealth <= 0f)
        {
            //����
            Die();
        }
    }

    //����ѣ��
    public virtual void ResetStunResistance()
    {
        isStunned = false;      //��ѣ��
        currentStunResistance = entityData.stunResistance;        //����ѣ�εֿ���
    }

    //������Ծ
    public virtual void DamageHop(float velocity)
    {
        velocityWorkSpace.Set(rb.velocity.x, velocity);        //�����ٶ�
        rb.velocity = velocityWorkSpace;        //��ֵ������
    }

    //��ת
    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);     //Rotate��Rotation������:Rotate������ڵ�ǰ����ת,Rotation������������������ת
    }

    //����
    private void KnockBack()
    {
        knockback = true;
        knockbacksatart = Time.time;
        rb.velocity = new Vector2(entityData.knockbackSpeedX * -facingDirection, rb.velocity.y);
    }

    //����
    public virtual void Die()
    {
        Instantiate(deathChunkParticle, aliveGO.transform.position, Quaternion.identity); // ʵ������������Ч��
        Destroy(aliveGO.transform.parent.gameObject);        //���ٴ�����Alive�ĸ�����   
    }



    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));        //ǽ�ڼ����
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));        //���¼����
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3)(Vector2.down * facingDirection * entityData.groundChekDistance));    //��������
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);     //�����빥����Ҽ����
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);        //��С��޷�Χ�����
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);        //����޷�Χ�����
    }
}