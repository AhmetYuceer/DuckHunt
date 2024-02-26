using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public bool isPlay { get; set; }

    [SerializeField] private List<GameObject> ducks = new List<GameObject>();
    [SerializeField] private int maxActivateDucksCount;
    [SerializeField] private float timerAmount;

    private Duck duck;
    private int score;
    private float time;

    void Start()
    {
        isPlay = false;
        time = timerAmount;
        score = 0;
        UIManager.Instance.SetTimer(time);
        SaveAndLoad.Instance.SetMaxScore(0);
        ActivateDucks();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlay = true;
        }

        if (isPlay)
        {
            SetTimer();
        }
    }

    private void EndGame()
    {
        int maxScore = SaveAndLoad.Instance.GetMaxScore();

        if (score > maxScore)
        {
            SaveAndLoad.Instance.SetMaxScore(score);
        }
        UIManager.Instance.ActivateEndPanel();
    }

    private void SetTimer()
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

    #region Public Functions
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
            {
                i--;
            }
            duck.Activate();
        }
    }
    #endregion

    #region Private Functions
    private int ReturnActivatedDucksCount()
    {
        int count = 0;
        foreach (var duck in ducks)
        {
            if (duck.GetComponent<Duck>().isMove)
            {
                count++;
            }
        }
        return maxActivateDucksCount - count;
    }
    #endregion
}