using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAsset : MonoBehaviour
{
    public static ItemAsset instance;

    private void Awake()
    {
        instance = this;
    }
    public Transform pfItemWorld;

    public Sprite healthpotionSprite;
    public Sprite manaPotionSprite;
    public Sprite resetPowerPotion;

}
