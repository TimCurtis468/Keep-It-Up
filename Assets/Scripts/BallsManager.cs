using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    #region Singleton
    private static BallsManager _instance;

    public static BallsManager Instance => _instance;

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
    [SerializeField]
    private Ball ballPrefab;

    private Ball initialBall;

    private Rigidbody2D initialBallRb;

    private Camera mainCamera;
    private Vector2 screenBounds;


    public float initialBallSpeed = 250;

    public int numBalls = 3;

    public List<Ball> Balls { get; set; }
    public List<Rigidbody2D> BallRbs { get; set; }

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        InitBall();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            //            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            //            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + 0.29f, 0);
            //            initialBall.transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < Balls.Count; i++)
                {

                    float x_speed = UnityEngine.Random.Range(50.0f, 100.0f);
                    float y_speed = UnityEngine.Random.Range(250.0f, 450.0f);
                    int direction = UnityEngine.Random.Range(1, 3);
                    if (direction == 1)
                    {
                        x_speed = -x_speed;
                    }

                    BallRbs[i].isKinematic = false;
                    BallRbs[i].AddForce(new Vector2(x_speed, y_speed));
                    GameManager.Instance.IsGameStarted = true;
                }
            }
        }
    }

    public void ResetBalls()
    {
        foreach (var ball in this.Balls.ToList())
        {
            Destroy(ball.gameObject);
        }

        InitBall();
    }

    private void InitBall()
    {
        Ball newBall;
        Rigidbody2D newBallRb;

        Balls = new List<Ball>();
        BallRbs = new List<Rigidbody2D>();
        //       Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
        //       Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + 0.29f, 0);
        for (int i = 0; i < numBalls; i++)
        {
            Vector3 startingPosition = new Vector3(i, -screenBounds.y / 2, 0);
            newBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
            newBallRb = newBall.GetComponent<Rigidbody2D>();

            Balls.Add(newBall);
            BallRbs.Add(newBallRb);
        }
    }


    public void SpawnBalls(Vector3 position, int count, bool isLightningBall)
    {
        for (int i = 0; i < count; i++)
        {
            Ball spawnedBall = Instantiate(ballPrefab, position, Quaternion.identity);
            if (isLightningBall == true)
            {
 //               spawnedBall.StartLightningBall();
            }
            Rigidbody2D spawnedBallRb = spawnedBall.GetComponent<Rigidbody2D>();
            spawnedBallRb.isKinematic = false;
            spawnedBallRb.AddForce(new Vector2(i, initialBallSpeed));
            this.Balls.Add(spawnedBall);
        }
    }
}
