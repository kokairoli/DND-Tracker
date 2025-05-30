using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableSO", menuName = "Scriptable Objects/ConsumableSO")]
public class ConsumableSO : ItemSO
{
    [SerializeField] private int healthRestored;
    [SerializeField] private int actionRestored;

    public int GetHealthRestored()
    {
        return healthRestored;
    }

    public int GetActionRestored()
    {
        return actionRestored;
    }
}
