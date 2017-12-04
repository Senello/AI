using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldController : MonoBehaviour
{
    private Animator anim;
    private AudioSource aud;
    private int shakeHash = Animator.StringToHash("Shake");


    void Start()
    {
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameController.Instace.GetShake())
        {
            anim.SetTrigger(shakeHash);
            aud.Play();
        }

        GameController.Instace.SetShake(false);
    }
}