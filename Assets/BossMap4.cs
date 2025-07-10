using System.Collections;
using UnityEngine;

public class BossMap4 : BossMap3
{
    [SerializeField] private GameObject kiemKhi;
    [SerializeField] Vector2 xPos;
    [SerializeField] Vector2 yPos;


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
        AudioManager.instance.PlayMusic("Boss4");

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
        for(int i=0; i< 3;i++)
        {
            SetBoolStateAnimation("Attack", true);
            SetStateBlendTree("isAttack", 0);
            yield return new WaitForSeconds(1f);
        }
        SetBoolStateAnimation("Attack", false);
        StartCoroutine(Rest());
    }

    protected override IEnumerator SetSkill()
    {
        canSkill = false;
        SetBoolStateAnimation("Attack", true);
        SetStateBlendTree("isAttack", 1);
        for(int i=0; i< 3;i++)
        {
            transform.position = yPos;
            yield return new WaitForSeconds(0.5f);
            transform.position = xPos;
            yield return new WaitForSeconds(0.5f);
        }
        SetBoolStateAnimation("Attack", false);
        yield return new WaitForSeconds(5f);
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
        GoToSpawnPos();
        yield return new WaitForSeconds(7f);
        canSkill = true;
    }
    protected override IEnumerator SetEnrage()
    {
        rb.velocity = Vector2.zero;
        canEnrage = false;
        canAttack = false;
        canRun = false;
        isDead = true;
        SetTriggerStateAnimation("Enrage");
        enemyInfo.MaxHealth = 120;
        enemyInfo.Health = 120;
        enemyInfo.Speed += 2;
        transform.Find("Hitbox").GetComponent<HitBox>().SetDameHitBox(15);
        yield return new WaitForSeconds(5f);
        isDead = false;
        canRun = false;
        canAttack = false;
    }
    protected void TeleToPlayer()
    {
        transform.position= new Vector2(player.position.x+(player.localScale.x>0?-1f:1f), player.position.y+1f);
    }
    protected void Shoot()
    {
        Vector2 spawnpos = transform.position;
        spawnpos.x += transform.localScale.x > 0 ? -3 : 3;
        Instantiate(kiemKhi, spawnpos, Quaternion.identity).SetActive(true);
    }
}