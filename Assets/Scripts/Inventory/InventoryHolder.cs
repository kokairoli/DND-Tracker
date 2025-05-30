using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private List<ItemSO> items;

    public List<ItemSO> GetItems()
    {
        return items;
    }

    public int SubstractItemCount(ItemSO item)
    {
        bool found = false;
        int i = 0;
        int count = 1;

        while(!found && i < items.Count)
        {
            if (items[i].GetItemName() == item.GetItemName())
            {
                found = true;
                count = items[i].GetItemCount();
                if (items[i].itemUsed() < 1)
                {
                    Debug.Log("item " + i + " removed");
                    items.RemoveAt(i);
                }
                Debug.Log("Item count substracted");
            }
            else
            {
                i++;
            }
        }
        return count;
    }
}
