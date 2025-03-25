using UnityEditor;
using UnityEngine;

public class ActionUnit : SelectableUnit
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color highlightColor;
    [SerializeField] private UnitType unitType;
    [SerializeField] private Stats stats;
    private int tileX, tileY;

    private Color initialColor;

    private void Start()
    {
        initialColor = spriteRenderer.color;
    }

    public void SetTilePosition(int x, int y)
    {
        tileX = x;
        tileY = y;
    }

    private void OnMouseEnter()
    {
        EnableHighlight();
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
        gridManager.UnitSelected(this.x,this.y,this.unitType);
    }

    public override void DisableHighlight()
    {
        spriteRenderer.color = initialColor;
    }

    override public void EnableHighlight()
    {
        spriteRenderer.color = highlightColor;
    }

    override public void deselect()
    {
        isSelected = false;
        DisableHighlight();
    }

    public void Select()
    {
        isSelected = true;
        EnableHighlight();
    }

    public void Move(Vector3 destination, int x, int y)
    {
        this.transform.position = destination;
        SetTilePosition(x, y);
    }

    public int GetTileX()
    {
        return tileX;
    }
    public int GetTileY() {
        return tileY;
    }

    public void Attack(ActionUnit target)
    {
        target.TakeDamage(stats.GetAttackPower());
    }

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }

    public int RollInitiative()
    {
        return DiceController.RollDice(DiceType.D20);
    }

    public int getMovement()
    {
        return stats.GetCurrentMovement();
    }

    public void SubstractDistanceFromMovement(int distance)
    {
        stats.SubstractDistance(distance);
    }

    public void ResetResources()
    {
        stats.ResetResources();
    }

    public int GetAttackRange()
    {
        return stats.GetAttackRange();
    }

}
