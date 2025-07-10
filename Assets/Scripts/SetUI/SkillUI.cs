using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public static SkillUI instance;
    [SerializeField] public List<GameObject> SkillImageList;
    [SerializeField] public List<float> coolDownTime;
    private void Awake()
    {
        instance = this;
    }
    public List<GameObject> GetSkillImageList()
    {
        return SkillImageList;
    }
    public List<float> GetSkillcoolDownTime()
    {
        return coolDownTime;
    }

    public float coolDownSkill1 = 1f;
    public float coolDownSkill2 = 2f;
    public float coolDownSkill3 = 1f;
    public float coolDownSkill4 = 2f;
    public float coolDownSkill5 = 1f;
    public float coolDownSkill6 = 10f;

    private void Start()
    {
        for(int i=0;i <SkillImageList.Count;i++)
        {
            SkillImageList[i].GetComponent<Image>().fillAmount = 1;
        }
    }
}