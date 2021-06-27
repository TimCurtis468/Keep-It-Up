﻿using System;
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
    public Heart heartPrefab;
    public Drumstick drumstickPrefab;

    private Ball initialBall;

    private Rigidbody2D initialBallRb; 

    private Camera mainCamera;
    private Vector2 screenBounds;


    public float initialBallSpeed = 250;

    public int numBalls = 3;

    public List<Ball> Balls { get; set; }
    public List<Rigidbody2D> BallRbs { get; set; }
    public List<Rigidbody2D> DrumstickRbs { get; set; }

    public List<Heart> Hearts { get; set; }
    public List<Drumstick> Drumsticks { get; set; }


    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        Balls = new List<Ball>();
        BallRbs = new List<Rigidbody2D>();
        Hearts = new List<Heart>();
        Drumsticks = new List<Drumstick>();
        DrumstickRbs = new List<Rigidbody2D>();

        InitBall();
    }

    private void Update()
    {
        Rigidbody2D heartRb;
        if (!GameManager.Instance.IsGameStarted)
        {
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
                }

                heartRb = Hearts[0].GetComponent<Rigidbody2D>();
                heartRb.isKinematic = false;
                heartRb.AddForce(new Vector2(200, 700));

                for (int i = 0; i < Drumsticks.Count; i++)
                {

                    float x_speed = UnityEngine.Random.Range(50.0f, 100.0f);
                    float y_speed = UnityEngine.Random.Range(250.0f, 450.0f);
                    int direction = UnityEngine.Random.Range(1, 3);
                    if (direction == 1)
                    {
                        x_speed = -x_speed;
                    }

                    DrumstickRbs[i].isKinematic = false;
                    DrumstickRbs[i].AddForce(new Vector2(x_speed, y_speed));
                }


                GameManager.Instance.IsGameStarted = true;
            }
        }
    }

    public void DestroyBalls()
    {
        foreach (var ball in this.Balls.ToList())
        {
            Destroy(ball.gameObject);
        }

        foreach (var heart in this.Hearts.ToList())
        {
            Destroy(heart.gameObject);
        }


        foreach (var drumstick in this.Drumsticks.ToList())
        {
            Destroy(drumstick.gameObject);
        }
    }


    public void ResetBalls()
    {
        foreach (var ball in this.Balls.ToList())
        {
            Destroy(ball.gameObject);
        }

        foreach (var heart in this.Hearts.ToList())
        {
            Destroy(heart.gameObject);
        }


        foreach (var drumstick in this.Drumsticks.ToList())
        {
            Destroy(drumstick.gameObject);
        }

        InitBall();
    }

    private void InitBall()
    {
        Ball newBall;
        Rigidbody2D newBallRb;
        Heart newHeart;
        Drumstick newDrumstick;
        Rigidbody2D newDrumstickRb;

        int numDrumsticks = UnityEngine.Random.Range(0, 2);

        Balls.Clear();
        BallRbs.Clear();
        Hearts.Clear();
        Drumsticks.Clear();
        DrumstickRbs.Clear();



        for (int i = 0; i < numBalls; i++)
        {
            Vector3 startingPosition = new Vector3(i, -screenBounds.y / 2, 0);
            newBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
            newBallRb = newBall.GetComponent<Rigidbody2D>();

            Balls.Add(newBall);
            BallRbs.Add(newBallRb);
        }

        Vector3 heartPosition = new Vector3(0.5f, -screenBounds.y / 2, 0);
        newHeart = Instantiate(heartPrefab, heartPosition, Quaternion.identity);
        Hearts.Add(newHeart);

        for (int i = 0; i < numDrumsticks; i++)
        {
            Vector3 startingPosition = new Vector3(i + 0.2f, (-screenBounds.y / 2) + 0.2f, 0);
            newDrumstick = Instantiate(drumstickPrefab, startingPosition, Quaternion.identity);
            newDrumstickRb = newDrumstick.GetComponent<Rigidbody2D>();

            Drumsticks.Add(newDrumstick);
            DrumstickRbs.Add(newDrumstickRb);
        }

    }
}
