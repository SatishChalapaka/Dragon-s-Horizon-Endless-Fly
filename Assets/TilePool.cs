using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public static TilePool Instance;

    [Header("All Road Prefabs")]
    public GameObject[] tilePrefabs;

    [Header("Pool Amount Per Prefab")]
    public int amountPerPrefab = 3;

    private Dictionary<int, Queue<GameObject>> pools =
        new Dictionary<int, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;

        // Create pools in Awake so they exist before other scripts use them
        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            pools[i] = new Queue<GameObject>();

            for (int j = 0; j < amountPerPrefab; j++)
            {
                GameObject obj = Instantiate(tilePrefabs[i]);

                obj.SetActive(false);

                pools[i].Enqueue(obj);
            }
        }
    }

    public GameObject GetTile(int index)
    {
        // Safety check
        if (!pools.ContainsKey(index))
        {
            Debug.LogError("Pool index not found: " + index);
            return null;
        }

        // Create extra tile if pool empty
        if (pools[index].Count == 0)
        {
            GameObject extra = Instantiate(tilePrefabs[index]);

            extra.SetActive(false);

            pools[index].Enqueue(extra);
        }

        GameObject obj = pools[index].Dequeue();

        obj.SetActive(true);

        return obj;
    }

    public void ReturnTile(GameObject obj, int index)
    {
        if (!pools.ContainsKey(index))
        {
            Debug.LogError("Pool index not found when returning tile: " + index);
            return;
        }

        obj.SetActive(false);

        pools[index].Enqueue(obj);
    }
}