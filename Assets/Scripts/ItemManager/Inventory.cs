using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    List<Item> itemsList;

    public Inventory()
    {
        itemsList= new List<Item>();
    }
    public List<Item> GetitemList()
    {
        return itemsList;   
    }

    public void AddItem(Item item)
    {
        if(item.IsStackable())
        {
            bool ItemAlreadyinInventory = false;
            foreach(Item inventoryItem in itemsList)
            {
                if(inventoryItem.itemType==item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    ItemAlreadyinInventory = true;
                }
            }
            if(!ItemAlreadyinInventory)
            {
                itemsList.Add(item);
            }
        }

        else
        {
            itemsList.Add(item);
        }
        OnItemListChanged?.Invoke(this,EventArgs.Empty);
    }
    public void RemoveItem(Item item)
    {
        if (item.amount > 1)
        {
            item.amount--;
        }
        else
        {
            itemsList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

}
