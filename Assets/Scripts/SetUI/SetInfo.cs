using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInfo : MonoBehaviour
{

    [SerializeField] GameObject infoDetails;
    [SerializeField] GameObject InfoButton;
    public void active_infoDetails()
    {
        infoDetails.SetActive(true);
        InfoButton.SetActive(false);
        Time.timeScale = 0;
    }
    public void Re_active_infoDetails()
    {
        infoDetails.SetActive(false);
        InfoButton.SetActive(true);
        Time.timeScale = 1;

    }

}
