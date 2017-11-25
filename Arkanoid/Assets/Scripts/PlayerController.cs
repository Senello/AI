using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BoundaryPlayer
{
    public float xMin, xMax,yConst,zConst;
}


public class PlayerController : MonoBehaviour
{
    public float playerSpeed;

    public BoundaryPlayer boundary;
    private Vector3 playerPos;
    private GameController gameController;

    void Start ()
    {
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
    private void Update()
    {
        if(gameController.getStarted())
        {
            /*float xPos = transform.position.x;
            if (Input.GetKey(KeyCode.A)) xPos -= playerSpeed * 0.1F;
            else if (Input.GetKey(KeyCode.D)) xPos += playerSpeed * 0.1F;*/
            float movement = Input.GetAxis("Horizontal");
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) movement = 0.0f;
            float xPos = transform.position.x + (movement * playerSpeed);
            float playerXscale = transform.localScale.x / 2 + 0.1f;
            playerPos = new Vector3(Mathf.Clamp(xPos, boundary.xMin + playerXscale, boundary.xMax - playerXscale), boundary.yConst, boundary.zConst);
            transform.position = playerPos;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        foreach (ContactPoint contact in col.contacts)
            if (contact.thisCollider == GetComponent<Collider>())
            {
                float english = contact.point.x - transform.position.x;

                col.rigidbody.AddForce(400.0f * english, 0.0f, 0.0f);
            }
    }
}
