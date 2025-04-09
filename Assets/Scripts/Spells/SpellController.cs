using UnityEngine;
public class SpellController : MonoBehaviour
{
    [SerializeField] private SpellSO selectedSpell;
    public void SetSelectedSpell(SpellSO spell)
    {
        selectedSpell = spell;
    }

    public void CastSpell(ActionUnit source, object destination)
    {
        if (IsSpellSelected())
        {
            SpellBaseBehaviour spell =  selectedSpell.CreatePrefabAndAssignSpell();
            if (destination is ActionUnit)
            {
                spell.SetSourceAndDestination(source, (ActionUnit)destination);
            }
            else if(destination is Tile)
            {
                spell.SetSourceAndDestination(source, (Tile)destination);
            }
            spell.CastSpell();
        }
        else
        {
            Debug.LogWarning("No spell selected!");
        }
    }

    public void ClearSelectedSpell()
    {
        selectedSpell = null;
    }

    public bool IsSpellSelected()
    {
        return selectedSpell != null;
    }

    public int GetSpellRange()
    {
        return selectedSpell.GetSpellRange();
    }
}
