﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;
    public Text LivesText;

    public Sprite[] backgrounds;

    private SpriteRenderer spriteRenderer;

    public GameObject background_obj;

    public int Score { get; set; }

    private void Awake()
    {
        GameManager.OnLifeLost += OnLifeLost;
        GameManager.OnLifeGained += OnLifeGained;
        Paddle.OnPaddleHit += OnPaddleHit;
        Block.OnBlockHit += OnBlockHit;
        UpdateScoreText(0);

    }

    private void Start()
    {
        spriteRenderer = background_obj.GetComponent<SpriteRenderer>();

        OnLifeLost(GameManager.Instance.AvailableLives);
    }

    private void OnLifeLost(int remainingLives)
    {
        int background_num = 0;
        string txt = "LIVES: " + remainingLives.ToString();
        LivesText.text = txt;

        if (backgrounds.Length > 0)
        {
            background_num = UnityEngine.Random.Range(0, backgrounds.Length);
            spriteRenderer.sprite = backgrounds[background_num];
        }
    }

    private void OnLifeGained(int remainingLives)
    {
        string txt = "LIVES: " + remainingLives.ToString();
        LivesText.text = txt;
    }

    private void UpdateScoreText(int increment)
    {
        this.Score += increment;
        string scoreString = this.Score.ToString().PadLeft(5, '0');
        ScoreText.text = "SCORE: " + scoreString;
        GameManager.Instance.SetScore(Score);
    }

    private void OnPaddleHit(Paddle obj, int speed)
    {
        UpdateScoreText(speed);
    }

    private void OnBlockHit()
    {
        UpdateScoreText(1);
    }

    private void OnDisable()
    {
        GameManager.OnLifeLost -= OnLifeLost;
        Paddle.OnPaddleHit -= OnPaddleHit;
        GameManager.OnLifeGained -= OnLifeGained;
        Block.OnBlockHit-= OnBlockHit;
    }

}
