using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotTag {None, Head, Chest, Legs, Feet, Weapon}

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public SlotTag myTag;

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;
}
