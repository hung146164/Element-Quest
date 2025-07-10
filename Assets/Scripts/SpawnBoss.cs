using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] GameObject[] Wallboss;
    [SerializeField] GameObject WalltoBoss;
    [SerializeField] GameObject Key;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Key.activeSelf)
            {
                string textkey= "Vui lòng tìm khóa!\n(Cuối Map)";
                BossUI.instance.SetActiveTextKey(textkey);
            }
            else
            {
                ActiveBoss();
            }
        }
    }
    void ActiveBoss()
    {
        //xay tuong
        WalltoBoss.SetActive(false);
        foreach (GameObject wall in Wallboss)
        {
            wall.SetActive(true);
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //SpawnBoss
        BossUI.instance.SpawnBoss();
    }
    
   

    
}