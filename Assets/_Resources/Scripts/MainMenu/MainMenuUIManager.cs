using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Button sixtySecondButton;
    [SerializeField] private Button threeBulletButton;
    [SerializeField] private Button exitGameButton;

    private void Start()
    {
        SetButtonsListener();
    }

    private void SetButtonsListener()
    {
        sixtySecondButton.onClick.AddListener(() =>
        {
            ModManager.Instance.SelectSixtySecondsMod();
            SceneManager.LoadScene(1);
        });

        threeBulletButton.onClick.AddListener(() =>
        {
            ModManager.Instance.SelectThreeBulletsMod();
            SceneManager.LoadScene(1);
        });

        exitGameButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}