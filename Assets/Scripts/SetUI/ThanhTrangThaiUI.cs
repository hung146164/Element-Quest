using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThanhTrangThaiUI : MonoBehaviour
{
    [SerializeField] Image HpImage;
    [SerializeField] Image ManaImage;
    [SerializeField] Text HpText;
    [SerializeField] Text ManaText;
    private void Start()
    {
        PlayerInfo.instance.OnHealthChange += ChangeHealth;
        PlayerInfo.instance.OnManaChange += ChangeMana;

        //cap nhat lan dau
        ChangeHealth();
        ChangeMana();
    }
    void ChangeHealth()
    {
        HpImage.fillAmount= PlayerInfo.instance.Health/ PlayerInfo.instance.MaxHealth;
        HpText.text = PlayerInfo.instance.Health.ToString();
    }
    void ChangeMana()
    {
        ManaImage.fillAmount = PlayerInfo.instance.Mana / PlayerInfo.instance.MaxMana;
        ManaText.text = PlayerInfo.instance.Mana.ToString();
    }
    private void OnDestroy()
    {
        // Hủy đăng ký sự kiện để tránh lỗi tham chiếu null khi đối tượng bị hủy
        if (PlayerInfo.instance != null)
        {
            PlayerInfo.instance.OnHealthChange -= ChangeHealth;
            PlayerInfo.instance.OnManaChange -= ChangeMana;
        }
    }
}
