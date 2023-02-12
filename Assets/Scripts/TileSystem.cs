using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSystem : MonoBehaviour
{

    public GameObject[] tilePrefabs;

    public List<GameObject> activeTiles;

    public float maxGeneratedX = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawLine(new Vector3(maxGeneratedX, 0f, 0f), new Vector3(Camera.main.transform.position.x, 0f, 0f), Color.red);
        if (maxGeneratedX < Camera.main.transform.position.x + 10f)
        {
            GenerateTile();
        }

        if (activeTiles[0].transform.position.x + activeTiles[0].GetComponent<Tile>().width < Camera.main.transform.position.x - 10f)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }

    void GenerateTile()
    {
        int randomIndex = Random.Range(0, tilePrefabs.Length);
        GameObject tile = Instantiate(tilePrefabs[randomIndex], transform);
        tile.transform.position = new Vector3(maxGeneratedX, 0f, 0f);
        maxGeneratedX += tile.GetComponent<Tile>().width;
        activeTiles.Add(tile);
    }
}
