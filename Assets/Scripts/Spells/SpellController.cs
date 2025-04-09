using UnityEngine;
public class SpellController : MonoBehaviour
{
    private SpellSO selectedSpell;
    public void SetSelectedSpell(SpellSO spell)
    {
        selectedSpell = spell;
    }

    public void CastSpell()
    {
        if (IsSpellSelected())
        {
            selectedSpell.CastSpell();
            ClearSelectedSpell();
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
