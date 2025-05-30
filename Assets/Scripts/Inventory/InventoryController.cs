using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static Unity.VisualScripting.Member;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private BattleController battleController;
    [SerializeField] private UIController uiController;
    [SerializeField] private InventoryUIController inventoryUIController;
    public int UseItem(ItemSO item, int index)
    {
        ActionUnit target = battleController.GetSelectedUnit();
        switch (item.GetItemType())
        {
            case ItemType.HealthPotion:
                target.Heal(((ConsumableSO)item).GetHealthRestored());
                break;
            case ItemType.ActionPotion:
                target.ResetActions();
                break;
            default:
                Debug.Log("Item type not recognized");
                break;
        }

        target.GetStats().SubstractCost(item.GetCost());
        uiController.UpdateResourcesPanel(target.GetStats().GetResources());
        int remaining = target.ItemUsed(item);
        if (remaining < 1)
        {
            inventoryUIController.removeChild(index);
        }
        uiController.CloseInventoryPanel();
        return remaining;
    }
}
