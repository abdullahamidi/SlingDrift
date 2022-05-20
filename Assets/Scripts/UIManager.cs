using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI gameOverScoreText;
    private int score;
    private void Awake()
    {
        score = 0;
        gameOverPanel.SetActive(false);
    }
    private void OnEnable()
    {
        PlayerExitDriftArea.OnScore += UpdateScore;
        CrashDetection.OnCrash += ShowGameOverPanel;
    }

    private void OnDisable()
    {
        PlayerExitDriftArea.OnScore -= UpdateScore;
        CrashDetection.OnCrash -= ShowGameOverPanel;
    }
    private void ShowGameOverPanel()
    {
        gameOverScoreText.text = score.ToString();
        gameOverPanel.SetActive(true);
    }

    private void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
    public void Replay()
    {
        HideGameOverPanel();
        SceneManager.LoadScene(0);
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
