using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterPol : MonoBehaviour
{
    [SerializeField]
    //????????
    private GameObject afterImagePrefab;        //����Ԥ����
    //????Queue???????????????????
    private Queue<GameObject> availableobjects = new Queue<GameObject>();       //�洢����Ķ���Queue,��������ʹ��ʱ����Żض���
    //????,??????????????PlayerAfterPol.Instance??????
    //???set??????????
    public static PlayerAfterPol Instance { get; private set; }     //��������,���������ű�����
    private void Awake()
    {
        Instance = this;        //ʵ����ǰ����afterImagePrefab
        GrowPool();
    }
    private void GrowPool()
    {
        for(int i = 0; i< 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);      //�½�����
            instanceToAdd.transform.SetParent(transform);       
            AddToPool(instanceToAdd);       //���½��Ķ�����ӵ�������
        }
    }

    //������ʹ�õĶ���Ż�availableobjects�����
    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableobjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        //�������Ϊ��(��û�п��ö���), �����GrowPool�ٴ���������
        if (availableobjects.Count == 0)
        {
            GrowPool();
        }
        var instance = availableobjects.Dequeue();
        instance.SetActive(true);       //�������
        return instance;
    }
}
