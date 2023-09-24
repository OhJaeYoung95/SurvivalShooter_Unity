using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;

    public TextMeshProUGUI gameoverUI;

    public GameObject pauseUI;

    public void ScoreUpdateUI(int score)
    {
        scoreText.text = $"SCORE : {score}";
    }

    public void AmmoUpdateUI(int magAmmo, int remainAmmo)
    {
        ammoText.text = $"AMMO : {magAmmo} / {remainAmmo}";
    }

    public void GameOverUISetActive(bool isActive)
    {
        gameoverUI.gameObject.SetActive(isActive);
    }

    public void PauseUISetActive(bool isActive) 
    {
        pauseUI.SetActive(isActive);
    }
}
