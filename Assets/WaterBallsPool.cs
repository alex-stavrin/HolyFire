using System.Collections.Generic;
using UnityEngine;

public class WaterBallsPool : MonoBehaviour
{
    public static WaterBallsPool sharedInstance;
    [SerializeField] int amountToPool;
    [SerializeField] GameObject objectToPool;

    List<GameObject> pooledObjects;

    private void Awake()
    {
        sharedInstance = this;
    }


    void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject spawned = Instantiate(objectToPool);
            spawned.SetActive(false);
            pooledObjects.Add(spawned);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
