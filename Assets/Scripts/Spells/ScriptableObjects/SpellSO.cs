using System.Collections.Generic;
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

[System.Serializable]
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
    [SerializeField] protected List<Damage> damage;
    [SerializeField] protected CostType cost;
    [SerializeField] SpellBaseBehaviour spellPrefab;
    [SerializeField] float speed;


    public SpellBaseBehaviour CreatePrefabAndAssignSpell()
    {
        SpellBaseBehaviour spell = Instantiate(spellPrefab);
        spell.SetSpellData(this);
        return spell;
    }
    public int GetSpellRange()
    {
        return range;
    }

    public int GetDamage()
    {
        int damage = 0;
        for (int i = 0; i < this.damage.Count; i++)
        {
            for (int j = 0; j < this.damage[i].numberOfDice; j++)
            {
                damage += DiceController.RollDice(this.damage[i].dice, 0);
            }
        }
        return damage;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public CostType GetCost()
    {
        return cost;
    }
}
