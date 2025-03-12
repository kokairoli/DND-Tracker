using UnityEngine;

public class ActionUnit : SelectableUnit
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color highlightColor;
    private SelectableUnit currentTile;
    private int tileX, tileY;

    private Color initialColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
    }

    public void SetTilePosition(int x, int y)
    {
        tileX = x;
        tileY = y;
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = highlightColor;
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            spriteRenderer.color = initialColor;
        }
    }

    override protected void OnMouseDown()
    {
        isSelected = true;
        gridManager.UnitSelected(this.x,this.y,UnitType.PLAYER);
    }

    override public void deselect()
    {
        isSelected = false;
    }

    public void Move(Vector3 destination)
    {
        this.transform.position = destination;
    }
}
