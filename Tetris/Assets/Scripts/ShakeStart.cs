using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeStart : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("Shake"))animator.Play("Game field shake");
    }
}