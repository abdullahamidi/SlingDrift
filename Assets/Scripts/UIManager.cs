using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
    }
    private void OnEnable()
    {
        CrashDetection.OnCrash += ShowGameOverPanel;
    }

    private void OnDisable()
    {
        CrashDetection.OnCrash -= ShowGameOverPanel;
    }
    private void ShowGameOverPanel()
    {
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
}
