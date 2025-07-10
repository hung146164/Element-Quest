using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public static sceneManager instance;
    public static int current_Scene;

    private void Awake()
    {
        instance = this;
    }
    public void GotoNextScene()
    {
        current_Scene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneAsync(current_Scene + 1));
    }

    public void GotoAnyScene(int indexScene)
    {
        current_Scene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneAsync(indexScene));
    }

    public void ReloadScene()
    {
        current_Scene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneAsync(current_Scene));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Time.timeScale = 1.0f;
        AudioManager.instance.PlayMusic("BackGround1");

        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }


    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
