using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{


    [SerializeField] private float maxHealth;
    private float currentHealth;    //���Ѫ��  ĿǰѪ��
    [SerializeField] private bool applyKnockback;    //�Ƿ�Ӧ�û���Ч��
    [SerializeField] private float knockbackSpeedX, knockbackSpeedY; //�����ٶ�
    [SerializeField] private float knockbackDuration;    //���˳���ʱ��
    [SerializeField] private float knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque;  //deathTorque��������ʱ��Ť��

    private float knockbacksatart;  //��ʼ��¼����ʱ��
    private bool knockback; //�Ƿ����ڽ��л���
    
    //�˺�������
    private int playerFacingDirection;
    private bool playerOnLeft;

    //���
    private PlayerController pc;
    private GameObject aliveGO, brokenTopGO, brokenBotGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnim;

    [SerializeField]
    GameObject hitParticle; //���Ӷ���
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

    //�˺��ۼ�
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

        //����
        if (applyKnockback && currentHealth > 0f)
        {
            //����knockback
            KnockBack();
        }
        if (currentHealth <= 0f)
        {
            //����
            Die();
        }
    }
    //����
    private void KnockBack()
    {
        knockback = true;
        knockbacksatart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }
    //һ��ʱ����ֹͣ�ػ�
    private void CheckKnockback()
    {
        if(Time.time > knockbacksatart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0f, rbAlive.velocity.y);
        }
    }
    //��������
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
