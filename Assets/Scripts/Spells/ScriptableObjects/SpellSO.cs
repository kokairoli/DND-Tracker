using UnityEngine;

public enum DamageType
{
    Bludgeoning,
    Piercing,
    Slashing,
    Force,
    Fire,
    Ice,
    Lightning,
    Poison,
    Psychic,
    Thunder,
    Acid,
    Cold,
    Necrotic,
    Radiant
}

public struct Damage
{
    public DiceType dice;
    public int numberOfDice;
    public DamageType damageType;
}

[CreateAssetMenu(fileName = "New Spell", menuName = "SpellSO", order = 1)]
public class SpellSO : ScriptableObject
{
    [SerializeField] protected string spellName;
    [SerializeField] protected int level;
    [SerializeField] protected int range;
    [SerializeField] protected bool isConcentration;
    [SerializeField] protected bool isUpcastable;
    [SerializeField] protected Damage[] damage;
    [SerializeField] protected CostType cost;
    [SerializeField] SpellBaseBehaviour spellPrefab;


    public void CreatePrefabAndAssignSpell()
    {
        SpellBaseBehaviour spell = Instantiate(spellPrefab);
        spell.SetSpellData(this);
        spell.CastSpell();
    }
    public int GetSpellRange()
    {
        return range;
    }
}
