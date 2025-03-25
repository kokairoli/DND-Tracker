using System.Collections.Generic;
using UnityEngine;

public class Tile : SelectableUnit
{
    [SerializeField] private Color baseColor,offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    

    public void Init(bool isOffset)
    {
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }

    public void SetIsSelected(bool value)
    {
        isSelected = value;
    }

    
}
