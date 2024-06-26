using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoSingleton<SaveAndLoad>
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetMaxScore(int score)
    {
        PlayerPrefs.SetInt("MAX_SCORE", score);
    }
    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("MAX_SCORE");
    }

}