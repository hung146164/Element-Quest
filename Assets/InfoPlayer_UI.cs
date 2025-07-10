using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPlayer_UI : MonoBehaviour
{
    Transform increaseDameOj;
    Transform increaseHpMaxOj;
    Transform increaseManaMaxOj;
    Transform increaseArmorOj;
    Text scoreText;
    private void Awake()
    {
        increaseDameOj = transform.Find("IncreaseDame");
        increaseHpMaxOj = transform.Find("IncreaseHpMax");
        increaseManaMaxOj = transform.Find("IncreaseManaMax");
        increaseArmorOj = transform.Find("IncreaseArmor");
        //sự kiện khi nhấn nút tăng dame
        increaseDameOj.Find("IncreaseDameBtn").GetComponent<Button>().onClick.AddListener(IncreaseDame);
        //sự kiện khi nhấn nút tăng Hp max
        increaseHpMaxOj.Find("IncreaseHpMaxBtn").GetComponent<Button>().onClick.AddListener(IncreaseHpMax);
        //sự kiện khi nhấn nút tăng Mana max
        increaseManaMaxOj.Find("IncreaseManaMaxBtn").GetComponent<Button>().onClick.AddListener(IncreaseManaMax);
        //sự kiện khi nhấn nút tăng Armor
        increaseArmorOj.Find("IncreaseArmorBtn").GetComponent<Button>().onClick.AddListener(IncreaseArmor);
        PlayerInfo.instance.OnScoreChange += UpdateScoreText;
    }
    private void OnDestroy()
    {
        PlayerInfo.instance.OnScoreChange-=UpdateScoreText;
    }
    private void Start()
    {
        //cap nhat ui lan dau 
        IncreaseDame();
        IncreaseHpMax();
        IncreaseManaMax();
        IncreaseArmor();
    }
    public void IncreaseDame()
    {
        
        if(PlayerInfo.instance.Scores>0)
        {
            PlayerInfo.instance.Scores--;
            Text increaseDameText = increaseDameOj.Find("Text").GetComponent<Text>();
            float dame =PlayerInfo.instance.Damage++;
            increaseDameText.text = $"Dame: {dame}";
        }
    }
    public void IncreaseHpMax()
    {
        if (PlayerInfo.instance.Scores > 0)
        {
            PlayerInfo.instance.Scores--;
            Text increaseHpMaxText = increaseHpMaxOj.Find("Text").GetComponent<Text>();
            float hpMax = PlayerInfo.instance.MaxHealth++;
            increaseHpMaxText.text = $"Hp Max: {hpMax}";
        }
    }
    public void IncreaseManaMax()
    {
        if (PlayerInfo.instance.Scores > 0)
        {
            PlayerInfo.instance.Scores--;
            Text increaseManaMaxText = increaseManaMaxOj.Find("Text").GetComponent<Text>();
            float manaMax = PlayerInfo.instance.MaxMana++;
            increaseManaMaxText.text = $"Mana Max: {manaMax}";
        }
    }
    public void IncreaseArmor()
    {
        if (PlayerInfo.instance.Scores > 0)
        {
            PlayerInfo.instance.Scores--;
            Text increaseArmorText = increaseArmorOj.Find("Text").GetComponent<Text>();
            float armor = PlayerInfo.instance.Armor++;
            increaseArmorText.text = $"Armor: {armor}";
        }
    }
    public void UpdateScoreText()
    {
        scoreText = transform.Find("Scores").GetComponent<Text>();
        scoreText.text = $"Score: {PlayerInfo.instance.Scores}";
    }

}
