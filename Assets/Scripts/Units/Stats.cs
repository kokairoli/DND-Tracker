using UnityEngine;

public class Stats : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private int attackPower;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("Unit is dead");
        }
    }

    void attack(Stats target)
    {
        target.takeDamage(attackPower);
    }

    void heal(int amount)
    {
        currentHealth = amount + currentHealth >= maxHealth ? maxHealth : amount + currentHealth;
    }
}
