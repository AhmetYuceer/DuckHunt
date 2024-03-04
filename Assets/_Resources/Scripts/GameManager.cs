using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>, ISelectedMod
{
    public bool isPlay { get; set; }

    [SerializeField] private List<GameObject> ducks = new List<GameObject>();
    [SerializeField] private int maxActivateDucksCount;
    [SerializeField] private float defaultDucksSpeed = 2f;
    [SerializeField] private float speedIncreaseAmount = 0f;
    [SerializeField] private float timerAmount;
    private Duck duck;
    private int score;
    private float time;

    //Mods  
    public bool threeBulletMod, sixtySecondsMod;

    void Start()
    {

        StartingGame();
        SpeedUpDucks();
    }

    void Update()
    {
        if (sixtySecondsMod)
        {
            UpdateTimer();
        }
    }

    public void SpeedUpDucks()
    {
        defaultDucksSpeed += speedIncreaseAmount;
        foreach (var item in ducks)
        {
            duck = item.GetComponent<Duck>();
            duck.SetSpeed(defaultDucksSpeed);
        }
    }

    public void StartingGame()
    {
        sixtySecondsMod = false;
        threeBulletMod = false;
        switch (GameMods.activeMod)
        {
            case GameMods.Mods.threeBullets:
                SelectThreeBulletsMod();
                break;
            case GameMods.Mods.sixtySeconds:
                SelectSixtySecondsMod();
                break;
            default: break;
        }
        isPlay = true;
        ActivateDucks();
    }
    public void SelectSixtySecondsMod()
    {
        sixtySecondsMod = true;
        time = timerAmount;
        score = 0;
        UIManager.Instance.SetTimer(time);
    }

    public void SelectThreeBulletsMod()
    {
        threeBulletMod = true;
    }

    public void EndGame()
    {
        isPlay = false;
        int maxScore = SaveAndLoad.Instance.GetMaxScore();
        if (score > maxScore)
            SaveAndLoad.Instance.SetMaxScore(score);
        UIManager.Instance.ActivateEndPanel();
    }

    private void UpdateTimer()
    {
        time -= Time.deltaTime;
        int timeInt = Mathf.FloorToInt(time);
        if (timeInt <= 0)
        {
            isPlay = false;
            timeInt = 0;
            EndGame();
        }
        UIManager.Instance.SetTimer(timeInt);
    }

    public void AddScore(int increaseAmount)
    {
        score += increaseAmount;
        UIManager.Instance.SetScore(score);
    }
    public void ActivateDucks()
    {
        int count = ReturnActivatedDucksCount();
        int index;
        for (int i = 0; i < count; i++)
        {
            index = Random.Range(0, ducks.Count);
            duck = ducks[index].GetComponent<Duck>();

            if (duck.isMove)
                i--;

            duck.Activate();
        }
    }
    private int ReturnActivatedDucksCount()
    {
        int count = 0;
        foreach (var duck in ducks)
        {
            if (duck.GetComponent<Duck>().isMove)
                count++;
        }
        return maxActivateDucksCount - count;
    }
}