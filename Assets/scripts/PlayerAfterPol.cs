using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterPol : MonoBehaviour
{
    [SerializeField]
    //残影预制体
    private GameObject afterImagePrefab;
    //利用Queue队列先进先出创建对象池
    private Queue<GameObject> availableobjects = new Queue<GameObject>();
    //单例,其他脚本可以通过PlayerAfterPol.Instance访问它
    //私有set确保外部只能读取
    public static PlayerAfterPol Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        GrowPool();
    }
    private void GrowPool()
    {
        for(int i = 0; i< 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableobjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if(availableobjects.Count == 0)
        {
            GrowPool();
        }
        var instance = availableobjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
