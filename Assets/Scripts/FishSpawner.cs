using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

[System.Serializable]
public class FishSpawnWeight
{
    public Fish fishPrefab;
    public int weight;
}

[System.Serializable]
public class FishWeekSpawns
{
    public List<FishSpawnWeight> weekWeights;
}

public class FishSpawner : MonoBehaviour
{
    public static FishSpawner Instance;

    [SerializeField] 

    private List<FishWeekSpawns> fishSpawnWeights;

    private List<Fish> spawnedFish = new List<Fish>();

    private int totalWeight = 0;
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
        RunManager.Instance.ChangeDay.AddListener(DayChange);
        RunManager.Instance.ChangeWeek.AddListener(WeekChange);
    }

    IEnumerator SpawningLoop()
    {
        float startTime = Time.time;

        while (SpawningEnabled)
        {
            SpawnFish();
            yield return new WaitForSeconds(spawnDelay);
            if (Time.time - startTime > RunManager.Instance.DayLength) spawningEnabled = false;
        }

        yield return new WaitUntil(() => spawnedFish.Count == 0);
        RunManager.Instance.EndCurrentDay();
    }

    private void SpawnFish()
    {
        int direction = Random.Range(0, 2) == 0 ? -1 : 1;

        Fish newFish = Instantiate(PickFish(), transform);
        newFish.direction = direction;
        newFish.transform.position = new Vector3 (10 * -direction, Random.Range(-4f, 4f), newFish.transform.position.z);

        spawnedFish.Add(newFish);
        newFish.RemoveFish.AddListener(RemoveFish);
    }

    private Fish PickFish()
    {
        int pickedWeight = Random.Range(0, totalWeight + 1);
        int weightCount = 0;

        foreach (FishSpawnWeight spawn in fishSpawnWeights[GetSpawnWeekIndex()].weekWeights)
        {
            weightCount += spawn.weight;
            if (pickedWeight <= weightCount)
            {
                return spawn.fishPrefab;
            }
        }

        Debug.LogWarning($"Picked number was above total weight. Picked:{pickedWeight}, weightCount:{weightCount}.");
        return null;
    }

    public void RemoveFish(Fish fish)
    {
        spawnedFish.Remove(fish);
        Destroy(fish.gameObject);
    }

    private void DayChange(int day)
    {
        totalWeight = 0;

        fishSpawnWeights[GetSpawnWeekIndex()].weekWeights.ForEach(fish =>
        {
            totalWeight += fish.weight;
        });
    }

    private void WeekChange(int week)
    {
        Debug.Log(week);
    }

    private int GetSpawnWeekIndex()
    {
        if (fishSpawnWeights.Count == 0) Debug.LogWarning("No Fish Weights Exist!");
        return Mathf.Min(RunManager.Instance.Week - 1, fishSpawnWeights.Count - 1);
    }
}
