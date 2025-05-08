using UnityEngine;

public class SpellBaseBehaviour : MonoBehaviour
{
    [SerializeField] protected SpellSO spell;
    [SerializeField] protected BattleController battleController;

    protected ActionUnit source;
    protected ActionUnit destination;
    protected Tile destinationTile;
    protected SpellSO spellData;
    protected bool isActive = false;

    public void SetSourceAndDestination(ActionUnit source, ActionUnit destination)
    {
        this.source = source;
        this.destination = destination;
    }

    public void SetSourceAndDestination(ActionUnit source, Tile destination)
    {
        this.source = source;
        this.destinationTile = destination;
    }

    public void SetSpellData(SpellSO spellSO)
    {
        spellData = spellSO;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public virtual void CastSpell()
    {

    }
}
