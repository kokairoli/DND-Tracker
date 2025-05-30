using UnityEngine;
using UnityEngine.UI;


public enum ItemType
{
    HealthPotion,
    ActionPotion,
}



[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] protected Sprite icon;
    [SerializeField] protected CostType resourceCost;
    [SerializeField] protected bool isStackable;
    [SerializeField] protected int currentStack = 1;
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected string itemName;
    [SerializeField] protected string itemDescription;

    public ItemType GetItemType()
    {
        return itemType;
    }

    public CostType GetCost()
    {
        return resourceCost;
    }

    public Sprite GetSprite()
    {
        return icon;
    }

    public int GetItemCount()
    {
        return currentStack;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public string GetItemDescription()
    {
        return itemDescription;
    }

    public int itemUsed()
    {
        if (isStackable)
        {
            currentStack -= 1;
            return currentStack;
        }

        return 0;
    }
}
