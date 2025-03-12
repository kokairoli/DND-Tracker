using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform camera;
    [SerializeField] private GameObject player;
    [SerializeField] private BattleController battleController;

    private const float tileSize = 1.0f;

    private SelectableUnit[][] map;


    private ActionUnit playerUnit;


    private void Start()
    {
        map = new SelectableUnit[_width][];
        GenerateGrid();
        AddPlayerToSelectableUnits();
    }
    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            map[x] = new SelectableUnit[_height];
            for (int y = 0; y < _height; y++)
            {
                Tile spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                spawnedTile.transform.parent = transform;
                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset = (x % 2 == 0 && y%2!=0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                map[x][y] = spawnedTile;
                spawnedTile.SetPosition(x, y);
            }
        }
        SelectableUnit firstTile = map[0][0];
        camera.transform.position = new Vector3((float)_width / 2 - tileSize / 2, (float)_height - tileSize / 2,-10);
        player.transform.position = new Vector3(firstTile.transform.position.x, firstTile.transform.position.y, 0);
        player.GetComponent<ActionUnit>().SetTilePosition(0,0);
    }

    private void AddPlayerToSelectableUnits()
    {
        playerUnit = player.GetComponent<ActionUnit>();
    }

    private SelectableUnit GetSelectableUnitAtPosition(int x, int y)
    {
        return map[x][y];
    }

    public void UnitSelected(int x, int y, UnitType type)
    {
        switch (type)
        {
            case UnitType.PLAYER:
                {
                    battleController.ActionUnitSelected(playerUnit);
                    break;
                }
            case UnitType.TILE:
                {
                    battleController.TileUnitSelected(GetSelectableUnitAtPosition(x,y));
                    break;
                }
        }
        
    }
}
