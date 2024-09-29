using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterPol : MonoBehaviour
{
    [SerializeField]
    //��ӰԤ����
    private GameObject afterImagePrefab;
    //����Queue�����Ƚ��ȳ����������
    private Queue<GameObject> availableobjects = new Queue<GameObject>();
    //����,�����ű�����ͨ��PlayerAfterPol.Instance������
    //˽��setȷ���ⲿֻ�ܶ�ȡ
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
