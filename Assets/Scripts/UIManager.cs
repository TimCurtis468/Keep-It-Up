using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;
    public Text LivesText;

    public int Score { get; set; }

    private void Awake()
    {
        GameManager.OnLifeLost += OnLifeLost;
        Paddle.OnPaddleHit += OnPaddleHit;
        UpdateScoreText(0);

    }

    private void Start()
    {
        OnLifeLost(GameManager.Instance.AvailableLives);
    }

    private void OnLifeLost(int remainingLives)
    {
        string txt = "LIVES: " + remainingLives.ToString();
        LivesText.text = txt;
    }

    private void UpdateScoreText(int increment)
    {
        this.Score += increment;
        string scoreString = this.Score.ToString().PadLeft(5, '0');
        ScoreText.text = "SCORE: " + scoreString;
    }

    private void OnPaddleHit(Paddle obj)
    {
        UpdateScoreText(1);
    }

    private void OnDisable()
    {
        GameManager.OnLifeLost -= OnLifeLost;
        Paddle.OnPaddleHit -= OnPaddleHit;
    }

}
