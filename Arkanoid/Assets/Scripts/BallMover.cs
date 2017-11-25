using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoundaryBall
{
    public float xMin, xMax, yMin, yMax, zConst;
}


public class BallMover : MonoBehaviour {

    private Rigidbody rb;
    public BoundaryBall boundary;
    public float ballSpeed;
    private GameController gameController;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
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

    void Update()
    {
        if ((Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space)) && !gameController.getStarted())
        {
            gameController.setStarted(true);
            rb.AddForce(0.0f, 300.0f, 0.0f);
        }
        if (transform.position.y <= boundary.yMin+0.1f) Destroy(gameObject);

        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax),
            boundary.zConst
        );
    }

    void LateUpdate()
    {
        rb.velocity = ballSpeed * (rb.velocity.normalized);
    }
}
