using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private GameObject GameOverUI;
    Transform GroundCheck;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GroundCheck=transform.Find("GroundCheck");
        PlayerInfo.instance.OnHealthChange += HandleHealthChange;
    }

    private void OnDestroy()
    {
        if (PlayerInfo.instance != null)
        {
            PlayerInfo.instance.OnHealthChange -= HandleHealthChange;
        }
    }

    public void AnimState(string state, bool status)
    {
        animator.SetBool(state, status);
    }

    public void AnimType(string state, float status)
    {
        animator.SetFloat(state, status);
    }

    public void SetAnimAttack(float stateAttack)
    {
        animator.SetBool("Attack", true);
        animator.SetFloat("isAttack", stateAttack);
    }

    public void SetAnimMove(float playerVelocity)
    {
        AnimState("Move", true);
    }

    public void SetAnimJump()
    {
        AnimState("Jump", true);
        if (rb.velocity.y > 0.1)
        {
            GroundCheck.gameObject.SetActive(false); 
            AnimType("isJump", 0);
        }
        else if (rb.velocity.y <= 0.1)
        {
            GroundCheck.gameObject.SetActive(true);
            AnimType("isJump", 1);
        }
    }

    public void SetHurt()
    {
        animator.SetBool("Hurt", true);
        StartCoroutine(ResetHurt());
    }

    private IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Hurt", false);
    }

    private void HandleHealthChange()
    {
        SetHurt();
        if (PlayerInfo.instance.Health < 1)
        {
            AnimState("Died", true);
            AudioManager.instance.PlaySFX("GameOver");
            ActivateGameOverUI();
        }
    }

    private void ActivateGameOverUI()
    {
        GameOverUI.SetActive(true);
    }
}