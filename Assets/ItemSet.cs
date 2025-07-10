using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSet : MonoBehaviour
{
    private Item item;


    public void SetItem(Item item)
    {
        this.item = item;
    }

    public void UseItem()
    {
        Debug.Log("Using item: " +item.itemType);
        if(item.itemType ==Item.ItemType.HealthPotion)
        {
            UseHealthPotion();
        }
        else if(item.itemType==Item.ItemType.ManaPotion)
        {
            UseManaPotion();
        }  
        else if(item.itemType==Item.ItemType.ResetPowerPotion)
        {
            ResetPointPotion();
        }
    }
    public void UseHealthPotion()
    {
        PlayerInfo.instance.Health += 20;
        UI_Inventory.Instance.inventory.RemoveItem(item);
    }
    public void UseManaPotion()
    {
        PlayerInfo.instance.Mana += 20;
        UI_Inventory.Instance.inventory.RemoveItem(item);
    }
    public void ResetPointPotion()
    {
        UI_Inventory.Instance.inventory.RemoveItem(item);
    }
}

