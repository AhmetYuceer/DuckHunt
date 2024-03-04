using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoSingleton<HealthSystem>
{
    private const int maxHeartCount = 3;
    [SerializeField] private int currentHeart;

    private void Start()
    {
        currentHeart = maxHeartCount;
    }

    public void AddHeart()
    {
        if (currentHeart < maxHeartCount)
        {
            currentHeart += 1;
            UIManager.Instance.AddHearth();
        }
    }

    public void TakeDamage()
    {
        if (currentHeart <= 0)
            return;

        if (currentHeart > 0)
        {
            currentHeart -= 1;
            UIManager.Instance.TakeDamage();
            if (currentHeart <= 0)
            {
                GameManager.Instance.EndGame();
            }
        }
    }
}