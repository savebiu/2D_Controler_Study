using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderRender : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer m_SpriteRenderer;
    public float m_hitDuration = 0.1f;
    private Material materialInst;
    private void Start()
    {
        materialInst = m_SpriteRenderer.material;
        Debug.Log("开始");
        GetComponent<ShaderRender>().FlashEffect();
    }


    public void FlashEffect()
    {
        StartCoroutine(FlashRoutine());         //启动协程
        //Debug.Log("闪光协程");
    }

    public IEnumerator FlashRoutine()
    {
        m_SpriteRenderer.material.SetInt("_Flash", 1);     //启用闪光效果
        yield return new WaitForSeconds(m_hitDuration);     //应用延迟
        m_SpriteRenderer.material.SetInt("_Flash", 0);    //关闭闪光效果
    }
}
