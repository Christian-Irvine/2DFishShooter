using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject hub;   
    [SerializeField] private GameObject shooting;   
    [SerializeField] private GameObject midRoundCasino;   
    [SerializeField] private GameObject postGameCasino;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hub.SetActive(true);
        shooting.SetActive(false);
        midRoundCasino.SetActive(false);
        postGameCasino.SetActive(false);
    }
}
