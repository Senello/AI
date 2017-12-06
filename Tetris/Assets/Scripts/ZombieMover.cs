using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMover : MonoBehaviour
{
    public float speed;
    private SpriteRenderer sr;
    private Vector3 target;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            sr.flipX = true;
            target = transform.position + Vector3.left * speed;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            sr.flipX = false;
            target = transform.position + Vector3.right * speed;
        }
        else target = transform.position;


        transform.position = new Vector3(Mathf.Clamp(target.x, 11, 22), target.y, target.z);
    }
}