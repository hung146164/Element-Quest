using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Vector2 playerSpawnPos;
    public static LevelManager instance;
    public Transform[] levelMaps;
    public int currentLevel;
    [SerializeField] int firstlevel=0;
    List<GameObject> skillImage;
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
    private void Start()
    {
        AudioManager.instance.PlayMusic("BackGround");
        skillImage = SkillUI.instance.GetSkillImageList();
        LoadAnyLevel(firstlevel);
    }
    public void LoadAnyLevel(int level)
    {
        if (level >= 0 && level < levelMaps.Length)
        {
            DisableCurrentLevel();
            currentLevel = level;
            ActivateCurrentLevel();
        }
        else
        {
            Debug.LogError("Invalid level index!");
        }
    }

    public void LoadNextLevel()
    {
        if (currentLevel + 1 < levelMaps.Length)
        {
            LoadAnyLevel(currentLevel + 1);
        }
        else
        {
            sceneManager.instance.GotoNextScene();
        }
    }
    private void DisableCurrentLevel()
    {
        if (levelMaps[currentLevel] != null)
        {
            levelMaps[currentLevel].gameObject.SetActive(false);
            BossUI.instance.DisableWinUI();
        }
    }
  
    private void ActivateCurrentLevel()
    {
        if (levelMaps[currentLevel] != null)
        {
            levelMaps[currentLevel].gameObject.SetActive(true);
            SetPositionPlay();
            BackgroundSet.instance.SetSpriteBackGround(currentLevel);
            BossUI.instance.SetupBoss(currentLevel);
            Time.timeScale = 1f;
            for (int i = 0; i <= currentLevel + 1; i++)
            {
                skillImage[i].SetActive(true);
            }
            AudioManager.instance.PlayMusic($"BackGround{currentLevel}");
        }
    }
    private void SetPositionPlay()
    {
        Player.transform.position = playerSpawnPos;
    }
}