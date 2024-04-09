using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public static FishSpawner Instance;
    private List<Fish> spawnedFish = new List<Fish>();
    private Fish fishPrefab;

    private bool spawningEnabled;
    public bool SpawningEnabled
    {
        get { return spawningEnabled; } 
        set 
        { 
            spawningEnabled = value;
            SpawningLoop();
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RoundManager.Instance.ChangeDay.AddListener(DayChange);
    }

    IEnumerator SpawningLoop()
    {
        while (SpawningEnabled)
        {
            SpawnFish();
            yield return new WaitForSeconds(1);
        }
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

    }
}
