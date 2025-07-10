using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] GameObject coin;
    public static CoinManager instance;
    public Text NoticeNotEnCoin;
    Text coinText;
    public event Action OnCoinChanged;
    int coins=2;
    public int Coins { get => coins; set { 
            if(coins!=value)
            {
                coins = value;
                OnCoinChanged?.Invoke();
            }
        } 
    }
    public void AddCoins(int amount)
    {
        Coins+= amount;
        AudioManager.instance.PlaySFX("GetCoin");
        coin.SetActive(true);
    }
    private void Awake()
    {
        OnCoinChanged += SetCoinText;
        instance = this;
        coinText = transform.Find("Cointext").GetComponent<Text>();
        SetCoinText();
    }
    private void OnDestroy()
    {
        OnCoinChanged -= SetCoinText;
    }
    void SetCoinText()
    {
        coinText.text = coins.ToString();
    }

}
