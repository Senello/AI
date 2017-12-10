using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSetJump : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GameController.Instace.GetShake()) animator.SetTrigger("Jump");
    }
}