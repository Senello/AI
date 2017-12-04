using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldController : MonoBehaviour
{
    private Animator anim;
    private int shakeHash = Animator.StringToHash("Shake");

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(GameController.Instace.GetShake())anim.SetTrigger(shakeHash);
        GameController.Instace.SetShake(false);
    }
}