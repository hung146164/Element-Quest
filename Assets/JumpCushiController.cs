using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCushiController : MonoBehaviour
{
    [SerializeField]private float bounce = 22f;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SetJumpC());
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);

        }
    }
    IEnumerator SetJumpC()
    {
        animator.SetBool("On", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("On", false);
        
    }
}
