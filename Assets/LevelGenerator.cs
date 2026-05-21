using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;

    [Header("Player")]
    public Transform player;

    [Header("Tile Settings")]
    public float tileLength = 500f;

    public int tilesOnScreen = 3;

    private float spawnZ = 0;

    private List<GameObject> activeTiles =
        new List<GameObject>();

    private List<int> activeTileIndexes =
        new List<int>();

    private int lastTileIndex = -1;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (player.position.z - 500 >
            spawnZ - (tilesOnScreen * tileLength))
        {
            DeleteOldTile();

            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        int randomIndex = Random.Range
        (
            0,
            TilePool.Instance.tilePrefabs.Length
        );

        while (randomIndex == lastTileIndex &&
               TilePool.Instance.tilePrefabs.Length > 1)
        {
            randomIndex = Random.Range
            (
                0,
                TilePool.Instance.tilePrefabs.Length
            );
        }

        lastTileIndex = randomIndex;

        GameObject tile =
            TilePool.Instance.GetTile(randomIndex);

        tile.transform.position =
            Vector3.forward * spawnZ;

        activeTiles.Add(tile);

        activeTileIndexes.Add(randomIndex);

        spawnZ += tileLength;
    }

    void DeleteOldTile()
    {
        if (activeTiles.Count == 0)
            return;

        GameObject oldTile = activeTiles[0];

        int oldIndex = activeTileIndexes[0];

        activeTiles.RemoveAt(0);

        activeTileIndexes.RemoveAt(0);

        TilePool.Instance.ReturnTile(oldTile, oldIndex);
    }
}