using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMover : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
    }
}
