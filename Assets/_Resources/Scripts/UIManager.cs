using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("EndPanel")]
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TextMeshProUGUI maxScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    void Start()
    {
        DisableEndPanel();
        SetButtons();
    }

    private void SetButtons()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
    public void ActivateEndPanel()
    {
        SetMaxScoreText();
        SetCurrentScore();
        endGamePanel.SetActive(true);
    }

    public void DisableEndPanel()
    {
        endGamePanel.SetActive(false);
    }

    private void SetMaxScoreText()
    {
        maxScoreText.text = SaveAndLoad.Instance.GetMaxScore().ToString();
    }
    private void SetCurrentScore()
    {
        currentScoreText.text = scoreText.text;
    }

    public void SetScore(int scoreAmount)
    {
        scoreText.text = scoreAmount.ToString();
    }

    public void SetTimer(float currentTime)
    {
        timerText.text = currentTime.ToString();
    }
}