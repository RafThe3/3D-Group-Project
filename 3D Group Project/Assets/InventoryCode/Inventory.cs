using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItems carriedItem;
    public int Coins = 0;

    [SerializeField] InventorySlot[] inventorySlots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItems itemPrefab;
    [SerializeField] TextMeshProUGUI coinCounter;

    [Header("Item List")]
    [SerializeField] Item[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;


    void Awake()
    {
        Singleton = this;
        giveItemBtn.onClick.AddListener(delegate{SpawnInventoryItem();});
    }

    public void SpawnInventoryItem(Item item = null)
    {
        Item item_ = item;
        if(item_ == null)
        {
            int random = Random.Range(0, items.Length);
            item_ = items[random];
        }

        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(item_, inventorySlots[i]);
                break;
            }
        }
    }

    void Update()
    {
        if(carriedItem == null) return;

        carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItems item)
    {
        if(carriedItem != null)
        {
            if(item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.myTag) return;
            item.activeSlot.SetItem(carriedItem);
        }

        if(item.activeSlot.myTag != SlotTag.None)
        {
            EquipEquipment(item.activeSlot.myTag, null);
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }

    public void EquipEquipment(SlotTag tag, InventoryItems item = null)
    {
        switch (tag)
        {
            case SlotTag.Head: break;
            case SlotTag.Chest: break;
            case SlotTag.Legs: break;
            case SlotTag.Feet: break;
            case SlotTag.Weapon: break;
        }
    }
}
