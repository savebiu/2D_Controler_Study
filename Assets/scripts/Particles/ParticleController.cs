using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//粒子动画控制器
public class ParticleController : MonoBehaviour
{
    //销毁粒子动画
    void FinishAnim()
    {
        Destroy(gameObject);
    }
}
