using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    
    [Header("UI")]
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    private ItemSO itemInSlot;
    private InventoryController inventoryController;


    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        image.raycastTarget = false;
    }

    public void SetItemInSlot(ItemSO item)
    {
        itemInSlot = item;
        image.sprite = item.GetSprite();
        image.SetNativeSize();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int remaining = inventoryController.UseItem(itemInSlot, transform.GetSiblingIndex());
    }

    public void SetInventoryController(InventoryController invC)
    {
        inventoryController = invC;
    }
}
