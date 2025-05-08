using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
            spell.Hide();
            if (destination is ActionUnit)
            {
                spell.SetSourceAndDestination(source, (ActionUnit)destination);
            }
            else if(destination is Tile)
            {
                spell.SetSourceAndDestination(source, (Tile)destination);
            }
            source.PlayAnimation(ANIMATION.CAST_SPELL);
            StartCoroutine(CastSpellAfterAnimation(source,spell));

            uiController.UpdateResourcesPanel(source.GetStats().SubstractCost(selectedSpell.GetCost()));
            battleController.ClearHighLightOfSpell(selectedSpell.GetSpellRange());
        }
        else
        {
            Debug.LogWarning("No spell selected!");
        }
    }

    IEnumerator CastSpellAfterAnimation(ActionUnit target,SpellBaseBehaviour spell)
    {
        AnimatorStateInfo stateInfo = target.animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length - 0.1f);
        spell.Show();
        spell.CastSpell();
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
