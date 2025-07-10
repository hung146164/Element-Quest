using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap :MonoBehaviour
{
    [SerializeField] protected float damageTrap;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyTrapEffect();
        }
    }

    protected abstract void ApplyTrapEffect();
}
