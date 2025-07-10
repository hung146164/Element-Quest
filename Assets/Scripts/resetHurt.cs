using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class resetHurt : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Hurt", false);
    }
}
