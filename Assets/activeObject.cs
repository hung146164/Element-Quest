using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeObject : MonoBehaviour
{
    [SerializeField] private GameObject Object;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        { 
            Object.SetActive(true);
        }
    }
}
