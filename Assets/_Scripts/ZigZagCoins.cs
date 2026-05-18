using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagCoins : MonoBehaviour
{

    public List<GameObject> activeCoins;
    public List<Transform> activeCoinsPos;
    public GameObject coinPrefab;
    private void OnEnable()
    {
        activeCoins.Clear();
        for (int i = 0; i < activeCoinsPos.Count; i++)
        {
            GameObject nObject = Instantiate(coinPrefab, new Vector3(activeCoinsPos[i].transform.position.x, activeCoinsPos[i].transform.position.y, activeCoinsPos[i].transform.position.z), Quaternion.identity) as GameObject;
            nObject.transform.SetParent(transform);
            activeCoins.Add(nObject);
        }
    }
    private void OnDisable()
    {
        activeCoins.Clear();
        gameObject.SetActive(false);
    }
}
