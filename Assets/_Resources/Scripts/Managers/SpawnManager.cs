using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private GameObject duckPrefab;

    [SerializeField] private Transform firstDuckSpawnTransform;
    [SerializeField] private Transform secondDuckSpawnTransform;

    public void SpawnDuck()
    {
        Instantiate(duckPrefab, firstDuckSpawnTransform.position, Quaternion.identity);
        Instantiate(duckPrefab, secondDuckSpawnTransform.position, Quaternion.identity);
    }
}