using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float smoothTime = 0.3F;
    public Boundary boundary;
    
    private Transform target;
    private Vector3 velocity = Vector3.zero;

    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }
    void Start()
    {
        target = GetComponent<Transform>();
    }


    void Update()
    {
        float xSpeed = 0;
        float ySpeed = 0;
        float zSpeed = 0;
        if (Input.GetKey(KeyCode.W)) zSpeed = playerSpeed;
        if (Input.GetKey(KeyCode.S)) zSpeed = -playerSpeed;
        if (Input.GetKey(KeyCode.A)) xSpeed = -playerSpeed;
        if (Input.GetKey(KeyCode.D)) xSpeed = playerSpeed;
        Vector3 targetPosition = target.TransformPoint(new Vector3(xSpeed, ySpeed, zSpeed));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
        );
    }
}