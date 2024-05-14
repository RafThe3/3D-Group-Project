using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotTag {None, Tradeins, Head, Chest, Legs, Feet, Weapon}
public enum Rairity { Trash, Common, Uncommon, Rare, Epic, Legendary, FryingPan}

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public SlotTag myTag;
    public Rairity myRarity;

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;
}
