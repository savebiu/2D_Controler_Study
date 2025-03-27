using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;        // 攻击细节
    private Rigidbody2D rb; // 刚体   

    private float speed;    // 速度
    [SerializeField]
    private float gravity;      // 重力
    private float travelDistance = 2f;        // 飞行距离，飞行一定距离后开始有重力
    private float xStartPos;       // 投射物位置
    [SerializeField]
    private float damageRadius;     // 伤害半径

    private bool isGravotyOn;       // 重力开关
    private bool hasHitGround;          // 是否碰到地面


    //检测部分
    [Header("检测部分")]
    [SerializeField]
    LayerMask whatIsGround;     // 地面检测
    [SerializeField]
    LayerMask whatIsPlayer;     // 玩家检测
    [SerializeField]
    Transform damagePosition;       // 伤害位置

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0;        // 重力为0
        rb.velocity = transform.right * speed;      // 设置速度

        isGravotyOn = false;
        xStartPos = transform.position.x;        // 初始化投射物位置
    }

    // Update is called once per frame
    void Update()
    {
        // 启用重力期间改变掉落角度
        if (!hasHitGround)
        {
            attackDetails.position = transform.position;        // 设置攻击位置

            if (isGravotyOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;        // 计算旋转角度
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);        // 改变旋转角度
            }
        }
    }
    private void FixedUpdate()
    {
        // 没有碰到Ground
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);        // 检测玩家
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);        // 检测地面

            // 碰到玩家
            if (damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);        // 伤害玩家
                Destroy(gameObject);        // 销毁物体
            }

            // 碰到地面
            if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;       //重力清零
                rb.velocity = Vector2.zero;     // 速度清零
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && isGravotyOn)
            {
                isGravotyOn = true;
                rb.gravityScale = gravity;      // 重力开启
            }
        }
    }

    // 发射投掷物
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
