using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instace;
    public bool[,] Board = new bool[10, 20];
    public GameObject[] Blocks;
    public GameObject CubeDestroyParticle;
    public CubeCollector collector;
    public Text GameOverText;
    public Text TimeText;
    public Text ScoreText;
    public Text InfoText;
    private float startTime;
    private bool nextBlock;
    private bool pause;
    private bool fullLine;
    private int score;
    private int combo;
    private int randomBlock;
    private bool shake;

    void Start()
    {
        Instace = FindObjectOfType<GameController>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                Board[i, j] = false;
            }
        }
        GameOverText.text = "";
        TimeText.text = "";
        ScoreText.text = "Score: " + 0;
        InfoText.text = "";
        startTime = Time.time;
        nextBlock = true;
        pause = false;
        fullLine = false;
        shake = false;
        score = 0;
        combo = 0;
        randomBlock = Random.Range(0, Blocks.Length);
    }

    void Update()
    {
        if (!pause)
        {
            float t = Time.time - startTime;
            int m = (int) t / 60;
            int s = (int) t % 60;
            string zero = "";
            if (s < 10) zero = "0";
            TimeText.text = m + ":" + zero + s;
        }

        if (fullLine)
        {
            while (fullLine)
            {
                combo++;
                fullLine = false;
                for (int i = 0; i < 20; i++)
                {
                    int count = 0;
                    for (int j = 0; j < 10; j++)
                    {
                        if (Board[j, i] == false) break;
                        count++;
                        if (count == 10)
                        {
                            score += 100 * combo;
                            fullLine = true;
                            for (int k = 0; k < 10; k++)
                            {
                                Instantiate(CubeDestroyParticle, new Vector3(k, i, -1),
                                    CubeDestroyParticle.transform.rotation);
                                for (int l = i; l < 19; l++)
                                {
                                    Board[k, l] = Board[k, l + 1];
                                }
                            }
                            for (int k = 0; k < 10; k++)
                            {
                                Board[k, 19] = false;
                            }
                            collector.KillChildrenWithPositionY(i);
                        }
                    }
                }
            }
            combo = 0;
            ScoreText.text = "Score: " + score;
        }


        if (pause)
        {
            InfoText.text = "Press R for restart";
            if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (nextBlock && !pause)
        {
            GameObject toDestroy = GameObject.FindGameObjectWithTag("NextBlock");
            if (toDestroy != null) Destroy(toDestroy);
            Instantiate(Blocks[randomBlock]);
            randomBlock = Random.Range(0, Blocks.Length);
            GameObject nextBlockObject = Instantiate(Blocks[randomBlock]);
            if (randomBlock != 4) nextBlockObject.transform.position = new Vector3(-6.5f, 6.5f, 0f);
            else nextBlockObject.transform.position = new Vector3(-6.0f, 7.0f, 0f);
            nextBlockObject.GetComponent<BlockMover>().Movable = false;
            nextBlockObject.tag = "NextBlock";
            foreach (Transform child in nextBlockObject.GetComponentsInChildren<Transform>())
            {
                child.tag = "NextBlock";
            }
            nextBlock = false;
        }
    }

    public void SetNextBlock(bool var)
    {
        nextBlock = var;
        fullLine = true;
        shake = true;
    }

    public bool GetNextBlock()
    {
        return nextBlock;
    }

    public bool GetFullLine()
    {
        return fullLine;
    }

    public bool GetShake()
    {
        return shake;
    }

    public void SetShake(bool var)
    {
        shake = var;
    }

    public void GameOver()
    {
        GameOverText.text = "Game Over";
        pause = true;
    }
}