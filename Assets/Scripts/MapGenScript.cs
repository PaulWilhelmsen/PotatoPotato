using System.Collections.Generic;
using UnityEngine;

public class MapGenScript : MonoBehaviour
{
    [Tooltip("Rows and cols for the map grid")]
    public Vector2 myGrid;

    [Tooltip("Prefabs dimensions in Unity units")]
    public Vector3 tileDimensions;

    [Tooltip("Populate with all the prefabs used to generate map")]
    public GameObject[] prefabTiles;

    // storing the map tiles in a list could be useful
    List<GameObject> mapList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        CreateMap();
    }

    void CreateMap()
    {
        for (int row = 1; row <= myGrid.y; row++)
        {
            for (int col = 1; col <= myGrid.x; col++)
            {
                // choose a random prefab tile
                int n = Random.Range(0, prefabTiles.Length);
                GameObject thePrefab = prefabTiles[n];

                // spawns the tile
                GameObject theTile = Instantiate(thePrefab, transform);
                theTile.name = "Tile_" + col + "_" + row;
                theTile.transform.localPosition = new Vector3((col - 1) * tileDimensions.x, 0f, (row - 1) * tileDimensions.z);

                // stores the tile in the List
                mapList.Add(theTile);
            }
        }
        print(mapList.Count + " tiles in the map");
    }
}
