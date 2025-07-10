using System.Collections;
using UnityEngine;

public class BossMap5 : BossMap3
{
    [SerializeField] private GameObject kiemKhi;
    [SerializeField] protected GameObject vuNo;

    protected override void Awake()
    {
        isDead = false;
        canSkill = false;
        canAttack = false;
        canRun = false;
        base.Awake();
    }

    protected override void Start()
    {
        StartCoroutine(Rest());
        AudioManager.instance.PlayMusic("Boss5");

    }

    protected override void CheckAttack()
    {
        if (canSkill)
        {
            StartCoroutine(SetSkill());
        }
        else if (canAttack)
        {
            StartCoroutine(SetAttack());
        }
        LookAtPlayer();

    }

    protected override IEnumerator SetAttack()
    {
        canAttack = false;
        Debug.Log("SetAttack");
        rb.velocity = Vector2.zero;
        SetBoolStateAnimation("Attack", true);
        SetStateBlendTree("isAttack", 0);
        yield return new WaitForSeconds(1f);
        SetBoolStateAnimation("Attack", false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Rest());
    }

    protected override IEnumerator SetSkill()
    {
        canRun = true;
        canSkill = false;
        yield return new WaitForSeconds(0.5f);
        canRun = false;
        warnSkillBoss.SetActive(true);
        rb.velocity = Vector2.zero;
        SetBoolStateAnimation("Attack", true);
        SetStateBlendTree("isAttack", 1);
        rb.velocity = new Vector2(0, 15);
        yield return new WaitForSeconds(1f);
        SetBoolStateAnimation("Attack", false);
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }

    protected IEnumerator GoToSpawnPos()
    {
        SetStateBlendTree("isRun", 1);
        while (Vector2.Distance(transform.position, spawnPos) > 1f)
        {
            Vector2 direction = (spawnPos - (Vector2)transform.position).normalized;
            rb.velocity = direction * enemyInfo.Speed;
            LookAtPlayer();
            yield return new WaitForFixedUpdate();
        }
        rb.velocity = Vector2.zero;
        SetStateBlendTree("isRun", 0);
    }

    protected virtual IEnumerator Rest()
    {
        canRun = false;
        Debug.Log("SetRest");
        yield return StartCoroutine(GoToSpawnPos());
        yield return new WaitForSeconds(5f);
        canSkill = true;
    }

    protected void WaterBoomSpawn()
    {
        vuNo.SetActive(true);
    }

    protected void Shoot()
    {
        Vector2 spawnpos = transform.position;
        spawnpos.x += transform.localScale.x > 0 ? -3 : 3;
        Instantiate(kiemKhi, spawnpos, Quaternion.identity).SetActive(true);
    }
    protected override IEnumerator SetEnrage()
    {
        rb.velocity = Vector2.zero;
        canEnrage = false;
        canAttack = false;
        canRun = false;
        isDead = true;
        canSkill = false;
        SetTriggerStateAnimation("Enrage");
        enemyInfo.MaxHealth = 120;
        enemyInfo.Health = 120;
        enemyInfo.Speed += 2;
        transform.Find("Hitbox").GetComponent<HitBox>().SetDameHitBox(15);
        yield return new WaitForSeconds(5f);
        canSkill = true;
        isDead = false;
    }
}