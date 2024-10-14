using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField]
    private GameObject afterimagePrefab;
    private Queue<GameObject> availableObjs = new Queue<GameObject>();

    public float distBetweenImages = 1f;

    private void Awake()
    {
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(afterimagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObjs.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (availableObjs.Count == 0)
        {
            GrowPool();
        }

        var instance = availableObjs.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
