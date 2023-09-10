using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    // Prefabs
    public GameObject[] blubs;
    public GameObject obstacle;

    public GameObject backgroundsParent;
    public GameObject[] backgrounds;

    // Menus
    public GameObject mainMenu;
    public GameObject playMenu;
    public GameObject playCanvas;
    public GameObject gameOverMenu;

    // Text
    public TMP_Text scoreText;
    public TMP_Text gameOverText;
    public TMP_Text highScoreText;

    // Instantiate
    private GameObject player;
    private GameObject obstacle1;
    private GameObject obstacle2;
    private GameObject background;

    // Game Stats
    protected int score;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;

        background = Instantiate(backgrounds[UnityEngine.Random.Range(0, 3)], new Vector2(0, 0), Quaternion.Euler(90f, 180f, 0f));
        background.transform.parent = backgroundsParent.transform;

        // Set up UI
        mainMenu.SetActive(true);
        playMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playMenu.activeSelf)
        {
            PlayGame();
        }
    }

    // Play Controller
    void Spawn()
    {
        GameObject blub = blubs[UnityEngine.Random.Range(0, 3)];

        player = Instantiate(
            blub,
            new Vector3(
                0f,
                -3.25f,
                -1f
            ),
            Quaternion.identity
        );


        obstacle1 = Instantiate(
            obstacle,
            new Vector3(
                UnityEngine.Random.Range(-1.9f, 1.9f),
                5 + (obstacle.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y / 2f),
                -1
            ),
            Quaternion.identity
        );
        
        obstacle2 = Instantiate(
            obstacle,
            new Vector3 (
                UnityEngine.Random.Range(-1.9f, 1.9f),
                obstacle1.transform.position.y * 2f,
                -1
            ),
            Quaternion.identity
        );

        player.transform.parent = playMenu.transform;
        obstacle1.transform.parent = playMenu.transform;
        obstacle2.transform.parent = playMenu.transform;
    }

    void PlayGame()
    {
        if (obstacle1.transform.position.y < (-5 - (obstacle.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y / 2f)))
        {
            obstacle1.transform.position = new Vector3(
                UnityEngine.Random.Range(-1.9f, 1.9f),
                5 + (obstacle.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y / 2f),
                -1
            );
        }

        if (obstacle2.transform.position.y < (-5 - (obstacle.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y / 2f)))
        {
            obstacle2.transform.position = new Vector3(
                UnityEngine.Random.Range(-1.9f, 1.9f),
                5 + (obstacle.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y / 2f),
                -1
            );
        }

        if (player.transform.position.x >= 3.05f)
        {
            player.transform.position = new Vector3(
                -3.05f,
                player.transform.position.y,
                player.transform.position.z
            );
        } else if (player.transform.position.x <= -3.05f)
        {
            player.transform.position = new Vector3(
                3.05f,
                player.transform.position.y,
                player.transform.position.z
            );
        }
    }

    // UI
    public void Play()
    {
        AudioManager.Instance.playSoundEffect("Click");

        score = 0;
        scoreText.text = score.ToString();

        mainMenu.SetActive(false);
        playMenu.SetActive(true);
        playCanvas.SetActive(true);
        gameOverMenu.SetActive(false);

        Spawn();
    }

    public void Restart()
    {
        AudioManager.Instance.playSoundEffect("Click");

        Destroy(player);
        Destroy(obstacle1);
        Destroy(obstacle2);
        Destroy(background);

        background = Instantiate(backgrounds[UnityEngine.Random.Range(0, 3)], new Vector2(0, 0), Quaternion.Euler(90f, 180f, 0f));
        background.transform.parent = backgroundsParent.transform;

        mainMenu.SetActive(true);
        playMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    public void Score()
    {
        AudioManager.Instance.playSoundEffect("Score");

        score++;
        scoreText.text = score.ToString();

        if(PlayerPrefs.GetInt("HighScore", 0) < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void GameOver()
    {
        AudioManager.Instance.playSoundEffect("Hit");

        player.GetComponent<PlayerController>().speed = 0;
        obstacle1.GetComponent<ObstacleController>().speed = 0;
        obstacle2.GetComponent<ObstacleController>().speed = 0;

        player.GetComponent<SpriteRenderer>().sprite = player.GetComponent<PlayerController>().sadBlub;

        gameOverText.text = "Score:\n\n" + score;
        highScoreText.text = "Best:\n\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();

        mainMenu.SetActive(false);
        playCanvas.SetActive(false);
        gameOverMenu.SetActive(true);
    }
}
