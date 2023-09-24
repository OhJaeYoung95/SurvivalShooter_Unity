using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score = 0;
    public bool IsGameOver { get; private set; }
    public bool IsPause { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        IsGameOver = false;
        IsPause = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PressedPauseButton();
        }

    }

    public void PressedPauseButton()
    {
        IsPause = !IsPause;
        UIManager.Instance.PauseUISetActive(IsPause);
    }

    public void AddScore(int addScore)
    {
        if(!IsGameOver)
        {
            score += addScore;
            UIManager.Instance.ScoreUpdateUI(score);
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        UIManager.Instance.GameOverUISetActive(IsGameOver);
    }

    public void RestartLevel()
    {
        IsGameOver = false;
        IsPause = false;
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
