using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public static FishSpawner Instance;
    private List<Fish> spawnedFish = new List<Fish>();
    [SerializeField] private Fish fishPrefab;

    [SerializeField] private float dayLength;
    private float spawnDelay = 1;

    private bool spawningEnabled;
    public bool SpawningEnabled
    {
        get { return spawningEnabled; } 
        set 
        {
            spawningEnabled = value;
            StartCoroutine(SpawningLoop());
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RoundManager.Instance.ChangeDay.AddListener(DayChange);
        RoundManager.Instance.Day = 0;
    }

    IEnumerator SpawningLoop()
    {
        float startTime = Time.time;

        while (SpawningEnabled)
        {
            SpawnFish();
            yield return new WaitForSeconds(spawnDelay);
            if (Time.time - startTime > dayLength) spawningEnabled = false;
        }

        yield return new WaitUntil(() => spawnedFish.Count == 0);
        RoundManager.Instance.EndDay();
    }

    private void SpawnFish()
    {
        Fish newFish = Instantiate(fishPrefab);
        spawnedFish.Add(newFish);
        newFish.RemoveFish.AddListener(RemoveFish);
    }

    public void RemoveFish(Fish fish)
    {
        spawnedFish.Remove(fish);
        Destroy(fish.gameObject);
    }

    private void DayChange(int day)
    {
        SpawningEnabled = true;
    }
}
