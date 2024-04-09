using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour
{
    //Round manager is for each playthrough before death, GameManager is for the game as a whole including the Hub.
    public static RoundManager Instance;

    public class IntEvent : UnityEvent<int> { }

    public IntEvent ChangeDay = new IntEvent();

    [SerializeField] private Casino casino;

    private int day = 0;
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

    public void EndDay()
    {
        casino.gameObject.SetActive(true);
    }

    public void StartDay()
    {
        casino.gameObject.SetActive(false);
        Day += 1;
    }
}
