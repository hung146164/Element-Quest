using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AiFireController : MonoBehaviour
{
    public static AiFireController instance;
    [SerializeField] protected float lineOfSite;
    [SerializeField] protected Transform player;
    [SerializeField] protected GameObject Bullet;
    [SerializeField] GameObject NextDoor;
    [SerializeField] protected Vector3 spawnPos;

    protected Animator animator;
    protected bool isFlipped = false;
    protected EnemyInfo enemyInfo;

    protected virtual void Awake()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        enemyInfo.OnHealthChange += SetHurt;
    }
    protected virtual void OnDestroy()
    {
        enemyInfo.OnHealthChange -= SetHurt;
    }

    protected virtual void Start()
    {
        Initialize();
        AudioManager.instance.PlayMusic("Boss1");

    }

    protected void Update()
    {
        LookAtPlayer();
    }

    protected void Initialize()
    {
        InvokeRepeating("SetAnimationSkill", 2, 12);
        animator = GetComponent<Animator>();
    }

    protected virtual void SetAnimationSkill()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(SpecialSkill());
        }
    }

    protected void SetSkill()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(SpecialSkill());
        }
    }

    protected virtual IEnumerator SpecialSkill()
    {
        animator.SetBool("Attack", true);
        animator.SetFloat("isAttack", 1);
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        while (distanceFromPlayer > 3f && gameObject.activeInHierarchy)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyInfo.Speed * Time.deltaTime);
            distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(SetRest());
        }
    }

    protected virtual IEnumerator SetShoot()
    {
        animator.SetBool("Attack", true);
        animator.SetFloat("isAttack", 0);
        yield return new WaitForSeconds(4f);
        if (gameObject.activeInHierarchy)
        {
            FireBullet(3);
            animator.SetBool("Attack", false);
        }
    }

    void Shoot()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity).SetActive(true);
    }
    void FireBullet(int amount)
    {
        Vector2 spawnPos = transform.position;
        for(int i=0; i< amount;i++)
        {
            spawnPos.x += 1.5f;
            Instantiate(Bullet, spawnPos, Quaternion.identity).SetActive(true);
        }
    }

    protected virtual IEnumerator SetRest()
    {
        while (Vector2.Distance(transform.position, spawnPos) > 0.1f && gameObject.activeInHierarchy)
        {
            transform.position = Vector2.MoveTowards(transform.position, spawnPos, enemyInfo.Speed * Time.deltaTime);
            yield return null;
        }
        if (gameObject.activeInHierarchy)
        {
            animator.SetBool("Attack", false);
            yield return new WaitForSeconds(4f);
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(SetShoot());
            }
        }
    }

    protected virtual void SetHurt()
    {
        animator.SetBool("Hurt", true);
        if (enemyInfo.Health < 1)
        {
            StartCoroutine(Dead());
        }
        StartCoroutine(ResetHurt());
    }

    protected virtual IEnumerator Dead()
    {
        animator.SetBool("Died", true);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

        Vector2 itemDropPos = transform.position;
        itemDropPos.y += 5;
        NextDoor.SetActive(true);
        StopAllCoroutines();
    }

    protected IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Hurt", false);
    }

    protected void LookAtPlayer()
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
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}