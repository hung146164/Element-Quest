using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    public event Action OnHealthChange;
    public event Action OnManaChange;
    public event Action OnDameChange;
    public event Action OnArmorChange;
    public event Action OnScoreChange;
    HitBox hitBox;
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

        hitBox = transform.Find("Hitbox").GetComponent<HitBox>();   
    }
    private void Start()
    {
        hitBox.SetDameHitBox(damage);
    }
    [SerializeField]private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float mana;
    [SerializeField] private float maxMana;
    [SerializeField] private float armor;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private int scores=5;

    public float Health
    {
        get => health; 
        set
        {
            if (health != value)
            {
                health = value;

                health = Mathf.Clamp(value, 0, maxHealth);
                //gọi sự kiện nếu hp có thay đổi 
                OnHealthChange?.Invoke();
            }
        }
    }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Mana {
        get => mana; 
        set
        {
            if (mana != value)
            {
                mana = value;
                mana = Mathf.Clamp(value, 0, MaxMana);
                OnManaChange?.Invoke();
            }
        }
    }
    public float MaxMana { get => maxMana; set => maxMana = value; }
    public float Armor { get => armor;
        set
        {
            if (armor != value)
            {
                armor = value;
                OnArmorChange?.Invoke();
            }
        }
    }
    public float Damage { get => damage;
        set
        {
            if(damage!=value)
            {
                damage= value;
                hitBox.SetDameHitBox(value);
                OnDameChange?.Invoke();
            }
        }
    }
    public float Speed { get => speed; set => speed = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public int Scores { get => scores;
        set
        {
            if (scores != value)
            {
                scores = value;
                OnScoreChange?.Invoke();
            }
        }
    }

}
