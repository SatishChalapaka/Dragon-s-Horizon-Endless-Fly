using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCircleForm : MonoBehaviour
{
    public int numObjects = 12;
    public GameObject prefab;
    public List<GameObject> activeCoins;
    public List<Transform> coinsStartPositions;
    public Transform centerPoint;

    private void OnEnable()
    {
        Vector3 center = centerPoint.transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            int a = i * 30;
            Vector3 pos = RandomCircle(center, 1.0f, a);
            GameObject nObject = Instantiate(prefab, pos, Quaternion.identity);
            nObject.transform.SetParent(transform);
            activeCoins.Add(nObject);
        }
    }
    public void OnDisable()
    {
        activeCoins.Clear();
    }
    Vector3 RandomCircle(Vector3 center, float radius, int a)
    {
        //Debug.Log(a);
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
    
}
