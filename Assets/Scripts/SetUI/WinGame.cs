using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    [SerializeField] GameObject Continue;

    private void OnEnable()
    {
        StartCoroutine(doi2s());
    }
    IEnumerator doi2s()
    {
        AudioManager.instance.PlaySFX("Win");
        yield return new WaitForSeconds(2f);
        Continue.SetActive(true);
        Time.timeScale = 0f;
    }
}
