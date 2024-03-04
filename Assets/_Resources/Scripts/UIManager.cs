using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>, ISelectedMod
{
    public bool isPlay;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("EndPanel")]
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TextMeshProUGUI maxScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    //Mods
    [SerializeField] private Texture2D crosshair;
    [SerializeField] private Image crosshairReload;
    [SerializeField] private GameObject bulletsPanel;
    [SerializeField] private List<GameObject> bullets = new List<GameObject>();

    [Header("Health System")]
    [SerializeField] private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        StartingGame();
    }

    public void StartingGame()
    {
        bulletsPanel.SetActive(false);
        timerPanel.SetActive(false);

        switch (GameMods.activeMod)
        {
            case GameMods.Mods.threeBullets:
                SelectThreeBulletsMod();
                SetSixtySecondsModButtons();

                break;
            case GameMods.Mods.sixtySeconds:
                SelectSixtySecondsMod();
                break;
            default:
                break;
        }
        DisableEndPanel();
        isPlay = true;
    }

    public void SelectSixtySecondsMod()
    {
        timerPanel.SetActive(true);
        SetSixtySecondsModButtons();
    }

    public void SelectThreeBulletsMod()
    {
        foreach (var heart in hearts)
        {
            heart.SetActive(true);
        }
        bulletsPanel.SetActive(true);
    }

    private void SetSixtySecondsModButtons()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    #region Three Bullets Mod
    public void AddHearth()
    {
        foreach (var heart in hearts)
        {
            if (!heart.activeSelf)
            {
                heart.SetActive(true);
                break;
            }
        }
    }

    public void TakeDamage()
    {
        foreach (var heart in hearts)
        {
            if (heart.activeSelf)
            {
                heart.SetActive(false);
                break;
            }
        }
    }

    int count = 0;
    public void ReloadingRifle()
    {
        foreach (var bullet in bullets)
        {
            if (!bullet.activeSelf)
            {
                count++;
                SoundManager.Instance.ReloadingRifle();
                bullet.SetActive(true);
                break;
            }
        }

        if (count >= bullets.Count)
        {
            count = 0;
            SoundManager.Instance.ReloadedRifle();
        }
    }
    public void DeactiveBullets()
    {
        foreach (var bullet in bullets)
        {
            bullet.SetActive(false);
        }
    }

    public void DisableNextActiveBullet()
    {
        foreach (var bullet in bullets)
        {
            if (bullet.activeSelf)
            {
                bullet.SetActive(false);
                break;
            }
        }
    }
    #endregion

    public void ActivateEndPanel()
    {
        UICrosshair.Instance.EndGame();
        isPlay = false;
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