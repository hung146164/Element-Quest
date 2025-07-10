using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    [SerializeField] protected GameObject damepopup;
    protected virtual void HpTextPopup(float hpChange)
    {
        if (damepopup != null)
        {
            Vector3 spawnPos = new Vector3(transform.position.x,transform.position.y,-1);
            spawnPos.y += 0.5f;
            TextMeshPro hpText=Instantiate(damepopup, spawnPos, Quaternion.identity).GetComponent<TextMeshPro>();
            if (hpText != null)
            {
                hpText.text = hpChange.ToString();
                hpText.color = hpChange > 0 ? Color.green : Color.red;
            }
        }
        ChangeHpBar();
    }
    protected abstract void DeadSound();
    protected abstract void ChangeHpBar();


}
