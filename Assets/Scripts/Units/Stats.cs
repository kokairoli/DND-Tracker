using UnityEngine;

public class Stats : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private int attackPower;
    public HealthBar healthBar;

    private void Start()
    {
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

    void attack(Stats target)
    {
        target.TakeDamage(attackPower);
    }

    void heal(int amount)
    {
        currentHealth = amount + currentHealth >= maxHealth ? maxHealth : amount + currentHealth;
    }
}
