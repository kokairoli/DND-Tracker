using UnityEngine;

public enum CostType
{
    ACTION,
    BONUS_ACTION
}
struct Resources
{
    int maxActionPoints;
    int maxBonusActionPoints;
    int maxMovementPoints;

    int currentActionPoints;
    int currentBonusActionPoints;
    int currentMovementPoints;

    public int MaxActionPoints
    {
        get { return maxActionPoints; }
        set { maxActionPoints = value; }
    }

    public int MaxBonusActionPoints
    {
        get { return maxBonusActionPoints; }
        set { maxBonusActionPoints = value; }
    }

    public int MaxMovementPoints
    {
        get { return maxMovementPoints; }
        set { maxMovementPoints = value; }
    }

    public int CurrentActionPoints
    {
        get { return currentActionPoints; }
        set { currentActionPoints = value; }
    }

    public int CurrentBonusActionPoints
    {
        get { return currentBonusActionPoints; }
        set { currentBonusActionPoints = value; }
    }

    public int CurrentMovementPoints
    {
        get { return currentMovementPoints; }
        set { currentMovementPoints = value; }
    }

    public void resetResources()
    {
        currentActionPoints = maxActionPoints;
        currentBonusActionPoints = maxBonusActionPoints;
        currentMovementPoints = maxMovementPoints;
    }
}
public class Stats : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private int attackPower;
    public HealthBar healthBar;
    private Resources resources;
    private int attackRange = 1;

    private void Start()
    {
        resources.MaxMovementPoints = 6;
        resources.CurrentMovementPoints = 6;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Unit is dead");
        }
    }

    public int GetAttackPower()
    {
        return attackPower;
    }

    public int GetAttackRange()
    {
        return attackRange;
    }

    void attack(Stats target)
    {
        target.TakeDamage(attackPower);
    }

    void heal(int amount)
    {
        currentHealth = amount + currentHealth >= maxHealth ? maxHealth : amount + currentHealth;
    }

    public void SubstractDistance(int distance)
    {
        resources.CurrentMovementPoints -= distance;
    }

    public int GetCurrentMovement()
    {
        return resources.CurrentMovementPoints;
    }

    public void ResetResources()
    {
        resources.resetResources();
    }
}
