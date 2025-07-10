using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuaiController: MonoBehaviour
{
    [SerializeField] protected Transform xPos, yPos, SpawnPos;

    [SerializeField] protected Transform player;

    protected Animator animator;
    protected Rigidbody2D rb;

    protected EnemyInfo enemyInfo;
    public float lineOfSite;
    public float rangeAttack;
    public float limitRangeMove=10f;
    protected bool canMove = true;
    protected bool canAttack = true;
    protected bool canJump = true;
    protected bool movingyPos = true;

    [SerializeField]protected float rayDistance = 1.0f;
    [SerializeField]protected LayerMask groundLayer;
    [SerializeField]protected float jumpForce = 15f;
    

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyInfo = GetComponent<EnemyInfo>();
        enemyInfo.OnHealthChange += Hurt;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
    protected void Update()
    {
        CheckAttack();
        if (canMove)
        {
            MoveQuai();
        }
        CheckJump();

    }
    protected virtual void OnDestroy()
    {
        enemyInfo.OnHealthChange -= Hurt;

    }
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
        // Draw Raycast for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * rayDistance);
    }

    protected virtual void MoveQuai()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceToSpawnPos = Vector2.Distance(transform.position, SpawnPos.position);
        
        if(distanceToSpawnPos>limitRangeMove)
        {
            transform.position = SpawnPos.position;
        }
        else if (distanceToPlayer <= lineOfSite)
        {
            LookAtPlayer();
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * enemyInfo.Speed, rb.velocity.y);
        }
        else
        {
            //di qua di lai mot vi tri la 3f
            Patrol();
        }
    }
    protected virtual void Patrol()
    {
        Transform targetPoint = movingyPos ? yPos : xPos;
        Vector2 direction = (targetPoint.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * enemyInfo.Speed, rb.velocity.y);
        Vector2 newlocalscale = transform.localScale;
        if (movingyPos)
        {
            newlocalscale.x=Mathf.Abs(newlocalscale.x);
            transform.position = Vector2.MoveTowards(transform.position, yPos.position, enemyInfo.Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, yPos.position) < 0.1f)
            {
                movingyPos = false;
            }
        }
        else
        {
            newlocalscale.x = -Mathf.Abs(newlocalscale.x);

            transform.position = Vector2.MoveTowards(transform.position, xPos.position, enemyInfo.Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, xPos.position) < 0.1f)
            {
                movingyPos = true;
            }
        }
        transform.localScale=newlocalscale;
    }
    protected virtual void CheckJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x,-1) , rayDistance, groundLayer);

        if (hit.collider != null && hit.collider.CompareTag("Ground") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("Jump", true);
            canJump = false;
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }
    protected virtual void CheckAttack()
    {
        if(canAttack)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= rangeAttack)
            {
                rb.velocity = Vector2.zero;
                animator.SetBool("Attack", true);
                StartCoroutine(CoolDownForNextAttack(1f));
            }
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
    IEnumerator CoolDownForNextAttack(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }
    protected virtual void LookAtPlayer()
    {
        bool check = transform.position.x < player.position.x;
        float huong = Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector2(check ? huong : -huong, transform.localScale.y);
    }
    protected virtual void Hurt()
    {
        animator.SetBool("Hurt", true);

        if (enemyInfo.Health < 1)
        { 
            animator.SetFloat("isHurt", 1);
            AudioManager.instance.PlaySFX("Dead");
            transform.tag = "Player";
        }
        else
        {
            animator.SetFloat("isHurt", 0);
            StartCoroutine(resetHurt());
        }
    }
    protected virtual IEnumerator resetHurt()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Hurt", false);
    }
    protected virtual void DisableQuai()
    {
        gameObject.SetActive(false);
    }
    

}
