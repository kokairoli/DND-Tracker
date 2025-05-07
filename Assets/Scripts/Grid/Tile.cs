using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tile : SelectableUnit
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    //A Tile is blocked, if movementCost is 0
    private int movementCost = 1;

    private void OnMouseEnter()
    {
        gridManager.ClearAllHighlightedAreaOnTileMouse();
        gridManager.HighLightPathToDestination(x, y);
    }
    public void Init(bool isOffset)
    {
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }

    public void SetIsSelected(bool value)
    {
        isSelected = value;
    }

    public void ClearHighlight()
    {
        highlight.SetActive(false);
    }

    public int GetMovementCost()
    {
        return movementCost;
    }

    public void SetMovementCost(int value)
    {
        movementCost = value;
    }

    public void HighLight()
    {
        highlight.SetActive(true);
    }

}
