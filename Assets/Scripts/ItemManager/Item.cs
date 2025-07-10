using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        HealthPotion,
        ManaPotion,
        ResetPowerPotion,
    }
    public ItemType itemType;
    public int amount;
    public int cost = 0;
    public int sellcost = 0;
    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.HealthPotion:     return ItemAsset.instance.healthpotionSprite;
            case ItemType.ManaPotion:       return ItemAsset.instance.manaPotionSprite;
            case ItemType.ResetPowerPotion: return ItemAsset.instance.resetPowerPotion;
        }
    }
    public bool IsStackable()
    {
        switch(itemType)
        {
            default:
            case ItemType.HealthPotion:
            case ItemType.ManaPotion:
            case ItemType.ResetPowerPotion:
                return true;
        }
    }
}
