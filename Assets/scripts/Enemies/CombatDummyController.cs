using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{


    [SerializeField] private float maxHealth;
    private float currentHealth;    //最大血量  目前血量
    [SerializeField] private bool applyKnockback;    //是否应用击退效果
    [SerializeField] private float knockbackSpeedX, knockbackSpeedY; //击退速度
    [SerializeField] private float knockbackDuration;    //击退持续时间
    [SerializeField] private float knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque;  //deathTorque假人死亡时的扭矩

    private float knockbacksatart;  //开始记录击退时间
    private bool knockback; //是否正在进行击退
    
    //伤害方向检测
    private int playerFacingDirection;
    private bool playerOnLeft;

    //组件
    private PlayerController pc;
    private GameObject aliveGO, brokenTopGO, brokenBotGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnim;

    [SerializeField]
    GameObject hitParticle; //粒子动画
    private void Update()
    {
        CheckKnockback();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        aliveGO = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("BrokenTop").gameObject;
        brokenBotGO = transform.Find("BrokenBot").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBotGO.SetActive(false);
    }

    //伤害累计
    private void Damage(float amount)
    {
        currentHealth -= amount;
        playerFacingDirection = pc.GetFacingDirection();

        Instantiate(hitParticle, aliveAnim.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (playerFacingDirection == 1)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }
        aliveAnim.SetBool("PlayerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("damage");

        //击退
        if (applyKnockback && currentHealth > 0f)
        {
            //击退knockback
            KnockBack();
        }
        if (currentHealth <= 0f)
        {
            //死亡
            Die();
        }
    }
    //击退
    private void KnockBack()
    {
        knockback = true;
        knockbacksatart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }
    //一定时间内停止回击
    private void CheckKnockback()
    {
        if(Time.time > knockbacksatart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0f, rbAlive.velocity.y);
        }
    }
    //死亡控制
    private void Die()
    {
        aliveGO.SetActive(false);
        brokenBotGO.SetActive(true);
        brokenTopGO.SetActive(true);

        brokenBotGO.transform.position = aliveGO.transform.position;    
        brokenTopGO.transform.position = aliveGO.transform.position;

        rbBrokenBot.velocity = new Vector2( knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
    }
}
