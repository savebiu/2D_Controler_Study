using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

/*对象池子
 * 
*/


public class PlayerAfterImage : MonoBehaviour
{
    private Transform player;           //玩家
    private SpriteRenderer SR;          //精灵渲染器
    private SpriteRenderer playerSR;    //玩家精灵渲染器

    private Color color;

    [SerializeField]
    //时间设定
    private float activeTime = 0.1f;    //残影存在时间
    private float timeActivated;        //激活时间
    private float alpha;

    [SerializeField]
    //透明度设定
    public float alphaSet = 0.9f;
    private float alphaMultiplier = 0.6f;      //alpha降低比值

    //每次启动游戏都会调用它
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;      //获取角色位置
        playerSR = player.GetComponent<SpriteRenderer>();       //将渲染给playerSR
        SR = GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;

        timeActivated = Time.time;
    }
    private void Update()
    {

        alpha *= alphaMultiplier;       //随时间降低透明度
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;       //将颜色和透明度应用到精灵渲染器

        //判断对象存在时间,如果时间过长则将其放回对象池
        if (Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterPol.Instance.AddToPool(gameObject);      //将对象放回对象池
        }
    }
}
