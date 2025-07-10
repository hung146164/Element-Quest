using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOverSet : MonoBehaviour
{
    void setGameOver()
    {
        //doi 1s;
        StartCoroutine(doi1s());
    }
    IEnumerator doi1s()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;

    }
}
