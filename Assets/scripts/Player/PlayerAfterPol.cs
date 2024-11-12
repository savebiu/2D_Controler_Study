using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterPol : MonoBehaviour
{
    [SerializeField]
    //????????
    private GameObject afterImagePrefab;        //对象预制体
    //????Queue???????????????????
    private Queue<GameObject> availableobjects = new Queue<GameObject>();       //存储对象的队列Queue,当对象不再使用时将其放回队列
    //????,??????????????PlayerAfterPol.Instance??????
    //???set??????????
    public static PlayerAfterPol Instance { get; private set; }     //创建单例,方便其他脚本访问
    private void Awake()
    {
        Instance = this;        //实例当前对象afterImagePrefab
        GrowPool();
    }
    private void GrowPool()
    {
        for(int i = 0; i< 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);      //新建对象
            instanceToAdd.transform.SetParent(transform);       
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
            GrowPool();
        }
        var instance = availableobjects.Dequeue();
        instance.SetActive(true);       //激活对象
        return instance;
    }
}
