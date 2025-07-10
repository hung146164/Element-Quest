using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private TrailRenderer trailRenderer;
    private Rigidbody2D rb;
    private PlayerAnimation setAnim;

    
    [SerializeField] private float dashTime = 0.4f;
    [SerializeField] private float runSFXInterval = 0.5f;
    //[SerializeField] private Transform wallCheck;
    //[SerializeField] private LayerMask wallLayer;


    //private bool isWallSliding;
    //private float wallSlidingSpeed = 2f;
    private float lastRunSFXTime;
    public bool isAttacking = false;
    private bool canJump = true;
    private bool isDashing = false;
    private bool isMoving = false;
    private float xInput;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setAnim = GetComponent<PlayerAnimation>();
        trailRenderer = GetComponent<TrailRenderer>();
        lastRunSFXTime = -runSFXInterval;
    }

    private void Update()
    {
        SetPlayerMoveHorizontal();
        SetPlayerJump();
        Flip();
        //WallSlided();
    }
    //private bool IsWalled()
    //{
    //    return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    //}
    //private void WallSlided()
    //{
    //    if (IsWalled() && !canJump && xInput!=0)
    //    {
    //        isWallSliding = true;
    //        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
    //    }
    //    else
    //    {
    //        isWallSliding = false;
    //    }
    //}
   

    private void SetPlayerMoveHorizontal()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        float playerVelocity = xInput * PlayerInfo.instance.Speed;

        if (!isDashing&& !isAttacking)
        {
            rb.velocity = new Vector2(playerVelocity, rb.velocity.y);
        }

        if (xInput != 0 && !isDashing && canJump)
        {
            if (!isMoving)
            {
                isMoving = true;
            }
            setAnim.SetAnimMove(playerVelocity);

            if (Time.time - lastRunSFXTime >= runSFXInterval)
            {
                AudioManager.instance.PlaySFX("Run");
                lastRunSFXTime = Time.time;
            }
        }
        else
        {
            isMoving = false;
            setAnim.AnimState("Move", false);
        }
    }

    private void Flip()
    {
        Vector3 newScale = transform.localScale;

        if (rb.velocity.x>0.1)
        {
            newScale.x = Mathf.Abs(newScale.x) * Mathf.Sign(xInput);

        }
        else if(rb.velocity.x < 0.1 && newScale.x>0)
        {
            newScale.x = Mathf.Abs(newScale.x) * Mathf.Sign(xInput);

        }
        transform.localScale = newScale;

    }

    private void SetPlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            AudioManager.instance.PlaySFX("Jump");
            canJump = false;
            rb.velocity = new Vector2(rb.velocity.x, PlayerInfo.instance.JumpForce);
        }
        if (!canJump)
        {
            setAnim.SetAnimJump();
        }
    }

    private void Dash( float dashVelocity)
    {
        StartCoroutine(WaitForDash(dashVelocity));
    }

    private IEnumerator WaitForDash(float dashVelocity)
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashVelocity, 0f);
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        rb.velocity = Vector2.zero;
        trailRenderer.emitting = false;
        trailRenderer.Clear();
        isDashing = false;
    }

    public void SetCanJump(bool value)
    {
        canJump = value;
    }

    public void SetAnimState(string state, bool status)
    {
        setAnim.AnimState(state, status);
    }
}