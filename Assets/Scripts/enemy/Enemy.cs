using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyInfo : MonoBehaviour
{
    public event Action OnHealthChange;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float armor;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    private void Awake()
    {
        SetDameForHitBox();
    }
    public void SetDameForHitBox()
    {
        transform.Find("Hitbox").GetComponent<HitBox>().SetDameHitBox(damage);
        //nếu nhân vật thay đổi dame trong quá trình tấn công thì sửa hàm này
    }
    public float Health { get => health; set
        {
            if (health != value)
            {
                health=Mathf.Clamp(value, 0, maxHealth);
                //gọi sự kiện nếu hp có thay đổi 
                OnHealthChange?.Invoke(); 
            }
        }
    }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Armor { get => armor; set => armor = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
}
