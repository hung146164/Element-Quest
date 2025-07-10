using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Slider progressBar;
    public GameObject Loadingobject;
    public TMP_Text loadingText;
    public void LoadScene(string sceneToLoad)
    {
        Loadingobject.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }
    IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
                progressBar.value = progress;
            loadingText.text = $"Loading... {(int)(progress * 100)}%";
            if (operation.progress >= 0.9f)
            {

                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
