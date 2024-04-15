using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryItems : MonoBehaviour, IPointerClickHandler
{
    Image icon;
    public CanvasGroup canvasGroup {get; private set;}

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        icon = GetComponent<Image>();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;
        icon.sprite = item.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.PlayerInventory.SetCarriedItem(this);
        }
    }
}
