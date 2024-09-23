using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    //获取精灵渲染器
    private Transform player;
    private SpriteRenderer SR;
    private SpriteRenderer playerSR;

    private Color color;

    [SerializeField]
    //时间设定
    private float activeTime = 0.1f;
    private float timeActivated;    
    private float alpha;
    [SerializeField]
    //透明度设定
    public float alphaSet = 0.8f;
    public float alphaMult = 0.8f;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = GetComponent<SpriteRenderer>();
        SR = GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }
    private void Update()
    {
        alpha *= alphaMult;
        color = new Color(1f, 1f, 1f,alpha);
        SR.color = color;
        if(Time.time >=(timeActivated + activeTime))
        {
            PlayerAfterPol.Instance.AddToPool(gameObject);
        }
    }
}
