using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    //��ȡ������Ⱦ��
    private Transform player;
    private SpriteRenderer SR;
    private SpriteRenderer playerSR;

    private Color color;

    [SerializeField]
    //ʱ���趨
    private float activeTime = 0.1f;
    private float timeActivated;    
    private float alpha;
    [SerializeField]
    //͸�����趨
    public float alphaset = 0.8f;
    public float alphaMult = 0.8f;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = GetComponent<SpriteRenderer>();
        SR = GetComponent<SpriteRenderer>();

        alpha = alphaset;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }
}
