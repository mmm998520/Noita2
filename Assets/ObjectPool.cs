using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    private Queue<GameObject> gameObjectQueue = new Queue<GameObject>();
    public int startInstantiateNum = 0;

    void Start()
    {
        for (int i = 0; i < startInstantiateNum; i++)
        {
            GameObject obj = Instantiate(prefab,transform);
            obj.SetActive(false);
            gameObjectQueue.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (gameObjectQueue.Count == 0)
        {
            // if pool is empty, instantiate new object
            GameObject obj = Instantiate(prefab, transform);
            gameObjectQueue.Enqueue(obj);
        }
        GameObject pooledObject = gameObjectQueue.Dequeue();
        pooledObject.SetActive(true);
        return pooledObject;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        gameObjectQueue.Enqueue(obj);
    }
}
