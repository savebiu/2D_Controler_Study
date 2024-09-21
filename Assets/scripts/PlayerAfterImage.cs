using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    //获取精灵渲染器
    private Transform player;
    private SpriteRenderer SR;
    private SpriteRenderer PlayerSR;

    private Color color;

    [SerializeField]
    //时间设定
    private float activeTime = 0.1f;
    private float timeActivated;    
    private float alpha;
    [SerializeField]
    //透明度设定
    public float alphaset = 0.8f;
    public float alphaMult = 0.8f;
    private void OnEnable()
    {
        player = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SR = GetComponent<SpriteRenderer>();
    }
}
