using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
    public bool Movable = true;
    private Transform[] allChildren;
    private float delay = 1.0f;
    private float initializationTime;
    private float lastMove;
    private float horizontalDelay = 0.15f;
    private bool canRotate;
    private string n;
    private AudioSource aud;

    void Start()
    {
        if (Movable)
        {
            n = transform.name;
            lastMove = Time.time;
            canRotate = true;
            aud = GetComponent<AudioSource>();
            initializationTime = Time.timeSinceLevelLoad;
            allChildren = GetComponentsInChildren<Transform>();
            InvokeRepeating("Move", 0, 0.7f - 0.1f * Time.timeSinceLevelLoad/60);
        }
    }

    void Update()
    {
        if (Movable)
        {
            float timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.DownArrow))
            {
                if (timeSinceInitialization > delay)
                {
                    Move();
                }
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (SpecificRotation(270)) transform.Rotate(Vector3.back, -90);
                else transform.Rotate(Vector3.back, 90);
                canRotate = true;
                Check(0);
                Fade();
                if (canRotate) aud.Play();
            }
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && Time.time > lastMove + horizontalDelay)
            {
                lastMove = Time.time;
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
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && Time.time > lastMove + horizontalDelay)
            {
                lastMove = Time.time;
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
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.DownArrow))
        {
            GameController.Instace.SetShake(true);
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
                canRotate = false;
                if (SpecificRotation(0)) transform.Rotate(Vector3.back, 90);
                else transform.Rotate(Vector3.back, -90);
                break;
            }
            if (Convert.ToInt32(child.position.y) - below < 0)
            {
                if (below == 0)
                {
                    canRotate = false;
                    if (SpecificRotation(0)) transform.Rotate(Vector3.back, 90);
                    else transform.Rotate(Vector3.back, -90);
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
                canRotate = false;
                if (below == 0)
                    if (SpecificRotation(0)) transform.Rotate(Vector3.back, 90);
                    else transform.Rotate(Vector3.back, -90);
                if (below == 1) Kys();
            }
        }
    }

    bool SpecificRotation(int rot)
    {
        return (n == "Block I(Clone)" || n == "Block Z(Clone)" || n == "Block -Z(Clone)" ||
                n == "Block I" || n == "Block Z" || n == "Block -Z") && (int) transform.rotation.eulerAngles.z == rot;
    }
}