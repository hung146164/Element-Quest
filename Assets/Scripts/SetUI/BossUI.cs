using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public static BossUI instance;

    public Transform[] bosses;
    [SerializeField] private Text countdownText;
    [SerializeField] private Text keyText;
    [SerializeField] private GameObject hpBossUI;
    [SerializeField] private Text hpText;
    [SerializeField] private Image hpImage;
    [SerializeField] private GameObject winUI;

    private EnemyInfo enemyInfo;
    private Transform currentBoss;
    public int countdownTime = 5;

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

    public void SetupBoss(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < bosses.Length)
        {
            currentBoss = bosses[levelIndex];
            enemyInfo = currentBoss.GetComponent<EnemyInfo>();

            if (enemyInfo != null)
            {
                enemyInfo.OnHealthChange += UpdateHpUI;
                UpdateHpUI();
            }
        }
        else
        {
            Debug.LogError("Invalid boss index!");
        }
    }

    public void SetActiveTextKey(string message)
    {
        keyText.text = message;
        keyText.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterDelay(keyText, 4f));
    }

    public void SpawnBoss()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        for (int i = countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        countdownText.text = "Awake!";
        yield return new WaitForSeconds(1);
        countdownText.text = "";

        if (currentBoss != null)
        {
            currentBoss.gameObject.SetActive(true);
            hpBossUI.SetActive(true);
        }
    }

    public void ActiveWinUI()
    {
        winUI.SetActive(true);
    }
    public void DisableWinUI()
    {
        winUI.SetActive(false);
    }

    private void UpdateHpUI()
    {
        if (enemyInfo != null)
        {
            hpImage.fillAmount = enemyInfo.Health / enemyInfo.MaxHealth;
            hpText.text = $"{enemyInfo.Health} / {enemyInfo.MaxHealth}";
        }
    }

    private IEnumerator HideTextAfterDelay(Text text, float delay)
    {
        yield return new WaitForSeconds(delay);
        text.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (enemyInfo != null)
        {
            enemyInfo.OnHealthChange -= UpdateHpUI;
        }
    }
}