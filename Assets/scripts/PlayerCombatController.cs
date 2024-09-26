using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    //攻击输入
    [SerializeField]
    private bool combatEnabled;    
    [SerializeField]
    //冷却时间  检测范围  伤害
    private float inputTimer, attack1Radius, attack1Damage;
    //检测被攻击者的位置
    [SerializeField]
    private Transform attack1HitBoxPos;
    //检测层级中可能损坏的东西
    [SerializeField]
    private LayerMask whatIsDamageable;
        
    private bool gotInput, isAttacking, isFirstAttack;

   // public bool aa { get => isAttacking ? gotInput : isFirstAttack; set => isAttacking = value; }
    private Animator anim;
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        anim.SetBool("CanAttack", combatEnabled);
    }
    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }
    //记录最后一次攻击时间, NegativeInfinity负数无穷小
    private float lastInputTime = Mathf.NegativeInfinity;
    //输入检测
    private void CheckCombatInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gotInput = true;
            lastInputTime = Time.time;
        }
    }
    //检测攻击物体
    private void CheckAttacks()
    {
        //有攻击输入时
        if (gotInput)
        {
            //未在攻击
            if(!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("Attack1", true);
                anim.SetBool("FirstAttack", !isFirstAttack);
                anim.SetBool("IsAttacking", isAttacking);
            }
        }
        //攻击冷却
        if(Time.time > lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }
    //检测攻击对象
    private void CheckAttackHitBox()
    {
        //扫描对象
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);
        foreach(Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attack1Damage);
            //启用粒子反馈伤害
        }
    }
    //结束攻击状态
    private void FinishAttack1()
    {
        Debug.Log("Finish");
        isAttacking = false;
        anim.SetBool("IsAttacking", isAttacking);
        anim.SetBool("Attack1", false);
    }
    //绘制命中框
    private void OnDrawGizmos()
    {
        //线框球体
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
