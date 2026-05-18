using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsMatrixForm : MonoBehaviour
{
    //public int numObjects = 12;
    //public GameObject prefab;
    public List<GameObject> activeCoins;
    //public List<Transform> coinsStartPositions;
    //public Transform centerPoint;

    private void OnEnable()
    {
        float xOffset = -4.0f;
        float zOffset = 0.0f;
        for (int createdTiles = 0; createdTiles <= numberOfTiles; createdTiles++)
        {
            xOffset += distanceBetweenTiles;
            if (createdTiles % tilesPerColumn == 0)
            {
                zOffset += distanceBetweenTiles;
                xOffset = -4.0f;
            }
            GameObject nObject = Instantiate(tilePrefab, new Vector3(this.transform.position.x + xOffset, this.transform.position.y + 11.5f, this.transform.position.z + zOffset), Quaternion.identity) as GameObject;
            nObject.transform.SetParent(transform);
            activeCoins.Add(nObject);
        }
    }
    public void OnDisable()
    {
        activeCoins.Clear();
        gameObject.SetActive(false);
    }

    public GameObject tilePrefab;
    public int numberOfTiles = 100;
    public int tilesPerColumn = 5;
    public float distanceBetweenTiles = 2.0f;

}
