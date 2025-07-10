using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShop : MonoBehaviour
{
    [SerializeField] Shop_UI shop_ui;
    private void OnMouseDown()
    {
        shop_ui.TurnOnShop();
        AudioManager.instance.PlaySFX("UIClick");
    }
}
