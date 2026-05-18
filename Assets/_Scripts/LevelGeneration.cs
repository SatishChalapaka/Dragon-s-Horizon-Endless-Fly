using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnvironmentData
{
    public GameObject[] groundTilesPrefabs;
    public GameObject[] tilePrefabs;
}
public class LevelGeneration : MonoBehaviour
{
    public static LevelGeneration instance;
    public EnvironmentData[] environmentData;
    public List<GameObject> activeTiles;
    public List<GameObject> activeGroundTiles;
    public Material[] skyboxMaterials;
    public float tileLength;
    public float groundLength;
    public int numberOfTiles;
    public int totalNumOfTiles;
    public float zSpawn;
    public float zSpawnForGround;
    private Transform playerTransform;
    private int previousIndex;
    int indexGround = 0;
    public GameObject[] environments;
    public int environmentNumber;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        //GenerationEnvironment();
    }
    void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (DragonController.instance.isMove)
        {
            if (playerTransform.position.z - 30 >= zSpawn - (numberOfTiles * tileLength))
            {
                int index = Random.Range(0, environmentData[environmentNumber].tilePrefabs.Length);

                //while (index == previousIndex)
                //    index = Random.Range(0, totalNumOfTiles);

                DeleteTile();
                SpawnTile(index);
            }
            if (playerTransform.position.z - 300 >= zSpawnForGround - (3 * 300))
            {
                activeGroundTiles[indexGround].transform.position = new Vector3(0, 0, zSpawnForGround);
                indexGround += 1;
                zSpawnForGround += groundLength;
                if (indexGround >= 3)
                {
                    indexGround = 0;
                }
            }
        }
    }
    public float minX,maxX,minY,maxY;
    
    public void SpawnTile(int index)
    {
        GameObject tile = environmentData[environmentNumber].tilePrefabs[index];

        if (tile.activeInHierarchy)
        {
            for (int i = 0; i < environmentData[environmentNumber].tilePrefabs.Length; i++)
            {
                if (!environmentData[environmentNumber].tilePrefabs[i].activeInHierarchy)
                {
                    tile = environmentData[environmentNumber].tilePrefabs[i];
                }
            }
        }
        if (tile.activeInHierarchy)
        {
            print(index);
        }
        //if (tile.activeInHierarchy)
        //    tile = environmentData[environmentNumber].tilePrefabs[index + 8];
        //if (tile.activeInHierarchy)
        //    tile = environmentData[environmentNumber].tilePrefabs[index + 9];
        //if (tile.activeInHierarchy)
        //    tile = environmentData[environmentNumber].tilePrefabs[index + 10];
        //if (tile.activeInHierarchy)
        //    tile = environmentData[environmentNumber].tilePrefabs[index + 11];
        //if (tile.activeInHierarchy)
        //    tile = environmentData[environmentNumber].tilePrefabs[index + 12];

        tile.transform.position = new Vector3(Random.Range(minX,maxX), Random.Range(minY, maxY), zSpawn);
        tile.transform.rotation = Quaternion.identity;
        tile.SetActive(true);

        activeTiles.Add(tile);
        zSpawn += tileLength;
        previousIndex = index;

    }
    private void DeleteTile()
    {
        activeTiles[0].SetActive(false);
        activeTiles.RemoveAt(0);
    }
    public void GenerationEnvironment()
    {
        indexGround = 0;
        environmentNumber = Random.Range(0, environments.Length);
        RenderSettings.skybox = skyboxMaterials[environmentNumber];
        for (int i = 0; i < environments.Length; i++)
        {
            if (environmentNumber ==  i)
            {
                environments[i].SetActive(true);
            }else
            {
                environments[i].SetActive(false);
            }
        }
        activeTiles = new List<GameObject>();
        activeGroundTiles = new List<GameObject>();
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
                //for (int j = 0; j < 5; j++)
                //{
                //    SpawnTile(j);
                //}
            else
                SpawnTile(Random.Range(0, environmentData[environmentNumber].tilePrefabs.Length));
        }
        for (int i = 0; i < 3; i++)
        {
            environmentData[environmentNumber].groundTilesPrefabs[i].transform.position = new Vector3(0, 0, zSpawnForGround);
            zSpawnForGround += groundLength;
            activeGroundTiles.Add(environmentData[environmentNumber].groundTilesPrefabs[i]);
        }
    }
}
