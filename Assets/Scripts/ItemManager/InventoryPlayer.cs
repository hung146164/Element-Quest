using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField]TextMeshPro TextItem;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.gameObject.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            TextItem.text= itemWorld.GetItem().amount.ToString();
            TextItem.gameObject.SetActive(true);
            UI_Inventory.Instance.inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    
   
}
