using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;
    protected PlayerAnimation setAnim;
    protected bool canSkill1 = true;
    protected bool canSkill2 = true;
    protected bool canSkill3 = true;
    protected bool canSkill4 = true;
    protected bool canSkill5 = true;
    protected bool canSkill6 = true;
    protected bool isWaiting = false;
    public bool isparry = false;
    List<GameObject> skillImage;
    List<float> timeCooldownskill;

    [SerializeField] private GameObject fireObj;
    [SerializeField] private float waitForNextSkillTime = 0.3f;
    private Rigidbody2D rb;
    private Transform Skill6Obj;
    private void Awake()
    {
        instance = this;
        setAnim = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody2D>();
        Skill6Obj = transform.Find("ef");
        
    }
    private void Start()
    {
        skillImage = SkillUI.instance.GetSkillImageList();
        timeCooldownskill = SkillUI.instance.GetSkillcoolDownTime();
    }

    private void Update()
    {
        if(!isparry)
        {
            HandleAttack();
        }
    }

    private void HandleAttack()
    {
        if (Input.GetKey(KeyCode.J) && canSkill1 && !isWaiting && PlayerInfo.instance.Mana>=2)
        {
            PlayerInfo.instance.Mana -= 2;
            isWaiting = true;
            Attack(timeCooldownskill[0], skillImage[0].GetComponent<Image>(), StateAttack.attack, SkillType.Skill1,0.2f);
            AudioManager.instance.PlaySFX("Attack");
        }
        else if (Input.GetKey(KeyCode.U) && canSkill4 && !isWaiting && PlayerInfo.instance.Mana>=5 && LevelManager.instance.currentLevel>1)
        {
            PlayerInfo.instance.Mana -= 5;
            isWaiting = true;
            Attack(timeCooldownskill[3], skillImage[3].GetComponent<Image>(), StateAttack.fireAttack, SkillType.Skill4, 0.2f);
            AudioManager.instance.PlaySFX("Fire");
        }
        else if (Input.GetKey(KeyCode.O) && canSkill2&& PlayerInfo.instance.Mana >= 3 && !isWaiting)
        {
            PlayerInfo.instance.Mana -= 3;
            isWaiting = true;
            Attack(timeCooldownskill[1], skillImage[1].GetComponent<Image>(), StateAttack.shield, SkillType.Skill2, 0.5f);
            AudioManager.instance.PlaySFX("Shield");
            isparry= true;
            PlayerMovement.instance.isAttacking = true;

        }
        else if (Input.GetKey(KeyCode.I) && canSkill5 && !isWaiting && PlayerInfo.instance.Mana >= 10 && LevelManager.instance.currentLevel>2)
        {
            PlayerInfo.instance.Mana -= 10;
            isWaiting = true;
            AudioManager.instance.PlaySFX("DashAttack");
            Attack(timeCooldownskill[4], skillImage[4].GetComponent<Image>(), StateAttack.luotChem, SkillType.Skill5, 0.2f);
        }
        else if(Input.GetKey(KeyCode.L) && canSkill3 && !isWaiting && PlayerInfo.instance.Mana >= 3 && LevelManager.instance.currentLevel > 0)
        {
            PlayerInfo.instance.Mana -= 3;
            isWaiting = true;
            AudioManager.instance.PlaySFX("Dash");

            Attack(timeCooldownskill[2], skillImage[2].GetComponent<Image>(), StateAttack.dash, SkillType.Skill3, 0.2f);
        }
        else if (Input.GetKey(KeyCode.P) && canSkill6 && !isWaiting && PlayerInfo.instance.Mana >= 10 && LevelManager.instance.currentLevel > 3)
        {
            Debug.Log("GG");
            PlayerInfo.instance.Mana -= 10;
            isWaiting = true;
            Attack(timeCooldownskill[5], skillImage[5].GetComponent<Image>(), StateAttack.enrage, SkillType.Skill6, 0.2f);
            StartCoroutine(SetSkill6());
        }
        else
        {
            setAnim.AnimState("Attack", false);
        }
    }
    protected void ResetParry()
    {
        isparry = false;
        PlayerMovement.instance.isAttacking = true;
    }
    private void Attack(float cooldown, Image skillImage, float attackType, SkillType skillType,float timeWaitToMove)
    {
        rb.velocity = new Vector2(0,rb.velocity.y);
        PlayerMovement.instance.isAttacking = true;
        SetSkillAvailability(skillType, false);
        StartCoroutine(Cooldown(cooldown, skillImage, () => SetSkillAvailability(skillType, true),timeWaitToMove));
        setAnim.AnimState("Attack", true);
        setAnim.AnimType("isAttack", attackType);
        StartCoroutine(WaitBetweenSkills(waitForNextSkillTime));
    }

    private IEnumerator Cooldown(float time, Image skillImage, System.Action onCooldownComplete,float timeWaitToMove)
    {
        skillImage.fillAmount = 0;
        float elapsed = 0;
        yield return new WaitForSeconds(timeWaitToMove);
        PlayerMovement.instance.isAttacking = false;
        while (elapsed < time)
        {
            yield return null;
            elapsed += Time.deltaTime;
            skillImage.fillAmount = elapsed / time;
        }

        skillImage.fillAmount = 1;
        onCooldownComplete.Invoke();
    }

    private void SetSkillAvailability(SkillType skillType, bool isAvailable)
    {
        switch (skillType)
        {
            case SkillType.Skill1:
                canSkill1 = isAvailable;
                break;
            case SkillType.Skill2:
                canSkill2 = isAvailable;
                break;
            case SkillType.Skill3:
                canSkill3 = isAvailable;
                break;
            case SkillType.Skill4:
                canSkill4 = isAvailable;
                break;
            case SkillType.Skill5:
                canSkill5=isAvailable;
                break;
            case SkillType.Skill6:
                canSkill6=isAvailable;
                break;
        }
    }

    private IEnumerator WaitBetweenSkills(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
    IEnumerator SetSkill6()
    {
        PlayerInfo.instance.Damage += 10;
        PlayerInfo.instance.Armor += 10;
        Skill6Obj.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        Skill6Obj.gameObject.SetActive(false);
        PlayerInfo.instance.Damage -= 10;
        PlayerInfo.instance.Armor -= 10;
    }
    private enum SkillType
    {
        Skill1,
        Skill2,
        Skill3,
        Skill4,
        Skill5,
        Skill6,
    }

    private void SpawnFire()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.x += transform.localScale.x > 0 ? 0.5f : -0.5f;
        Instantiate(fireObj, spawnPos, Quaternion.identity).SetActive(true);
    }
}