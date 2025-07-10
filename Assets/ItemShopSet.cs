using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopSet : MonoBehaviour
{
    private Item item;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI MoneyToBuyFull;
    public int amount = 1;
    public int Price;
    public int QuantitylimitBuy = 100;
    private void Start()
    {
        Price = item.cost;
        setAmountText();
    }
    public void IncreaseAmount()
    {
        amount *= 10;
        Price*=10;
        setAmountText();
    }
    public void DecreaseAmount()
    {
        amount /= 10;
        Price /= 10 ;
        setAmountText();
    }
    public void setAmountText()
    {
        Price = Mathf.Clamp(Price, item.cost, QuantitylimitBuy* item.cost);
        amount = Mathf.Clamp(amount, 1, QuantitylimitBuy);
        amountText.text ="X"+ amount.ToString();
        MoneyToBuyFull.text = Price.ToString();
    }
    public void setItem(Item item)
    {
        this.item = item;
    }
    public void BuyItem()
    {
        if (CoinManager.instance.Coins >=Price)
        {
            CoinManager.instance.Coins-=Price;
            UI_Inventory.Instance.inventory.AddItem(new Item { amount = this.amount, itemType = item.itemType, cost = item.cost, sellcost = item.sellcost });
        }
        else
        {
            CoinManager.instance.NoticeNotEnCoin.gameObject.SetActive(true);
        }
    }
}
