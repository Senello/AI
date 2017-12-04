using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdle : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetTrigger("Attack");
        }
        if (GameController.Instace.GameOverText.text == "Game Over")
        {
            animator.SetTrigger("Dead");
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("Walk", true);
        }
    }
}