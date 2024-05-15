using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryItems : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;
    Inventory inv;
    public CanvasGroup canvasGroup {get; private set;}
    public Image destroyedImage;

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
        inv = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
    }
    void OnDestroy()
    {
        itemIcon = destroyedImage;
        inv.Coins++;
    }
    public void Initialize(Item item, InventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;
        itemIcon.sprite = item.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
    }
}
