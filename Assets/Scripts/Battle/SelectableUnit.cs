using UnityEngine;
public enum UnitType
{
    ACTION_UNIT,
    TILE
}
public class SelectableUnit : MonoBehaviour
{
    [SerializeField] protected GameObject highlight;
    static protected GridManager gridManager;
    protected bool isSelected;
    public int x, y;

    private void Start()
    {
        if (gridManager == null)
        {
            gridManager = GetComponentInParent<GridManager>();
        }
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            highlight.SetActive(false);
        }
    }

    virtual public void DisableHighlight()
    {
        highlight.SetActive(false);
        isSelected = false;
    }

    virtual public void EnableHighlight()
    {
        highlight.SetActive(true);
        isSelected = true;
    }

    virtual protected void OnMouseDown()
    {
        isSelected = true;
        gridManager.UnitSelected(this.x,this.y,UnitType.TILE);
    }

    virtual public void deselect()
    {
        isSelected = false;
        highlight.SetActive(false);
    }
}
