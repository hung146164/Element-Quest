using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthSystemEnemy : HealthSystem
{
    
    [SerializeField] protected Transform HealBar;
    EnemyInfo enemyInfo;


    private void Awake()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        enemyInfo.OnHealthChange += DeadSound;
    }
    private void OnDestroy()
    {
        enemyInfo.OnHealthChange -= DeadSound;

    }
    public void ChangeHealth(float amount, bool add)
    {
        float hpChange;
        if (!add)
        {
            float standardDamage = Mathf.Ceil(amount - enemyInfo.Armor / 5);
            hpChange = -standardDamage;
        }
        else
        {
            hpChange = amount;
        }

        if (enemyInfo != null)
        {
            enemyInfo.Health += hpChange;
            HpTextPopup(hpChange);
        }
    }

    protected override void ChangeHpBar()
    {
        if (HealBar != null && enemyInfo != null)
        {
            HealBar.localScale = new Vector3(enemyInfo.Health / enemyInfo.MaxHealth, 1, 1);
        }
    }

    protected override void DeadSound()
    {
        if (enemyInfo != null && enemyInfo.Health < 1)
        {

            CoinManager.instance.AddCoins(4);
            PlayerInfo.instance.Scores+=1;
        }
    }
}
