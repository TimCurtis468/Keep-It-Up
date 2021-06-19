using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance => _instance;


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public GameObject gameOverScreen;
    public GameObject victoryScreen;

    public int AvailableLives = 3;

    public int Lives { get; set; }

    public bool IsGameStarted { get; set; }

    public static event Action<int> OnLifeLost;

    public GameObject LeftWall;
    public GameObject RightWall;
    private Vector2 screenBounds;
    private Camera mainCamera;

    private void Start()
    {
        float objectWidth;

        this.Lives = AvailableLives;
        Ball.OnBallDeath += OnBallDeath;

        /* Set position of walls to match screen resolution */
        mainCamera = FindObjectOfType<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        objectWidth = RightWall.GetComponent<SpriteRenderer>().bounds.extents.x;
        RightWall.transform.position = new Vector3(screenBounds.x + (objectWidth / 2), RightWall.transform.position.y, RightWall.transform.position.z);

        objectWidth = LeftWall.GetComponent<SpriteRenderer>().bounds.extents.x;
        LeftWall.transform.position = new Vector3(-(screenBounds.x + (objectWidth / 2)), LeftWall.transform.position.y, LeftWall.transform.position.z);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        this.Lives = AvailableLives;
    }

    private void OnBallDeath(Ball obj)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {
            this.Lives--;

            if (this.Lives < 1)
            {
                gameOverScreen.SetActive(true);
            }
            else
            {
                OnLifeLost?.Invoke(this.Lives);
                BallsManager.Instance.ResetBalls();
                IsGameStarted = false;
                BlocksManager.Instance.NewLevel();
            }
        }
    }

    public void ShowVictoryScreen()
    {
        this.victoryScreen.SetActive(true);
    }

    private void OnDisable()
    {
        {
            Ball.OnBallDeath -= OnBallDeath;
        }
    }
}
