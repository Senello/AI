using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostScript : MonoBehaviour
{

    private Rigidbody rb;
    public float boostSpeed;
    public string boostType;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0.0f, -boostSpeed, 0.0f);
        DestroyObject(gameObject,8.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (boostType == "Scale" && other.tag == "Player")
        {
            other.transform.localScale = new Vector3(1.2f * other.transform.localScale.x, other.transform.localScale.y, other.transform.localScale.z);
            DestroyObject(gameObject);
        }
    }
}
