using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private InventorySlot inventorySlotPrefab;
    [SerializeField] private InventoryItem inventoryItemPrefab;
    [SerializeField] private InventoryController inventoryController;

    public void CreateInventory(List<ItemSO> items)
    {
        foreach (ItemSO item in items)
        {
            InventorySlot newSlot = Instantiate(inventorySlotPrefab, transform);
            InventoryItem inventoryItem = Instantiate(inventoryItemPrefab, newSlot.transform);
            inventoryItem.GetComponent<InventoryItem>().SetInventoryController(inventoryController);
            inventoryItem.SetItemInSlot(item);
        }
    }

    public void FlushInventory()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void removeChild(int index)
    {
        Destroy(transform.GetChild(index).gameObject);
    }
}
