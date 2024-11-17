using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu (fileName = "New Item", menuName = "Lost Island/Inventory/New Item")]
public class ItemsSO : ScriptableObject
{
    [Header("Type")]
    public ItemType itemType;

    public enum ItemType { Generic, Consumable, Weapon, MeleeWeapon}

    [Header("General")]
    public Sprite icon;
    public string itemName;
    public string description = "New Item Description";
    [Space]
    public bool isStackable;
    public int maxStack = 1;

    [Header ("Consumable")]
    public float healthChange = 10f;
    public float hungerChange = 10f;
    public float thirstChange = 10f;
}
