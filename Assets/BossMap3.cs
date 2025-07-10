using System.Collections;
using UnityEngine;

public class BossMap3 : MonoBehaviour
{
    // Variables
    [SerializeField] protected Transform player;
    [SerializeField] protected GameObject NextDoor;
    protected TrailRenderer trailRenderer;
    [SerializeField] protected GameObject warnSkillBoss;
    protected Rigidbody2D rb;
    protected Animator animator;
    [SerializeField] protected Vector2 spawnPos;
    [SerializeField] protected float rangeattack;

    protected EnemyInfo enemyInfo;

    protected bool canRun = true;
    protected bool canAttack = true;
    protected bool canEnrage = true;
    protected bool canSkill = true;
    protected bool isDead = true;

    protected virtual void Awake()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        trailRenderer = GetComponent<TrailRenderer>();
        enemyInfo.OnHealthChange += CheckHurt;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        StartCoroutine(WaitIntro());
        AudioManager.instance.PlayMusic("Boss3");
    }

    protected virtual IEnumerator WaitIntro()
    {
        yield return new WaitForSeconds(5f);
        isDead = false;
    }

    protected virtual void OnDestroy()
    {
        enemyInfo.OnHealthChange -= CheckHurt;
    }

    protected virtual void FixedUpdate()
    {
        ControlBoss();
    }

    protected virtual void ControlBoss()
    {
        if (!isDead)
        {
            MoveBoss();
            CheckAttack();
        }
    }

    protected virtual void MoveBoss()
    {
        if (canRun)
        {
            float player_X_Pos = player.transform.position.x;
            float target_Pos = (player_X_Pos - transform.position.x) * enemyInfo.Speed;
            rb.velocity = new Vector2(target_Pos, rb.velocity.y);
            SetStateBlendTree("isRun", 1);
            LookAtPlayer();
        }
        else
        {
            SetStateBlendTree("isRun", 0);
        }
    }

    protected virtual void CheckAttack()
    {
        float distancefromplayer = Vector2.Distance(transform.position, player.position);
        if (distancefromplayer < rangeattack && canAttack)
        {
            StartCoroutine(SetAttack());
        }
        else if (canSkill && canAttack)
        {
            StartCoroutine(SetSkill());
        }
        else if (canRun)
        {
            SetBoolStateAnimation("Attack", false);
        }
    }

    protected virtual IEnumerator SetAttack()
    {
        canAttack = false;
        canRun = false;
        rb.velocity = Vector2.zero;
        SetBoolStateAnimation("Attack", true);
        SetStateBlendTree("isAttack", 0);
        yield return new WaitForSeconds(0.5f);
        canRun = true;
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }

    protected virtual IEnumerator SetSkill()
    {
        warnSkillBoss.SetActive(true);
        canSkill = false;
        canAttack = false;
        canRun = false;
        rb.velocity = Vector2.zero;
        SetBoolStateAnimation("Attack", true);
        SetStateBlendTree("isAttack", 1);
        yield return new WaitForSeconds(0.5f);
        canRun = true;
        yield return new WaitForSeconds(1f);
        canAttack = true;

        yield return new WaitForSeconds(5f);
        canSkill = true;
    }

    protected virtual void CheckHurt()
    {
        rb.velocity = Vector2.zero;
        canRun = false;
        if (enemyInfo.Health < 1)
        {
            if (canEnrage)
            {
                StartCoroutine(SetEnrage());
                return;
            }
            SetTriggerStateAnimation("Dead");
            isDead = true;
            tag = "Player";
            canRun = false;
            canAttack = false;
            canSkill = false;
            NextDoor.SetActive(true);
            return;
        }
        else if (canAttack)
        {
            StartCoroutine(SetHurt());
        }
    }

    protected virtual IEnumerator SetEnrage()
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
        canRun = true;
        canAttack = true;
    }

    protected virtual IEnumerator SetHurt()
    {
        SetBoolStateAnimation("Hurt", true);
        yield return new WaitForSeconds(0.5f);
        SetBoolStateAnimation("Hurt", false);
        canRun = true;
    }

    protected virtual void Dash()
    {
        float distance = player.position.x - transform.position.x;
        float velocityAttack = distance * (enemyInfo.Speed + 6);
        rb.velocity = new Vector2(velocityAttack, rb.velocity.y);
    }

    protected void SetStateBlendTree(string name, int state)
    {
        animator.SetFloat(name, state);
    }

    protected void SetBoolStateAnimation(string name, bool active)
    {
        animator.SetBool(name, active);
    }

    protected void SetTriggerStateAnimation(string name)
    {
        animator.SetTrigger(name);
    }

    protected virtual void LookAtPlayer()
    {
        Vector3 newScale = transform.localScale;
        if (player.position.x > transform.position.x)
        {
            newScale.x = Mathf.Abs(newScale.x);
        }
        else if (player.position.x < transform.position.x)
        {
            newScale.x = -Mathf.Abs(newScale.x);
        }
        transform.localScale = newScale;
    }
    
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeattack);
    }
}
