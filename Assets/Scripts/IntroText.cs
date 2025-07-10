using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroText : MonoBehaviour
{
    [SerializeField] Text uiText;
    [SerializeField] string[] Intro;
    [SerializeField] float timePerChar = 0.05f;
    [SerializeField] LoadingScene loadingScene;

    private int currentIndex = 0;
    private bool isWriting = false;
    private bool isTextFinished = false;

    public string sceneToLoad;
    public void AddWriter(Text uiText, string textToWrite, float timePerChar)
    {
        this.uiText = uiText;
        this.timePerChar = timePerChar;
        StartCoroutine(WriteText(textToWrite));
    }

    private void Start()
    {
        if (Intro.Length > 0)
        {
            AddWriter(uiText, Intro[currentIndex], timePerChar);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isWriting)
            {
                // If text is still being written, complete it immediately
                StopAllCoroutines();
                uiText.text = Intro[currentIndex];
                isWriting = false;
                isTextFinished = true;
            }
            else if (isTextFinished)
            {
                currentIndex++;
                if (currentIndex < Intro.Length)
                {
                    AddWriter(uiText, Intro[currentIndex], timePerChar);
                }
                else
                {

                    loadingScene.LoadScene(sceneToLoad);
                    gameObject.SetActive(false);
                }
                isTextFinished = false;
            }
        }
    }

    private IEnumerator WriteText(string textToWrite)
    {
        isWriting = true;
        uiText.text = ""; // Clear the previous text

        foreach (char c in textToWrite)
        {
            uiText.text += c;
            yield return new WaitForSeconds(timePerChar);
        }

        isWriting = false;
        isTextFinished = true;
    }
   

}