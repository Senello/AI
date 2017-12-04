using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWalk : StateMachineBehaviour {

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow)) animator.SetBool("Walk", false);
    }
}
