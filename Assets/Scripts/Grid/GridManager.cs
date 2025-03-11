using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform camera;
    [SerializeField] private GameObject player;

    private const float tileSize = 1.0f;

    private Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();


    private void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Tile spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset = (x % 2 == 0 && y%2!=0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                tiles[new Vector2(x,y)] = spawnedTile;
            }
        }

        tiles.TryGetValue(new Vector2(0, 0), out Tile firstTile);
        camera.transform.position = new Vector3((float)_width / 2 - tileSize / 2, (float)_height - tileSize / 2,-10);
        player.transform.position = new Vector3(firstTile.transform.position.x, firstTile.transform.position.y, 0);
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        if (tiles.TryGetValue(position,out Tile tile))
        {
            return tile;
        }
        return null;
    }
}
