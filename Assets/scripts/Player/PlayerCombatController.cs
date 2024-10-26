using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    //��������
    [SerializeField]
    private bool combatEnabled;    
    [SerializeField]
    //��ȴʱ��  ��ⷶΧ  �˺�
    private float inputTimer, attack1Radius, attack1Damage;
    //��ⱻ�����ߵ�λ��
    [SerializeField]
    private Transform attack1HitBoxPos;
    //���㼶�п����𻵵Ķ���
    [SerializeField]
    private LayerMask whatIsDamageable;
        
    private bool gotInput, isAttacking, isFirstAttack;

    //����
    float[] attackDetails = new float[2];

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
    //��¼���һ�ι���ʱ��, NegativeInfinity��������С
    private float lastInputTime = Mathf.NegativeInfinity;
    //������
    private void CheckCombatInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gotInput = true;
            lastInputTime = Time.time;
        }
    }
    //��⹥������
    private void CheckAttacks()
    {
        //�й�������ʱ
        if (gotInput)
        {
            //δ�ڹ���
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
        //������ȴ
        if(Time.time > lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }
    //��⹥������
    private void CheckAttackHitBox()
    {
        //ɨ�����
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        attackDetails[0] = attack1Damage;
        attackDetails[1] = transform.position.x;

        foreach(Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
            //�������ӷ����˺�
        }
    }
    //��������״̬
    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("IsAttacking", isAttacking);
        anim.SetBool("Attack1", false);
    }
    //�������п�
    private void OnDrawGizmos()
    {
        //�߿�����
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
