using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform camera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private BattleController battleController;

    private const float tileSize = 1.0f;

    private Tile[][] map;


    private ActionUnit playerUnit;
    private ActionUnit enemyUnit;


    private void Start()
    {
        map = new Tile[_width][];
        AddPlayerAndEnemyToSelectableUnits();
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            map[x] = new Tile[_height];
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
        enemy.transform.position = new Vector3(map[_width - 1][_height - 1].transform.position.x, map[_width - 1][_height - 1].transform.position.y, 0);
        playerUnit.SetTilePosition(0,0);
        enemyUnit.SetTilePosition(_width - 1, _height - 1);
    }

    private void AddPlayerAndEnemyToSelectableUnits()
    {
        playerUnit = player.GetComponent<ActionUnit>();
        enemyUnit = enemy.GetComponent<ActionUnit>();
    }

    private SelectableUnit GetSelectableUnitAtPosition(int x, int y)
    {
        return map[x][y];
    }

    private Tile GetTileAtPosition(int x, int y)
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
                    battleController.TileUnitSelected(GetTileAtPosition(x,y));
                    break;
                }
            case UnitType.ENEMY:
                {
                    battleController.ActionUnitSelected(enemyUnit);
                    break;
                }
        }
        
    }

    public void HighLightMoveableArea(int x, int y,int movePoints)
    {
        for (int i = x-movePoints < 0 ? 0 : x-movePoints; i <= x+movePoints && i < _width; i++)
        {
            for (int j = y-movePoints < 0 ? 0 : y-movePoints; j <= y+movePoints && j< _height; j++)
            {
                map[i][j].EnableHighlight();
            }
        }
    }

    public void ClearAllHighlightedArea(int x, int y, int movePoints)
    {
        for (int i = x - movePoints < 0 ? 0 : x - movePoints; i <= x + movePoints && i < _width; i++)
        {
            for (int j = y - movePoints < 0 ? 0 : y - movePoints; j <= y + movePoints && j < _height; j++)
            {
                map[i][j].DisableHighlight();
            }
        }
    }

    public bool IsInReachDistance(ActionUnit start,SelectableUnit destiantion)
    {
        return CalculateDistance(start.GetTileX(), start.GetTileY(), destiantion.x, destiantion.y) <= start.getMovement();
    }

    public bool IsInAttackRange(ActionUnit start, ActionUnit destiantion)
    {
        return CalculateDistance(start.GetTileX(), start.GetTileY(), destiantion.GetTileX(), destiantion.GetTileY()) <= start.GetAttackRange();
    }

    public int CalculateDistance(int startX, int startY, int destX, int destY)
    {
        double distance = Math.Sqrt(Math.Pow(destX - startX, 2) + Math.Pow(destY - startY, 2));
        return (int)Math.Round(distance);
    }
}
