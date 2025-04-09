using UnityEngine;
public class SpellController : MonoBehaviour
{
    [SerializeField] private SpellSO selectedSpell;
    public void SetSelectedSpell(SpellSO spell)
    {
        selectedSpell = spell;
    }

    public void CastSpell()
    {
        if (IsSpellSelected())
        {
            selectedSpell.CreatePrefabAndAssignSpell();
            //ClearSelectedSpell();
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
