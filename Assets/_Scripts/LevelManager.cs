using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> activeTiles;
    public List<GameObject> chunks;
    public float minX, maxX, minY, maxY;
    public float spawnZ;
    public Transform playerTransform;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        activeTiles = new List<GameObject>();
        for (int i = 0; i < chunks.Count; i++)
        {
            SpawnChunks();
        }
    }
    private void Update()
    {
        if(playerTransform.position.z >= spawnZ - 70f)
        {
            DeleteChuncks();
            SpawnTiles();
        }
    }
    public void SpawnChunks()
    {
        GameObject nObject = Instantiate(chunks[Random.Range(0, chunks.Count)], new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z + spawnZ), Quaternion.identity);
        activeTiles.Add(nObject);
        spawnZ += 20;
    }
    public void SpawnTiles()
    {
        for (int i = 0; i < activeTiles.Count; i++)
        {
            if (!activeTiles[i].activeSelf)
            {
                activeTiles[i].SetActive(true);
                activeTiles[i].transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z + spawnZ);
                spawnZ += 20;
            }
        }
    }
    public void DeleteChuncks()
    {
        for (int i = 0; i < 3; i++)
        {
            activeTiles[i].SetActive(false);
            //activeTiles.Remove(activeTiles[i]);
        }
    }
}
