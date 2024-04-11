using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunManager : MonoBehaviour
{
    //Round manager is for each playthrough before death, GameManager is for the game as a whole including the Hub.
    public static RunManager Instance;

    public class IntEvent : UnityEvent<int> { }

    public IntEvent ChangeDay = new IntEvent();
    public UnityEvent StartNewRun = new UnityEvent();

    public float dayLength;
    [SerializeField] private float dayStartDelay;

    [SerializeField] private Casino casino;
    [SerializeField] private Store store;

    [SerializeField] private int day = 0;
    public int Day
    {
        get { return day; }
        set 
        { 
            day = value;
            Debug.Log("Day Time");
            ChangeDay?.Invoke(Day);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(StartRun());
    }

    public IEnumerator StartRun()
    {
        yield return new WaitForSeconds(dayStartDelay);
        StartNewRun?.Invoke();
        StartNextDay();
    }

    public void EndDay()
    {
        store.gameObject.SetActive(true);


        //Remove this when store is made
        //store.gameObject.SetActive(false);
        //StartCoroutine(StartNextDay());
    }

    public void EndRun()
    {
        casino.gameObject.SetActive(true);
    }

    public void StartNextDay()
    {
        store.gameObject.SetActive(false);
        Day += 1;
        FishSpawner.Instance.SpawningEnabled = true;
    }
}
