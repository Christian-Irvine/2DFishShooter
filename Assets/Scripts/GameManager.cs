using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject hub;   
    [SerializeField] private GameObject game;   

    private void Awake()
    {
        Instance = this;
        //StartHub();
    }

    public void StartHub()
    {
        hub.SetActive(true);
        game.SetActive(false);
    }

    public void StartRound()
    {
        hub.SetActive(false);
        game.SetActive(true);
    }
}
