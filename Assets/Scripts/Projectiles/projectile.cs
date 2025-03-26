using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private AttackDetails attackDetails;        // ����ϸ��
    private Rigidbody2D rb; // ����   

    private float speed;    // �ٶ�
    [SerializeField]
    private float gravity;      // ����
    private float travelDistance = 2f;        // ���о��룬����һ�������ʼ������
    private float xStartPos;       // Ͷ����λ��
    [SerializeField]
    private float damageRadius;     // �˺��뾶

    private bool isGravotyOn;       // ��������
    private bool hasHitGround;          // �Ƿ���������


    //��ⲿ��
    [Header("��ⲿ��")]
    [SerializeField]
    LayerMask whatIsGround;     // ������
    [SerializeField]
    LayerMask whatIsPlayer;     // ��Ҽ��
    [SerializeField]
    Transform damagePosition;       // �˺�λ��

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0;        // ����Ϊ0
        rb.velocity = transform.right * speed;      // �����ٶ�

        isGravotyOn = false;
        xStartPos = transform.position.x;        // ��ʼ��Ͷ����λ��
    }

    // Update is called once per frame
    void Update()
    {
        // ���������ڼ�ı����Ƕ�
        if (!hasHitGround)
        {
            attackDetails.position = transform.position;        // ���ù���λ��

            if (isGravotyOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;        // ������ת�Ƕ�
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);        // �ı���ת�Ƕ�
            }
        }
    }
    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);        // ������
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);        // ������

            // �������
            if (damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);        // �˺����
                Destroy(gameObject);        // ��������
            }

            // ��������
            if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;       //��������
                rb.velocity = Vector2.zero;     // �ٶ�����
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && isGravotyOn)
            {
                isGravotyOn = true;
            }
        }
    }

    public void FireProjectile(float speed, float traveDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = traveDistance;
        attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
