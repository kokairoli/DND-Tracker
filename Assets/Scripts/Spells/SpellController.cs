using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    private SpellSO selectedSpell;
    [SerializeField] private UIController uiController;
    [SerializeField] private BattleController battleController;
    public void SetSelectedSpell(SpellSO spell)
    {
        selectedSpell = spell;
        battleController.HighLightSpellRangeArea(spell.GetSpellRange());
        uiController.CloseSpellPanel();
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
            uiController.UpdateResourcesPanel(source.GetStats().SubstractCost(selectedSpell.GetCost()));
            battleController.ClearHighLightOfSpell(selectedSpell.GetSpellRange());
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
