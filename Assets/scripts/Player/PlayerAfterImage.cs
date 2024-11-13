using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    //��ȡ������Ⱦ��
    private Transform player;
    private SpriteRenderer SR;
    private SpriteRenderer playerSR;    //��ʱ�併��alpha

    private Color color;

    [SerializeField]
    //ʱ���趨
    private float activeTime = 0.1f;
    private float timeActivated;    
    private float alpha;
    
    [SerializeField]
    //͸�����趨
    public float alphaSet = 0.8f;
    private float alphaMultiplier = 0.85f;      //alpha���ͱ�ֵ

    //ÿ��������Ϸ���������
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;      //��ȡ��ɫλ��
        playerSR = player.GetComponent<SpriteRenderer>();
        SR = GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;

        timeActivated = Time.time;
    }
    private void Update()
    {
        
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;
        if(Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterPol.Instance.AddToPool(gameObject);
        }
    }
}
