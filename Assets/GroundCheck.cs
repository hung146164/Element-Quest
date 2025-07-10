using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GroundCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayerMovement.instance.SetCanJump(true);
            PlayerMovement.instance.SetAnimState("Jump", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayerMovement.instance.SetCanJump(false);
            PlayerMovement.instance.SetAnimState("Jump", false);
        }
    }
}
