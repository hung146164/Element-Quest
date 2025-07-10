using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthSystemPlayer : HealthSystem
{
    public static HealthSystemPlayer instance;
    [SerializeField] protected Transform HealBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (PlayerInfo.instance != null)
        {
            PlayerInfo.instance.OnHealthChange += DeadSound;
            PlayerInfo.instance.OnHealthChange += ChangeHpBar;
        }
    }
    private void OnDestroy()
    {
        // Hủy đăng ký sự kiện để tránh lỗi tham chiếu null khi đối tượng bị hủy
        if (PlayerInfo.instance != null)
        {
            PlayerInfo.instance.OnHealthChange -= DeadSound;
            PlayerInfo.instance.OnHealthChange -= ChangeHpBar;
        }
    }
    public void ChangeHealth(float amount, bool add)
    {
        float hpChange;
        if (!add)
        {
            float standardDamage = Mathf.Ceil(amount - PlayerInfo.instance.Armor  / 5);
            hpChange = -standardDamage;
        }
        else
        {
            hpChange = amount;
        }

        if (PlayerInfo.instance != null)
        {
            PlayerInfo.instance.Health += hpChange;
            HpTextPopup(hpChange);
        }
    }

    protected override void ChangeHpBar()
    {
        if (HealBar != null && PlayerInfo.instance != null)
        {
            HealBar.localScale = new Vector3(PlayerInfo.instance.Health / PlayerInfo.instance.MaxHealth, 1, 1);
        }
    }

    protected override void DeadSound()
    {
        if (PlayerInfo.instance != null && PlayerInfo.instance.Health < 1)
        {
            AudioManager.instance.PlaySFX("Dead");
        }
    }
}