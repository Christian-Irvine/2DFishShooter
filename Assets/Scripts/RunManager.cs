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
    public UnityEvent EndDay = new UnityEvent();
    public UnityEvent StartNewRun = new UnityEvent();
    public UnityEvent CoinChange = new UnityEvent();
    public UnityEvent DropsPickedUp = new UnityEvent();

    [SerializeField] private float dayStartDelay;

    [SerializeField] private Casino casino;
    [SerializeField] private Store store;
    [SerializeField] private GameObject playObjects;

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
    }

    private void OnEnable()
    {
        StartCoroutine(StartRun());
    }

    public IEnumerator StartRun()
    {
        Reset();
        yield return new WaitForSeconds(dayStartDelay);
        StartNewRun?.Invoke();
        StartNextDay();
    }

    public void EndCurrentDay()
    {
        EndDay?.Invoke();
    }

    public void EndRun()
    {
        casino.gameObject.SetActive(true);
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

    private void Reset()
    {
        XP = 0;
        Coins = 0;
        Day = 0;
    }
}
