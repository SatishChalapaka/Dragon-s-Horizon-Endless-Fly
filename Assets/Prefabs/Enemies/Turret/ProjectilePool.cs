using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;
    public GameObject Prefab => bulletPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);

            bullet.SetActive(false);

            pool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (pool.Count == 0)
        {
            GameObject bullet = Instantiate(bulletPrefab);

            bullet.SetActive(false);

            pool.Enqueue(bullet);
        }

        GameObject obj = pool.Dequeue();

        obj.SetActive(true);

        return obj;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);

        pool.Enqueue(bullet);
    }
}