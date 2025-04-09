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

public class SpellSO : ScriptableObject
{
    [SerializeField] protected string spellName;
    [SerializeField] protected int level;
    [SerializeField] protected int range;
    [SerializeField] protected bool isConcentration;
    [SerializeField] protected bool isUpcastable;
    [SerializeField] protected Damage[] damage;
    [SerializeField] protected CostType cost;
    [SerializeField] GameObject spellPrefab;

    public void CastSpell()
    {
        Debug.Log("I cast a spell");
    }

    public int GetSpellRange()
    {
        return range;
    }
}
