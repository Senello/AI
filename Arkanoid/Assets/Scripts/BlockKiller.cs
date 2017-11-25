using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockKiller : MonoBehaviour
{
    private int HP;
    public int points;
    public GameObject particles;
    public Material[] materials;
    public GameObject boostScale;
    public bool haveBoost = false;
    private Renderer rend;

    private GameController gameController;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        HP = materials.Length+1;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.rigidbody.tag == "ball")
        {
            HP -= 1;
            gameController.AddScore(10);
            if (HP == 0)
            {
                if (haveBoost)
                {
                    boostScale.transform.position = transform.position;
                    Instantiate(boostScale);
                }
                particles.transform.position = transform.position;
                Instantiate(particles);
                Destroy(gameObject);
                gameController.AddScore(points);
            }
            else
            {
                rend.sharedMaterial = materials[HP-1];
            }
        }
    }
}
