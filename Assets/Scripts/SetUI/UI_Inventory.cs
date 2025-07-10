using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    public static UI_Inventory Instance;

    public Inventory inventory;
    [SerializeField] private Transform itemSlot;
    [SerializeField] private Transform itemTemp;

    private void Awake()
    {
        Instance = this;
        inventory = new Inventory();
        inventory.AddItem(new Item {itemType=Item.ItemType.HealthPotion,amount=1 });
        inventory.AddItem(new Item { itemType = Item.ItemType.ManaPotion,amount = 1 });
    }

    private void Start()
    {
        RefreshInventory();
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

    }
    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        foreach (Transform child in itemSlot)
        {
            if (child == itemTemp) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotSize = 60f;

        foreach (Item item in inventory.GetitemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemTemp, itemSlot).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotSize, y * itemSlotSize);

            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            ItemSet UseItem = image.GetComponent<ItemSet>();
            UseItem.SetItem(item);
            x++;
            if (x > 6)
            {
                x = 0;
                y++;
            }
        }
    }
}