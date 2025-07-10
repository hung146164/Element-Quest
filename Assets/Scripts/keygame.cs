using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keygame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.PlaySFX("GetKey");
        gameObject.SetActive(false);

    }
}
