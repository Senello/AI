using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{


    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    public GameObject player;
    public GameObject ball;
    public GameObject playerLifeObject;

    private int playerLife;
    private bool gameOver;
    private bool restart;
    private int score;
    private bool started;

    void Start ()
    {
        Application.targetFrameRate = 50;
        playerLife = 3;
        gameOver = false;
        restart = false;
        started = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        makeLifes();
    }

    private void Update()
    {
        if (gameOver)
        {
            restartText.text = "Press 'R' for Restart";
            restart = true;
        }
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                started = false;
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    private void LateUpdate()
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("brick");
        int brickCount = bricks.Length;
        if (started && brickCount == 0)
        {
            YouWon();
        }
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
        int ballCount = balls.Length;
        if (started && ballCount == 0)
        {
            playerLife--;
            if (playerLife == 0)
            {
                destroyLife();
                GameOver();
            }
        else if (playerLife > 0)
        {
            destroyLife();
            started = false;
            player.transform.position = new Vector3(0.0f, -7.0f, 9.5f);
            player.transform.localScale = new Vector3(1.5f, -0.15f, 1.0f);
                Instantiate(ball);
        }
        else
        {
            playerLife = 0;
        }
    }
    }

    public bool getStarted()
    {
        return started;
    }

    public void setStarted(bool v)
    {
        started = v;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    private void GameOver()
    {
        if (!gameOver)
        {
            gameOverText.text = "Game Over!";
            gameOver = true;
        }
}
    private void YouWon()
    {
        if (!gameOver)
        {
            gameOverText.text = "You Won!";
            gameOver = true;
        }
    }
    private void makeLifes()
    {
        for (int i = 0; i < playerLife; i++)
        {
            Instantiate(playerLifeObject);
            playerLifeObject.transform.position = new Vector3(-4.2f + 0.8f * i, -7.3f, 9.5f);
        }
    }

    private void destroyLife()
    {
        DestroyObject(GameObject.FindWithTag("PlayerLife"));
    }
}
