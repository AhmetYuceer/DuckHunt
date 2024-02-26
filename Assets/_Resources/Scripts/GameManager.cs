using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private Duck duck;
    [SerializeField] private List<GameObject> ducks = new List<GameObject>();
    [SerializeField] private int maxActivateDucksCount;

    void Start()
    {
        ActivateDucks();
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
}