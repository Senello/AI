using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
    private Transform[] allChildren;
    private float nextActionTime;
    private float period = 0.15f;
    public bool Movable = true;

    void Start()
    {
        if (Movable)
        {
            allChildren = GetComponentsInChildren<Transform>();
            InvokeRepeating("Move", 0, 0.7f);
        }
    }

    void Update()
    {
        if (Movable)
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.DownArrow))
            {
                if (Time.time > nextActionTime)
                {
                    nextActionTime += period;
                    Move();
                }
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.Rotate(Vector3.back, 90);
                Check(0);
                Fade();
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                bool canMove = true;
                foreach (Transform child in allChildren)
                {
                    if (child.position.x <= 0.5)
                    {
                        canMove = false;
                        break;
                    }
                    if (Convert.ToInt32(child.position.y) < 20 &&
                        GameController.Instace.Board[Convert.ToInt32(child.position.x - 1),
                            Convert.ToInt32(child.position.y)])
                    {
                        canMove = false;
                        break;
                    }
                }
                if (canMove)
                    transform.position =
                        new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                bool canMove = true;
                foreach (Transform child in allChildren)
                {
                    if (child.position.x >= 8.5)
                    {
                        canMove = false;
                        break;
                    }
                    if (Convert.ToInt32(child.position.y) < 20 &&
                        GameController.Instace.Board[Convert.ToInt32(child.position.x + 1),
                            Convert.ToInt32(child.position.y)])
                    {
                        canMove = false;
                        break;
                    }
                }
                if (canMove)
                    transform.position =
                        new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            }
        }
    }

    void Move()
    {
        if (Movable)
        {
            Check(1);
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            Fade();
        }
    }

    void Kys() // Kill yourself
    {
        foreach (Transform child in allChildren)
        {
            if (child.position.y < 19.5)
                GameController.Instace.Board[Convert.ToInt32(child.position.x), Convert.ToInt32(child.position.y)] =
                    true;
            else
            {
                GameController.Instace.GameOver();
                break;
            }
        }

        transform.DetachChildren();
        GameController.Instace.SetNextBlock(true);
        Destroy(gameObject);
    }

    void Fade() // Fade over field
    {
        foreach (Transform child in allChildren)
        {
            if (Convert.ToInt32(child.position.y) >= 20) child.gameObject.layer = 8;
            else child.gameObject.layer = 0;
        }
    }

    void Check(int below)
    {
        foreach (Transform child in allChildren)
        {
            if (Convert.ToInt32(child.position.x) < 0 || Convert.ToInt32(child.position.x) > 9)
            {
                transform.Rotate(Vector3.back, -90);
                break;
            }
            if (Convert.ToInt32(child.position.y) - below < 0)
            {
                if (below == 0)
                {
                    transform.Rotate(Vector3.back, -90);
                    break;
                }
                if (below == 1) Kys();
            }
            /*else
                for (int i = 0; i < 20; i++)
                    if (GameController.Instace.Board[Convert.ToInt32(child.position.x), i])
                        if (Convert.ToInt32(child.position.y) - below <= i)
                        {
                            if (below == 0) transform.Rotate(Vector3.back, -90);
                            if (below == 1) Kys();
                        }*/
            else if (Convert.ToInt32(child.position.y) - below >= 0 && Convert.ToInt32(child.position.y) - below < 20 &&
                     GameController.Instace.Board[Convert.ToInt32(child.position.x),
                         Convert.ToInt32(child.position.y) - below])
            {
                if (below == 0) transform.Rotate(Vector3.back, -90);
                if (below == 1) Kys();
            }
        }
    }
}