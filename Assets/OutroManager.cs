using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroManager : MonoBehaviour
{
    
    void Start()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlayMusic("BackGround1");
    }

    
}
