using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler current;
    public GameObject pooledObject;
    public int pooledAmount;
    public bool willGrow;
    public List<GameObject> pooledObjects;
    public float spawnTime = 2f;
    void Start()
    {
        current = this;
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

    }
    private void Update()
    {
        if (spawnTime <= 0)
        {
            Shoot();
            spawnTime = 2f;
        }else
        {
            spawnTime -= Time.deltaTime;
        }
    }
    public void Shoot()
    {
        GameObject obj1 = ObjectPooler.current.GetPooledObject();
        if (obj1 == null)
        {
            return;
        }
        obj1.transform.position = transform.position;
        obj1.transform.rotation = transform.rotation;
        obj1.SetActive(true);
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }
        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }
    public void ResetBullets()
    {
        foreach (GameObject u in ObjectPooler.current.pooledObjects)
        {
            u.gameObject.SetActive(false);
            Destroy(u.gameObject);
        }
        ObjectPooler.current.pooledObjects.Clear();
    }
}
