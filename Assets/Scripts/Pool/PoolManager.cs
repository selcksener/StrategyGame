using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonBehaviour<PoolManager>
{
    //references of objects
    public List<PoolObjectReference> poolObjectReferences = new List<PoolObjectReference>();
    public Dictionary<PoolObjectType, GameObject> poolObjectPrefab = new Dictionary<PoolObjectType, GameObject>();
    public int size = 5;

    //objects in the pool
    public Dictionary<PoolObjectType, Queue<GameObject>> poolObject =
        new Dictionary<PoolObjectType, Queue<GameObject>>() { };

    //active objects
    private Dictionary<PoolObjectType, Queue<GameObject>>
        activeObjects = new Dictionary<PoolObjectType, Queue<GameObject>>() { };

    protected override void Awake()
    {
        base.Awake();
        transform.parent = null;
        DontDestroyOnLoad(gameObject); //
        //objects are created at the beginning
        for (int i = 0; i < poolObjectReferences.Count; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject pooledObject = Instantiate(poolObjectReferences[i].poolObjectPrefab, transform);
                pooledObject.SetActive(false);
                if (poolObject.ContainsKey(poolObjectReferences[i].poolobjectType) == false)
                {
                    poolObject.Add(poolObjectReferences[i].poolobjectType, new Queue<GameObject>());
                }

                poolObject[poolObjectReferences[i].poolobjectType].Enqueue(pooledObject);
            }

            //activeObjects.Add(poolObjectReferences[i].poolobjectType, new Queue<GameObject>());
            poolObjectPrefab.Add(poolObjectReferences[i].poolobjectType, poolObjectReferences[i].poolObjectPrefab);
        }
    }
    
    /// <summary>
    /// add object to the pool
    /// </summary>
    /// <param name="type"></param>
    /// <param name="obj"></param>
    public void AddObjectInPool(PoolObjectType type, GameObject obj)
    {
        if (poolObject.ContainsKey(type) == false)
        {
            poolObject.Add(type, new Queue<GameObject>());
        }

        poolObject[type].Enqueue(obj);
        //activeObjects[type].Dequeue();
        obj.SetActive(false);
       // obj.transform.localScale = Vector3.one;
    }

    /// <summary>
    /// Get object from the pool
    /// if the pool is empty, create a new object
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetObject(PoolObjectType type)
    {
        if (poolObject.ContainsKey(type) == false)
        {
            poolObject.Add(type, new Queue<GameObject>());
        }

        if (poolObject[type].Count == 0)
        {
            GameObject pooledObject = Instantiate(poolObjectPrefab[type], transform);
            poolObject[type].Enqueue(pooledObject);
            return GetObject(type);
        }

        GameObject p = poolObject[type].Dequeue();
        //activeObjects[type].Enqueue(p);
        p.SetActive(true);
        return p;
    }

    /// <summary>
    /// resets the pool
    /// </summary>
    private void ResetPool()
    {
        foreach (var item in activeObjects)
        {
            while (item.Value.Count != 0)
            {
                AddObjectInPool(item.Key, item.Value.Peek());
            }
        }
    }
}


public enum PoolObjectType
{
    None,
    Unit,
    Bullet,
    BarrackUnit,
    PowerPlantUnit,
    SoldierUnit,
    Soldier_1,
    Soldier_2,
    Soldier_3
}

[System.Serializable]
public class PoolObjectReference
{
    public PoolObjectType poolobjectType;
    public GameObject poolObjectPrefab;
}