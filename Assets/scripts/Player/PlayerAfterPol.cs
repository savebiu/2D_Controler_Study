using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*对象池:
 * 1.创建单例,便于其他脚本调用
 * 2.在Awake中实例化单例
 * 3.核心方法
 *      --GrowPool: 扩充对象池
 *      --AddToPool: 将不再使用的对象放回availableobjects对象池
 *      --GetFromPool: 从对象池中获取对象,若空,则扩展对象池
 *
 *
*/
public class PlayerAfterPol : MonoBehaviour
{
    [SerializeField]

    private GameObject afterImagePrefab;        //对象预制体

    private Queue<GameObject> availableobjects = new Queue<GameObject>();       //存储对象的队列


    public static PlayerAfterPol Instance { get; private set; }     //创建单例,方便其他脚本访问

    private void Awake()
    {
        Instance = this;        //实例当前对象afterImagePrefab
        GrowPool();
    }
    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);      //添加对象到对象池中
            instanceToAdd.transform.SetParent(transform);       //设置位置
            AddToPool(instanceToAdd);       //将新建的对象添加到队列中
        }
    }

    //将不再使用的对象放回availableobjects对象池
    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableobjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        //如果队列为空(即没有可用对象), 会调用GrowPool再次扩充对象池
        if (availableobjects.Count == 0)
        {
            GrowPool();     // 扩展对象池
        }
        var instance = availableobjects.Dequeue();
        instance.SetActive(true);       //激活对象
        return instance;
    }
}
