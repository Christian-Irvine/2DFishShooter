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
    public IntEvent ChangeWeek = new IntEvent();
    public UnityEvent EndDay = new UnityEvent();
    public UnityEvent StartNewRun = new UnityEvent();
    public UnityEvent EndRun = new UnityEvent();
    public UnityEvent CoinChange = new UnityEvent();
    public UnityEvent DropsPickedUp = new UnityEvent();
    public UnityEvent EndRunReset = new UnityEvent();

    [SerializeField] private float runStartDelay;
    [SerializeField] private int daysInWeek;

    [SerializeField] private CasinoManager casino;
    [SerializeField] private StoreManager store;
    [SerializeField] private GameObject playObjects;

    private bool runOver;
    public bool RunOver {  get { return runOver; } }

    [SerializeField] private int week;
    public int Week
    {
        get { return week; }
        set 
        { 
            week = value;
            ChangeWeek?.Invoke(Week);
        }
    }

    [SerializeField] private float xP = 0;
    public float XP
    {
        get { return xP; }
        set { xP = value; }
    }

    [SerializeField] private int coins = 0;
    public int Coins
    {
        get { return coins; }
        set 
        { 
            coins = value;
            CoinChange?.Invoke();
        }
    }


    [SerializeField] private float dayLength;
    public float DayLength { get { return dayLength; } }
    [SerializeField] private int day = 0;
    public int Day
    {
        get { return day; }
        set 
        { 
            day = value;

            int newWeek = Mathf.FloorToInt((day - 1) / daysInWeek) + 1;
            if (Week != newWeek) Week = newWeek;

            ChangeDay?.Invoke(Day);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DropsPickedUp.AddListener(OpenStore);
        EndRun.AddListener(OnEndRun);
        EndRunReset.AddListener(ResetRun);
    }

    public IEnumerator StartRun()
    {
        ResetRun();
        yield return new WaitForSeconds(runStartDelay);
        StartNewRun?.Invoke();
        StartNextDay();
    }

    public void EndCurrentDay()
    {
        if (!runOver)
        {
            Debug.Log("Day Ended");
            EndDay?.Invoke();
        }
    }

    private void OnEndRun()
    {
        Debug.Log("The Run Is Over!");
        runOver = true;
        StartCoroutine(OpenCasino());
    }

    IEnumerator OpenCasino()
    {
        yield return new WaitUntil(() => runOver);
        casino.gameObject.SetActive(true);
        playObjects.SetActive(false);
    }

    public void StartNextDay()
    {
        store.gameObject.SetActive(false);
        casino.gameObject.SetActive(false);
        playObjects.SetActive(true);
        Day += 1;
        FishSpawner.Instance.SpawningEnabled = true;
    }

    public void OpenStore()
    {
        store.gameObject.SetActive(true);
        playObjects.SetActive(false);
    }

    private void ResetRun()
    {
        runOver = false;
        XP = 0;
        Coins = 0;
        Day = 0;
    }
}
