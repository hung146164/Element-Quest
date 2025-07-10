using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    List<Item> listItemShop;
    Transform itemShopContainer,itemSell;
    private void Awake()
    {
        itemShopContainer = transform.Find("ItemShopContainer");
        itemSell = itemShopContainer.transform.Find("ItemSell");
        listItemShop = new List<Item>();
        AddItem(new Item{ itemType = Item.ItemType.HealthPotion,cost=2,sellcost=1,amount=1 });
        AddItem(new Item {itemType = Item.ItemType.ManaPotion, cost = 2, sellcost = 1, amount = 1 });
        AddItem(new Item {itemType = Item.ItemType.ResetPowerPotion, cost = 5, sellcost = 2, amount = 1 });
    
    }
    private void Start()
    {
        RefreshShop();
    }
    public void RefreshShop()
    {
        int x=0, y=0;
        int Width=180, Height=-130;
        foreach (Item item in listItemShop)
        {
            RectTransform itemShop = Instantiate(itemSell, itemShopContainer).GetComponent<RectTransform>();
            itemShop.anchoredPosition = new Vector2(x*Width, y*Height);
            itemShop.Find("Image").GetComponent<Image>().sprite = item.GetSprite();
            ItemShopSet infoItem= itemShop.GetComponent<ItemShopSet>();
            infoItem.setItem(item);
            x++;
            if(x>1)
            {
                x = 0;y++;
            }
            itemShop.gameObject.SetActive(true);
        }
    }
    public List<Item> GetListItemShop()
    {
        return listItemShop;
    }
    public void AddItem(Item item)
    {
        listItemShop.Add(item);
    }
    public void TurnOnShop()
    {
        gameObject.SetActive(true);
    }
    public void TurnOffShop()
    {
        gameObject.SetActive(false);
    }

}
