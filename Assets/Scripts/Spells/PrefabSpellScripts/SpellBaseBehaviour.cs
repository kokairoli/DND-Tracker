using UnityEngine;

public class SpellBaseBehaviour : MonoBehaviour
{
    [SerializeField] protected SpellSO spell;

    protected SpellSO spellData;

    public void SetSpellData(SpellSO spellSO)
    {
        spellData = spellSO;
    }
    public virtual void CastSpell()
    {

    }
}
