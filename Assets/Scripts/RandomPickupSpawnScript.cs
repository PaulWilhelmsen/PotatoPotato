using UnityEngine;

public class RandomPickupSpawnScript : MonoBehaviour
{
    public GameObject[] prefabTiles;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("spawned");
        int n = Random.Range(0, prefabTiles.Length);
        float randomSize = Random.Range(1f, 2.5f);
        var thePrefab = prefabTiles[n];

        // spawns the tile
        var prefab = Instantiate(thePrefab, transform);
        var ls = prefab.transform.localScale;
        prefab.transform.localScale = new Vector3(ls.x * randomSize, ls.y * randomSize, ls.z * randomSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
